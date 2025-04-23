## Cybersecurity Awareness Bot
This is a console-based chatbot application built in C# designed to educate users about cybersecurity practices. It uses ASCII art, sound effects, and a typewriter-style output for a more engaging experience.

## Project Structure
User class: Represents the user interacting with the bot.

Program class: Contains the main logic including:

ASCII art display

Text typing effect

Audio playback

MainMethod class: Contains the Main() function which serves as the entry point for the application.

## Features
 User Input Handling
Prompts the user to enter their name and validates that it's not empty.

ASCII Art
Displays stylized welcome ASCII art in magenta color for a cool visual effect.

## Audio Playback
Plays a .wav audio file as a welcome message. (Make sure the file path is correct.)

## Typewriter Effect
Prints a message one character at a time to simulate a typing effect.

Basic Chatbot Functionality
Responds to a users input with relevant cybersecurity tips:

Password safety

Phishing attack awareness

Safe browsing habits

Also includes fun conversational responses like “How are you?”

## Audio File
Make sure to update the path to the .wav file:

csharp
Copy
Edit
string audioPath = @"C:\Your\Path\To\Welcome message.wav";
If the file cannot be found or can't be played, an error will be shown in red.

## How to Run
Open the project in Visual Studio or any C# compatible IDE.

Ensure NAudio is installed (via NuGet).

Updated the audioPath to point to the valid .wav file.

Run the project.

## Dependencies
[.NET Framework/Core]

NAudio – For audio playback

## Notes
Console colors are used for a better UI experience.

The bot will only answer to limited pre-programmed questions.


