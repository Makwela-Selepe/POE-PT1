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
    }

    public class Program
    {
        // Memory for storing user data
        public static Dictionary<string, string> memory = new Dictionary<string, string>();

        // Cybersecurity keyword-based tips
        public static Dictionary<string, string> keywordResponses = new Dictionary<string, string>
        {
            {"password", "Make sure to use strong, unique passwords for each account. Avoid using personal details."},
            {"scam", "Be cautious of unsolicited messages. Never share personal info or click unknown links."},
            {"privacy", "Review your app permissions and limit the information you share online."}
        };

        // Sentiment-based empathetic replies
        public static Dictionary<string, string> sentimentResponses = new Dictionary<string, string>
        {
            {"worried", "It's completely understandable to feel that way. Let's go through some tips to ease your concern."},
            {"curious", "Great to see your curiosity! Let's explore that topic further."},
            {"frustrated", "Cybersecurity can be overwhelming at times. You're not alone—let's take it step by step."}
        };

        // To maintain context
        public static string lastTopic = "";

        // Typing effect
        public static void TypeEffect(string text, int delay = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        // ASCII Art Banner
        public static void DisplayAsciiAr()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
                      ,--,      ,--,                                             ,----,                       
       ,--.        ,---.'|   ,---.'|                                           ,/   .`|                  ,--, 
   ,--/  /|   ,---,|   | :   |   | :   .--.--.              .---.   ,---,    ,`   .'  : ,----..        ,--.'| 
,---,': / ',`--.' |:   : |   :   : |  /  /    '.           /. ./|,`--.' |  ;    ;     //   /   \    ,--,  | : 
:   : '/ / |   :  :|   ' :   |   ' : |  :  /`. /       .--'.  ' ;|   :  :.'___,/    ,'|   :     :,---.'|  : ' 
|   '   ,  :   |  ';   ; '   ;   ; ' ;  |  |--`       /__./ \ : |:   |  '|    :     | .   |  ;. /|   | : _' | 
'   |  /   |   :  |'   | |__ '   | |_|  :  ;_     .--'.  '   \\' .|   :  |;    |.';  ; .   ; /--` :   : |.'  | 
|   ;  ;   '   '  ;|   | :.'||   | :.'\\  \\    `. /___/ \\ |    ' ''   '  ;`----'  |  | ;   | ;    |   ' '  ; : 
:   '   \\  |   |  |'   :    ;'   :    ;`----.   \\;   \\  \\;      :|   |  |    '   :  ; |   : |    '   |  .'. | 
|   |    ' '   :  ;|   |  ./ |   |  ./ __ \\  \\  | \\   ;  `      |'   :  ;    |   |  ' .   | '___ |   | :  | ' 
'   : |.  \\|   |  ';   : ;   ;   : ;  /  /`--'  /  .   \\    .\\  ;|   |  '    '   :  | '   ; : .'|'   : |  : ; 
|   | '_\\.''   :  ||   ,/    |   ,/  '--'.     /    \\   \\   ' \\ |'   :  |    ;   |.'  '   | '/  :|   | '  ,/  
");
            Console.ResetColor();
        }

        // Play a WAV audio file
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

        // Handle user input with all features
        public static void HandleUserInput(string input)
        {
            input = input.ToLower();

            if (string.IsNullOrWhiteSpace(input))
            {
                TypeEffect("I didn't quite understand that. Could you rephrase?");
                return;
            }

            var matchedSentiment = sentimentResponses.Keys.FirstOrDefault(s => input.Contains(s));
            if (matchedSentiment != null)
            {
                TypeEffect(sentimentResponses[matchedSentiment]);
                return;
            }

            var matchedKeyword = keywordResponses.Keys.FirstOrDefault(k => input.Contains(k));
            if (matchedKeyword != null)
            {
                TypeEffect(keywordResponses[matchedKeyword]);
                lastTopic = matchedKeyword;

                if (!memory.ContainsKey("topic"))
                {
                    memory["topic"] = matchedKeyword;
                    TypeEffect($"I'll remember that you're interested in {matchedKeyword}. It's an important part of cybersecurity.");
                }
                return;
            }

            if (input.Contains("more") || input.Contains("explain"))
            {
                if (!string.IsNullOrEmpty(lastTopic) && keywordResponses.ContainsKey(lastTopic))
                {
                    TypeEffect($"Here's a bit more on {lastTopic}: {keywordResponses[lastTopic]}");
                }
                else
                {
                    TypeEffect("Can you tell me which topic you'd like more details about?");
                }
                return;
            }

            if (input.Contains("remember") && memory.ContainsKey("topic"))
            {
                string rememberedTopic = memory["topic"];
                TypeEffect($"You told me you're interested in {rememberedTopic}. Here's a tip on that: {keywordResponses[rememberedTopic]}");
                return;
            }

            // Default fallback
            TypeEffect("I'm not sure I understand. Can you try rephrasing?");
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
            Console.Write("Please enter your name: ");
            string inputName = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(inputName))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Name can't be blank. Please enter a name:");
                Console.ResetColor();
                inputName = Console.ReadLine();
            }

            User user = new User { Name = inputName };
            Program.memory["name"] = user.Name;

            Console.Clear();
            Program.DisplayAsciiAr();
            Console.ForegroundColor = ConsoleColor.Green;

            // Update the path to match your environment
            string audioPath = @"C:\Users\lab_services_student\Desktop\POE PT1 PROG\ChatBoxSecurityPoe\ChatBoxSecurityPoe\Audio\Welcome message.wav";
            Program.PlayWav(audioPath);

            Program.TypeEffect($"Nice to meet you, {user.Name.ToUpper()}!");
            Console.ResetColor();

            string question;
            while (true)
            {
                Console.WriteLine("\nAsk me something, or type 'bye' to exit.");
                Console.Write("Your Question: ");
                question = Console.ReadLine();

                if (question.ToLower() == "bye")
                {
                    Program.TypeEffect("Goodbye! Stay safe in the digital world.");
                    break;
                }

                Console.ForegroundColor = ConsoleColor.White;
                Program.HandleUserInput(question);
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress any key to exit...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
