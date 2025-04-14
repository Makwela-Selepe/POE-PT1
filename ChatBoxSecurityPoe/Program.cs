// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Threading;
using System.Media;
namespace ChatBoxSecurityPoe
{
    class User
    {
        public string Name { get; set; }
    }
    class Program {
        static void TypeEffect(string text, int delay = 30)
        {
            foreach (char c in text) {
                Console.Write(c);
                     Thread.Sleep(delay);

            }
            Console.WriteLine();
            static void DisplayAsciiAr()
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("@");
                Console.ResetColor();
            }
       static void Main(string[] args)
            {
                Console.Title = "Cybersecurity Awareness Bot";

          DisplayAsciiAr();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=============================================");
                Console.WriteLine("           WELCOME TO THE SECURITY HUB        ");
                Console.WriteLine("=============================================");
                Console.WriteLine("Please enter you name:  ");
                string inputName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(inputName))
                {
                Console.ForegroundColor= ConsoleColor.Cyan;
                    Console.WriteLine("Nmae cant be blank. Please enter a name:");
                    Console.ResetColor();
                    inputName= Console.ReadLine();   
                }
                User user = new User { Name = inputName };
                Console.Clear();
                DisplayAsciiAr();
                Console.ForegroundColor = ConsoleColor.Green;
                TypeEffect($"Nice to meet you, {user.Name.ToUpper()}!");
                Console.ResetColor() ;

                Console.WriteLine("Ask me something?" );
                Console.Write("\nYour Question: ");
                string question = Console.ReadLine().ToLower();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                if (string.IsNullOrWhiteSpace(question))
                {
                    TypeEffect("I dindt quite understand that.Could you rephrase?");
                }
                else if (question.Contains("how are you"))
                {
                    TypeEffect("Im functioning optimally , thank you!");
                }
                else if (question.Contains("purpose")) {
                    
                }



        }
        }
    }
}