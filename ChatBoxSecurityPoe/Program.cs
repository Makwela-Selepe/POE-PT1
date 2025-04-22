using System;
using System.Threading;

namespace ChatBoxSecurityPoe
{
    // User class to store user-related information
    public class User
    {
        public string Name { get; set; }
    }

    // Program class to handle the security chatbot logic
    public class Program
    {
        // Static method to display type effect text
        public static void TypeEffect(string text, int delay = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        // Static method to display ASCII art
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
    }

    // Main method class to initiate the program
    public class MainMethod
    {
        // Entry point for the application
        public static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";

            // Initialize and call methods from Program class
            Program.DisplayAsciiAr();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=============================================");
            Console.WriteLine("           WELCOME TO THE SECURITY HUB        ");
            Console.WriteLine("=============================================");
            Console.WriteLine("Please enter your name:  ");
            string inputName = Console.ReadLine();

            // Ensure the name is not empty or whitespace
            while (string.IsNullOrWhiteSpace(inputName))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Name can't be blank. Please enter a name:");
                Console.ResetColor();
                inputName = Console.ReadLine();
            }

            // Create user object
            User user = new User { Name = inputName };
            Console.Clear();
            Program.DisplayAsciiAr();
            Console.ForegroundColor = ConsoleColor.Green;
            Program.TypeEffect($"Nice to meet you, {user.Name.ToUpper()}!");
            Console.ResetColor();

            // User interaction
            Console.WriteLine("Ask me something?");
            Console.Write("\nYour Question: ");
            string question = Console.ReadLine().ToLower();

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
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress any key to exit...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
