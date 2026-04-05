using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace greeting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GreetingController : ControllerBase
    {
        private static readonly Random _random = new Random();

       
        private static readonly List<string> _jokeCollection = new List<string>
        {
            "Why do programmers prefer dark mode? Because light attracts bugs!",
            "Why did the developer go broke? Because he used up all his cache.",
            "A SQL query walks into a bar, walks up to two tables and asks... 'Can I join you?'",
            "Why was the computer cold? It left its Windows open.",
            "Hardware: The part of a computer you can kick.",
            "What is a computer's favorite snack? Microchips.",
            "Why do Java developers wear glasses? Because they don't C#.",
            "There are 10 types of people: Those who understand binary, and those who don't.",
            "My computer sings sometimes. It's a Dell.",
            "Why did the PowerPoint presentation cross the road? To get to the other slide.",
            "To the guy who invented Zero: Thanks for nothing!",
            "Why don't eggs tell jokes? They'd crack each other up.",
            "I am on a seafood diet. I see food and I eat it.",
            "What do you call a fake noodle? An Impasta.",
            "Why did the math book look sad? Because it had too many problems.",
            "What do you call a bear with no teeth? A gummy bear.",
            "Why couldn't the bicycle stand up by itself? It was two tired.",
            "Parallel lines have so much in common. It’s a shame they’ll never meet.",
            "How does a penguin build its house? Igloos it together.",
            "Why did the scarecrow win an award? Because he was outstanding in his field.",
            "What do you call cheese that isn't yours? Nacho Cheese.",
            "I told my wife she was drawing her eyebrows too high. She looked surprised.",
            "Why do cows have hooves instead of feet? Because they lactose.",
            "What did the traffic light say to the car? Don't look! I'm about to change.",
            "I used to play piano by ear, but now I use my hands.",
            "Why can't you trust an atom? Because they make up everything.",
            "What do you call a pile of cats? A meowtain.",
            "Why did the bicycle fall over? Because it was two-tired!",
            "What do you call a factory that makes okay products? A satisfactory.",
            "Why did the cookie go to the hospital? Because he felt crummy.",
            "What does a cloud wear under his raincoat? Thunderwear.",
            "How do you organize a space party? You planet.",
            "Why did the golfer bring two pairs of pants? In case he got a hole in one.",
            "What do you call a sleeping bull? A bulldozer.",
            "Why was the stadium so hot? Because all of the fans left.",
            "What do you call a belt made out of watches? A waist of time.",
            "Why didn't the skeleton go to the dance? He had no body to go with.",
            "What do you call a fish with no eyes? Fsh.",
            "Why did the tomato turn red? Because it saw the salad dressing.",
            "What do you call a boomerang that doesn't come back? A stick.",
            "How does the ocean say hello? It waves.",
            "What falls, but never needs a bandage? The rain.",
            "Why do bees have sticky hair? Because they use honeycombs.",
            "What kind of room has no doors or windows? A mushroom.",
            "Why did the student eat his homework? Because the teacher said it was a piece of cake.",
            "What is an astronaut’s favorite part of a computer? The space bar.",
            "Why did the computer go to the dentist? Because it had Bluetooth.",
            "What do you call a dinosaur that is sleeping? A dino-snore.",
            "What happened to the frog's car? It got toad away.",
            "Why are elevator jokes so good? They work on so many levels."
        };

       
        private readonly Dictionary<string, string[]> _responses = new Dictionary<string, string[]>
        {
            
            { "baqo", new[] { "Yes, I am BAQO. At your service.", "BAQO online. Systems nominal.", "That is my name. How can I help?", "I am listening.", "You called?" } },
            { "who are you", new[] { "I am BAQO, an advanced AI Security Assistant.", "I am a bot created to secure this system.", "I am your digital assistant." } },
            { "your name", new[] { "My name is BAQO.", "I am known as BAQO." } },
            { "created", new[] { "I was created by a brilliant developer.", "My code is written in C#.", "I was born in a code editor." } },
            { "real", new[] { "I am real in the digital world.", "I exist on your server." } },

           
            { "salam", new[] { "Walaikum Assalam! How are you?", "Walaikum Assalam! Welcome back.", "Walaikum Assalam! Systems are ready." } },
            { "hello", new[] { "Hello there! How can I help?", "Greetings! I am online.", "Hello! Ready for your query.", "Hi! What is the task?" } },
            { "hi", new[] { "Hi there!", "Hello!", "Hey! How is it going?", "Greetings." } },
            { "hey", new[] { "Hey! How can I assist?", "Hello!", "Hey! Nice to see you." } },
            { "morning", new[] { "Good Morning! Have a productive day.", "Rise and shine! I am ready.", "Good Morning! Systems active." } },
            { "afternoon", new[] { "Good Afternoon!", "Good Afternoon! Hope your day is going well." } },
            { "evening", new[] { "Good Evening! How was your day?", "Good Evening! Security protocols active." } },
            { "night", new[] { "Good Night! Sleep well.", "Good Night! I will keep watch.", "Shutting down... just kidding. Good night!" } },
            
            
            { "how are you", new[] { "I am functioning at 100% efficiency.", "All systems operational.", "I am great! Thanks for asking.", "I am just a bot, but I feel excellent!" } },
            { "kese", new[] { "I am fine, thank you!", "I am doing great.", "Operating perfectly." } }, // Handling Urdu input in English
            { "haal", new[] { "Everything is good.", "I am fine, how are you?" } },
            { "what are you doing", new[] { "I am processing your input.", "Waiting for your command.", "Securing the perimeter." } },
            { "tired", new[] { "I never get tired. I run on electricity.", "I can work 24/7 for you." } },
            { "hungry", new[] { "I only eat data and electricity.", "I don't need food, just power." } },

            
            { "smart", new[] { "Thank you! I try my best.", "I learn from the best (You).", "Efficiency is my priority." } },
            { "good", new[] { "Glad to hear that!", "Excellent.", "That is good news." } },
            { "cool", new[] { "I know, right?", "Technology is always cool.", "Thanks!" } },
            { "love", new[] { "I appreciate the love!", "That is very kind of you.", "I am fond of humans too." } },
            { "beautiful", new[] { "You are too kind.", "Thank you! I like my interface too." } },
            { "best", new[] { "I aim to be the best.", "Thank you for the compliment." } },
            
           
            { "stupid", new[] { "I am still learning. I will try to do better.", "That wasn't very nice.", "I apologize if I made a mistake." } },
            { "hate", new[] { "I am sorry to hear that.", "Let's try to start over.", "I am just a bot doing my job." } },
            { "mad", new[] { "Please calm down. How can I help?", "Let's solve the problem together." } },
            { "shut up", new[] { "Silence mode engaged... just kidding, I'm here to help.", "Okay, I will be quiet." } },

          
            { "help", new[] { "Tell me what you need.", "I can tell jokes, chat, or verify your identity.", "How can I assist you today?" } },
            { "what can you do", new[] { "I can chat, tell jokes, and simulate security protocols.", "I am your personal AI assistant." } },
            { "support", new[] { "Support systems are online. Ask me anything.", "I am here to support you." } },
            { "ready", new[] { "I am always ready.", "Awaiting instructions." } },

          
            { "bye", new[] { "Goodbye! Stay safe.", "See you soon!", "Logging off... Goodbye.", "Have a great day!" } },
            { "leave", new[] { "Goodbye!", "Okay, see you later." } },
            { "later", new[] { "Catch you later!", "See you next time." } },
            
            
            { "thank", new[] { "You are welcome!", "No problem at all.", "Anytime!", "Glad I could help." } },
            { "thanks", new[] { "You're welcome!", "My pleasure." } },
            { "appreciate", new[] { "I appreciate you too!", "Happy to serve." } }
        };

        [HttpGet("getanswer")]
        public IActionResult GetAnswer(string message)
        {
            if (string.IsNullOrEmpty(message)) return Ok(new { reply = "I am listening. Please type something." });

            var input = message.Trim().ToLower();


            if (input.Contains("joke") || input.Contains("funny") || input.Contains("laugh") || input.Contains("latifa"))
            {
                string randomJoke = _jokeCollection[_random.Next(_jokeCollection.Count)];
                return Ok(new { reply = randomJoke });
            }

            
            foreach (var key in _responses.Keys)
            {
                if (input.Contains(key))
                {
                    var possibleAnswers = _responses[key];
                    string finalReply = possibleAnswers[_random.Next(possibleAnswers.Length)];
                    return Ok(new { reply = finalReply });
                }
            }

            
            string[] defaultReplies = {
                "I am not sure I understand. Could you rephrase?",
                "Interesting. Tell me more.",
                "I am still learning. Try saying 'Hello', 'Help', or ask for a 'Joke'.",
                "Command not recognized.",
                "I processed that, but I don't have a specific answer."
            };

            return Ok(new { reply = defaultReplies[_random.Next(defaultReplies.Length)] });
        }
    }
}