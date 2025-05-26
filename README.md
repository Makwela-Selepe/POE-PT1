# Cybersecurity Awareness Chatbot

This is a console-based C# chatbot designed to educate users about essential cybersecurity practices. It simulates an interactive conversation to help users understand topics like password safety, phishing attacks, scams, and online privacy.

## Features

- Dynamic Responses: Recognizes keywords like "password", "phishing", "scam", and "privacy" and responds with random helpful tips.
- Sentiment Detection: Detects basic sentiments (e.g., "worried", "curious", "frustrated") and adjusts tone to be empathetic or supportive.
- Memory and Recall: Remembers the user's name and preferred cybersecurity topic to personalize responses.
- Keyword Recognition: Scans user input for specific cybersecurity-related keywords and responds accordingly.
- Conversation Flow: Supports ongoing dialogue without restarting the session.
- Error Handling: Handles unknown or unexpected inputs with default fallback responses.
- Modular Code Design: Code is split into multiple classes for maintainability and future expansion.

## Project Structure

ChatBoxSecurityPoe/
│
├── Program.cs                 # Entry point; handles UI and flow
├── ChatBot.cs                 # Main logic for chatbot interaction
├── ChatMemory.cs              # Stores user information and interests
├── CyberKeywordResponder.cs   # Manages cybersecurity topics and tips
├── SentimentAnalyzer.cs       # Detects and responds to user sentiment
├── User.cs                    # User model with basic properties

## Technologies Used

- C#
- .NET Console Application
- NAudio (for optional WAV file playback)

## Getting Started

1. Clone or download the repository.
2. Open the solution in Visual Studio.
3. Ensure the NAudio package is installed via NuGet if you plan to use the audio features.
4. Build and run the project.

## Usage

- The bot begins by greeting the user and asking for their name.
- Users can ask questions or express concerns using natural language.
- The bot identifies cybersecurity keywords and sentiment to provide appropriate guidance.
- Type `bye` to exit the application.

## Example Interactions

User: I'm worried about online scams.  
Bot: It's completely understandable to feel that way. Scammers can be very convincing. Let me share some tips to help you stay safe.

User: Tell me about password safety.  
Bot: Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords.

## License

This project is intended for educational use and may be reused or modified with proper attribution.
