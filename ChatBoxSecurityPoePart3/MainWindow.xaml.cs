// MainWindow.xaml.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CyberChat
{
    public partial class MainWindow : Window
    {
        private string userName = "";
        private string favoriteTopic = "";
        private List<string> activityLog = new List<string>();
        private List<TaskItem> tasks = new List<TaskItem>();
        private DispatcherTimer reminderTimer;
        private int quizScore = 0;
        private int quizIndex = 0;
        private bool quizActive = false;

        private bool awaitingReminderDescription = false;
        private string pendingReminderDescription = "";
        private bool awaitingReminderDelay = false;

        private static readonly Dictionary<string, List<string>> responses = new Dictionary<string, List<string>>
        {
            ["password"] = new List<string>
            {
                "Use strong, unique passwords for every account.",
                "Avoid common passwords and consider using a password manager.",
                "Update your passwords regularly and use 2FA."
            },
            ["scam"] = new List<string>
            {
                "Be cautious of unsolicited emails or messages.",
                "Never click suspicious links, even if they look legit.",
                "Report scam attempts to your email provider or IT."
            },
            ["privacy"] = new List<string>
            {
                "Review app permissions and limit data sharing.",
                "Use encrypted apps and VPNs to protect your data.",
                "Keep your accounts private and secure."
            }
        };

        private readonly List<QuizQuestion> quizQuestions = new List<QuizQuestion>
        {
            new QuizQuestion("What should you do if you receive an email asking for your password?", new[] { "A) Reply with your password", "B) Delete the email", "C) Report the email as phishing", "D) Ignore it" }, 2),
            new QuizQuestion("Which of these is a strong password?", new[] { "123456", "password", "!Qx7$eLp@2", "qwerty" }, 2),
            new QuizQuestion("True or False: Public Wi-Fi is always safe to use.", new[] { "True", "False" }, 1),
            new QuizQuestion("What does 2FA stand for?", new[] { "Two-Factor Authentication", "Twice-forgotten Access", "Trusted Firewall Access", "Two-Form Algorithm" }, 0),
            new QuizQuestion("What is phishing?", new[] { "A cyber-attack to trick users into revealing information", "Catching fish online", "Scanning for viruses", "Buying software" }, 0),
            new QuizQuestion("Why should you update your software regularly?", new[] { "To remove bugs", "To enhance security", "To get new features", "All of the above" }, 3),
            new QuizQuestion("What is a VPN used for?", new[] { "Increasing internet speed", "Secure browsing", "Tracking users", "None" }, 1),
            new QuizQuestion("True or False: You should use the same password for all sites.", new[] { "True", "False" }, 1),
            new QuizQuestion("Which is an example of social engineering?", new[] { "Sending fake login pages", "Guessing passwords", "Using software exploits", "Firewall configuration" }, 0),
            new QuizQuestion("How do you recognize a secure website?", new[] { "It has https and a padlock icon", "It loads quickly", "It has ads", "It asks for your password" }, 0)
        };

        public MainWindow()
        {
            InitializeComponent();
            AddBotMessageAnimated("Welcome to the Cybersecurity Awareness Bot! What's your name?");
            reminderTimer = new DispatcherTimer();
            reminderTimer.Interval = TimeSpan.FromSeconds(10);
            reminderTimer.Tick += ReminderTimer_Tick;
            reminderTimer.Start();
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;
            AddUserMessage(input);
            await ProcessInputAsync(input.ToLower());
            UserInput.Text = "";
        }

        private async Task ProcessInputAsync(string input)
        {
            if (string.IsNullOrEmpty(userName))
            {
                userName = input;
                AddBotMessageAnimated($"Nice to meet you, {userName}!");
                activityLog.Add($"User name set to {userName}");
                AddBotMessageAnimated("How can I assist you in cybersecurity today?");
                return;
            }

            if (awaitingReminderDescription)
            {
                pendingReminderDescription = input;
                awaitingReminderDescription = false;
                awaitingReminderDelay = true;
                AddBotMessageAnimated("In how many days should I remind you?");
                return;
            }

            if (awaitingReminderDelay)
            {
                if (int.TryParse(input, out int days))
                {
                    tasks.Add(new TaskItem
                    {
                        Description = pendingReminderDescription,
                        RemindAt = DateTime.Now.AddDays(days)
                    });
                    AddBotMessageAnimated($"Reminder set for '{pendingReminderDescription}' in {days} day(s).");
                    activityLog.Add($"Reminder set: {pendingReminderDescription} in {days} day(s)");
                }
                else
                {
                    AddBotMessageAnimated("Sorry, please enter a number (e.g., 3 for 3 days).");
                    return;
                }
                awaitingReminderDelay = false;
                pendingReminderDescription = "";
                return;
            }

            if (quizActive)
            {
                HandleQuizAnswer(input);
                return;
            }

            if (input.Contains("bye"))
            {
                AddBotMessageAnimated("Goodbye! Stay safe online.");
                activityLog.Add("User exited the chatbot.");
                Application.Current.Shutdown();
                return;
            }

            if (input.Contains("activity log") || input.Contains("what have you done"))
            {
                string log = string.Join("\n", activityLog.Skip(Math.Max(0, activityLog.Count - 10)).Select((x, i) => $"{i + 1}. {x}"));
                AddBotMessageAnimated("Here's a summary of recent actions:\n" + log);
                return;
            }

            if (input.Contains("set reminder"))
            {
                awaitingReminderDescription = true;
                AddBotMessageAnimated("What should I remind you about?");
                return;
            }

            if (input.Contains("quiz"))
            {
                StartQuiz();
                return;
            }

            foreach (var keyword in responses.Keys)
            {
                if (input.Contains(keyword))
                {
                    favoriteTopic = keyword;
                    AddBotMessageAnimated(GetRandomResponse(keyword));
                    activityLog.Add($"Provided advice on {keyword}");
                    return;
                }
            }

            AddBotMessageAnimated("I'm not sure I understand. Try asking about password, scam, or privacy, or type 'start quiz'.");
        }

        private void StartQuiz()
        {
            quizScore = 0;
            quizIndex = 0;
            quizActive = true;
            AddBotMessageAnimated("Let's start the Cybersecurity Quiz!");
            AskNextQuizQuestion();
        }

        private void AskNextQuizQuestion()
        {
            if (quizIndex < quizQuestions.Count)
            {
                var q = quizQuestions[quizIndex];
                AddBotMessageAnimated(q.Question + "\n" + string.Join("\n", q.Options));
            }
            else
            {
                quizActive = false;
                AddBotMessageAnimated($"Quiz complete! You scored {quizScore}/{quizQuestions.Count}.");
                activityLog.Add($"Quiz completed: {quizScore}/{quizQuestions.Count}");
            }
        }

        private void HandleQuizAnswer(string input)
        {
            var q = quizQuestions[quizIndex];
            int answerIndex = -1;

            for (int i = 0; i < q.Options.Length; i++)
            {
                if (input.Contains(((char)('a' + i)).ToString().ToLower()) || input.Contains(i.ToString()))
                {
                    answerIndex = i;
                    break;
                }
            }

            if (answerIndex == q.CorrectAnswer)
            {
                quizScore++;
                AddBotMessageAnimated("Correct!");
            }
            else
            {
                AddBotMessageAnimated($"Wrong. The correct answer is: {q.Options[q.CorrectAnswer]}");
            }
            quizIndex++;
            AskNextQuizQuestion();
        }

        private string GetRandomResponse(string keyword)
        {
            var list = responses[keyword];
            return list[new Random().Next(list.Count)];
        }

        private void AddUserMessage(string message)
        {
            ChatStack.Children.Add(new TextBlock
            {
                Text = $"You: {message}",
                Foreground = System.Windows.Media.Brushes.LightGreen,
                Margin = new Thickness(5)
            });
        }

        private async void AddBotMessageAnimated(string message)
        {
            var tb = new TextBlock
            {
                Foreground = System.Windows.Media.Brushes.LightBlue,
                Margin = new Thickness(5)
            };
            ChatStack.Children.Add(tb);

            foreach (char c in $"Bot: {message}")
            {
                tb.Text += c;
                await Task.Delay(30);
            }
        }

        private void ReminderTimer_Tick(object sender, EventArgs e)
        {
            var dueTasks = tasks.Where(t => t.RemindAt <= DateTime.Now && !t.Reminded).ToList();
            foreach (var task in dueTasks)
            {
                AddBotMessageAnimated($"Reminder: {task.Description}");
                activityLog.Add($"Reminder triggered: {task.Description}");
                task.Reminded = true;
            }
        }
    }

    public class TaskItem
    {
        public string Description { get; set; }
        public DateTime RemindAt { get; set; }
        public bool Reminded { get; set; } = false;
    }

    public class QuizQuestion
    {
        public string Question { get; set; }
        public string[] Options { get; set; }
        public int CorrectAnswer { get; set; }

        public QuizQuestion(string question, string[] options, int correct)
        {
            Question = question;
            Options = options;
            CorrectAnswer = correct;
        }
    }
}
