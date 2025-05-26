using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

using System;

namespace ChatBoxSecurityPoe
{
    public class ChatBot
    {
        private readonly ChatMemory _memory;

        public ChatBot(ChatMemory memory)
        {
            _memory = memory;
        }

        public void GreetUser()
        {
            Console.Write("Please enter your name: ");
            string name = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name can't be blank. Please enter a valid name:");
                name = Console.ReadLine();
            }

            _memory.CurrentUser = new User { Name = name };
            WriteBotResponse($"\nNice to meet you, {_memory.CurrentUser.Name.ToUpper()}!");
        }

        public void StartConversation()
        {
            while (true)
            {
                Console.Write("\nAsk me something, or type 'bye' to exit: ");
                string question = Console.ReadLine().ToLower();

                if (question == "bye")
                {
                    WriteBotResponse("Goodbye! Stay safe in the digital world.");
                    break;
                }

                var sentiment = SentimentAnalyzer.DetectSentiment(question);
                var sentimentResponse = SentimentAnalyzer.GetSentimentResponse(sentiment);
                if (sentimentResponse != null)
                    WriteBotResponse(sentimentResponse);

                if (CyberKeywordResponder.IsKeyword(question, out string keyword))
                {
                    _memory.RememberTopic(keyword);
                    WriteBotResponse(CyberKeywordResponder.GetRandomResponse(keyword));
                }
                else if (question.Contains("favorite") || question.Contains("interested"))
                {
                    string interest = _memory.RecallInterest();
                    if (!string.IsNullOrEmpty(interest))
                        WriteBotResponse($"As someone interested in {interest}, here's a tip: {CyberKeywordResponder.GetRandomResponse(interest)}");
                    else
                        WriteBotResponse("Tell me what interests you in cybersecurity so I can tailor advice to you!");
                }
                else
                {
                    WriteBotResponse("I'm not sure I understand. Can you try rephrasing?");
                }
            }
        }

        private void WriteBotResponse(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}

