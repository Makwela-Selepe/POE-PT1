using System;
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
        public static Dictionary<string, string> memory = new Dictionary<string, string>();
        public static Dictionary<string, string> keywordResponses = new Dictionary<string, string>
{
    {"password", "Make sure to use strong, unique passwords for each account. Avoid using personal details."},
    {"scam", "Be cautious of unsolicited messages. Never share personal info or click unknown links."},
    {"privacy", "Review your app permissions and limit the information you share online."}
};

        public static Dictionary<string, string> sentimentResponses = new Dictionary<string, string>
{
    {"worried", "It's completely understandable to feel that way. Let's go through some tips to ease your concern."},
    {"curious", "Great to see your curiosity! Let's explore that topic further."},
    {"frustrated", "Cybersecurity can be overwhelming at times. You're not alone—let's take it step by step."}
};

        public static string lastTopic = "";

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

            // 👇 Use your provided path
            string audioPath = @"C:\Users\lab_services_student\Desktop\POE PT1 PROG\ChatBoxSecurityPoe\ChatBoxSecurityPoe\Audio\Welcome message.wav";
            Program.PlayWav(audioPath);

            Program.TypeEffect($"Nice to meet you, {user.Name.ToUpper()}!");
            Console.ResetColor();

            string question;
            while (true)
            {
                Console.WriteLine("Ask me something, or type 'bye' to exit.");
                Console.Write("\nYour Question: ");
                question = Console.ReadLine().ToLower();

                if (question == "bye")
                {
                    Program.TypeEffect("Goodbye! Stay safe in the digital world.");
                    break;
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

                if (string.IsNullOrWhiteSpace(question))
                {
                    Program.TypeEffect("I didn't quite understand that. Could you rephrase?");
                }
                else if (question.Contains("how are you"))
                {
                    Program.TypeEffect("I'm functioning optimally, thank you!");
                }
                else if (question.Contains("purpose"))
                {
                    Program.TypeEffect("My purpose is to help users stay safe in the digital world.");
                }
                else if (question.Contains("ask"))
                {
                    Program.TypeEffect("You can ask me about:");
                    Program.TypeEffect("Password safety");
                    Program.TypeEffect("Phishing Attacks");
                    Program.TypeEffect("Safe browsing habits");
                }
                else if (question.Contains("password"))
                {
                    Program.TypeEffect("Always use complex passwords and avoid reusing them. Consider a password manager.");
                }
                else if (question.Contains("phishing"))
                {
                    Program.TypeEffect("Phishing is a cyber attack that tricks you into revealing sensitive information. Never click unknown links.");
                }
                else if (question.Contains("browsing"))
                {
                    Program.TypeEffect("Use HTTPS sites, enable browser security settings, and don't download from untrusted sources.");
                }
                else
                {
                    Program.TypeEffect("I didn't quite understand that. Could you rephrase?");
                }

                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress any key to exit...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
