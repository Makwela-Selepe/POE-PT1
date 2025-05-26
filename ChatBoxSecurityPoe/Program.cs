using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NAudio.Wave;

namespace ChatBoxSecurityPoe
{
    public class User
    {
        public string Name { get; set; }
        public string FavoriteTopic { get; set; }
    }

    public class Program
    {
        static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["password"] = new List<string>
            {
                "Make sure to use strong, unique passwords for each account. Avoid using personal details.",
                "Consider using a password manager to generate and store secure passwords.",
                "Update your passwords regularly and enable two-factor authentication when possible."
            },
            ["scam"] = new List<string>
            {
                "Watch out for emails asking for personal information. Always verify the sender.",
                "If an offer sounds too good to be true, it probably is — especially online.",
                "Avoid clicking links from unknown sources, and double-check website URLs for legitimacy."
            },
            ["privacy"] = new List<string>
            {
                "Review your social media privacy settings to control who sees your information.",
                "Limit the amount of personal info you share online — less is more.",
                "Use end-to-end encrypted apps for private conversations and secure your browsing with VPNs."
            }
        };

        static List<string> positiveResponses = new List<string>
        {
            "Great question!",
            "I'm glad you're curious about that.",
            "Let me share something useful with you."
        };

        static List<string> confusedResponses = new List<string>
        {
            "I'm not sure I understand. Can you try rephrasing?",
            "That seems unclear — could you be more specific?",
            "Hmm, that doesn't match what I know. Want to try asking differently?"
        };

        static Dictionary<string, string> sentimentResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["worried"] = "It's completely understandable to feel that way. Let me help you stay safe.",
            ["frustrated"] = "I know cybersecurity can be overwhelming. I'm here to make it easier.",
            ["curious"] = "Curiosity is great! Let's learn more together."
        };

        public static void TypeEffect(string text, int delay = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        public static void DisplayAsciiAr()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
 ██████╗██╗   ██╗██████╗ ███████╗███████╗██╗   ██╗
██╔════╝██║   ██║██╔══██╗██╔════╝██╔════╝╚██╗ ██╔╝
██║     ██║   ██║██████╔╝█████╗  █████╗   ╚████╔╝ 
██║     ██║   ██║██╔═══╝ ██╔══╝  ██╔══╝    ╚██╔╝  
╚██████╗╚██████╔╝██║     ███████╗███████╗   ██║   
 ╚═════╝ ╚═════╝ ╚═╝     ╚══════╝╚══════╝   ╚═╝   
");
            Console.ResetColor();
        }

        public static void PlayWav(string path)
        {
            try
            {
                using (AudioFileReader audioFile = new AudioFileReader(path))
                using (WaveOutEvent outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error playing sound: " + ex.Message);
                Console.ResetColor();
            }
        }

        public static void RespondToQuestion(string question, User user)
        {
            question = question.ToLower();

            // Sentiment Detection
            foreach (var sentiment in sentimentResponses.Keys)
            {
                if (question.Contains(sentiment))
                {
                    TypeEffect(sentimentResponses[sentiment]);
                    return;
                }
            }

            // Topic detection with memory storage
            foreach (var keyword in keywordResponses.Keys)
            {
                if (question.Contains(keyword))
                {
                    user.FavoriteTopic = keyword;
                    string response = keywordResponses[keyword][new Random().Next(keywordResponses[keyword].Count)];
                    string positivePrefix = positiveResponses[new Random().Next(positiveResponses.Count)];

                    TypeEffect($"{positivePrefix} {response}");
                    return;
                }
            }

            // User asks for more info on remembered topic
            if (question.Contains("more") || question.Contains("details"))
            {
                if (!string.IsNullOrEmpty(user.FavoriteTopic) && keywordResponses.ContainsKey(user.FavoriteTopic))
                {
                    string topic = user.FavoriteTopic;
                    string detail = keywordResponses[topic][new Random().Next(keywordResponses[topic].Count)];
                    TypeEffect($"As someone interested in {topic}, here's more: {detail}");
                }
                else
                {
                    TypeEffect("Sure! Could you remind me which topic you're interested in?");
                }
                return;
            }

            // Default fallback
            TypeEffect(confusedResponses[new Random().Next(confusedResponses.Count)]);
        }
    }

    public class MainMethod
    {
        public static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";
            Program.DisplayAsciiAr();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=============================================");
            Console.WriteLine("           WELCOME TO THE SECURITY HUB        ");
            Console.WriteLine("=============================================");
            Console.WriteLine("Please enter your name:  ");
            string inputName = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(inputName))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Name can't be blank. Please enter a name:");
                Console.ResetColor();
                inputName = Console.ReadLine();
            }

            User user = new User { Name = inputName };
            Console.Clear();
            Program.DisplayAsciiAr();
            Console.ForegroundColor = ConsoleColor.Green;

            // 👇 Use your actual path
            string audioPath = @"C:\Users\lab_services_student\Desktop\POE PT1 PROG\ChatBoxSecurityPoe\ChatBoxSecurityPoe\Audio\Welcome message.wav";
            Program.PlayWav(audioPath);

            Program.TypeEffect($"Nice to meet you, {user.Name.ToUpper()}!");
            Console.ResetColor();

            string question;
            while (true)
            {
                Console.WriteLine("\nAsk me something, or type 'bye' to exit.");
                Console.Write("\nYour Question: ");
                question = Console.ReadLine().ToLower();

                if (question == "bye")
                {
                    Program.TypeEffect("Goodbye! Stay safe in the digital world.");
                    break;
                }

                if (!string.IsNullOrWhiteSpace(question))
                {
                    Program.RespondToQuestion(question, user);
                }
                else
                {
                    Program.TypeEffect("I didn't quite catch that. Please ask something.");
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress any key to exit...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
