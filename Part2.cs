using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberSecurityChatbotGUI
{
    public static class Part2
    {
        public static string username = null;

        private static List<cyberTask> tasks = new List<cyberTask>();
        private static List<string> activityLog = new List<string>();
        private static List<string> worriedKeywords = new List<string> { "worried", "concerned", "anxious", "scared" };
        private static List<string> curiousKeywords = new List<string> { "curious", "interested", "want to know" };
        private static List<string> frustratedKeywords = new List<string> { "frustrated", "annoyed", "upset", "confused" };

        private static bool quizActive = false;
        private static int currentQuestionIndex = 0;
        private static int quizScore = 0;

        private static readonly List<QuizQuestion> quizQuestions = new List<QuizQuestion>
        {
            new QuizQuestion("What does 'phishing' typically involve?",
                new[] { "A) Fishing online for viruses", "B) Tricking users into giving sensitive info", "C) Encrypting files for ransom", "D) Deleting files from a system" },
                "B", "Phishing is a type of cyberattack that tricks users into providing sensitive information like passwords."),
            new QuizQuestion("True or False: Using '123456' as a password is secure.", null,
                "False", "'123456' is one of the most commonly hacked passwords. Always use strong, unique ones."),
            new QuizQuestion("Which of the following is NOT a strong password practice?",
                new[] { "A) Using a password manager", "B) Changing passwords regularly", "C) Sharing passwords with friends", "D) Enabling 2FA" },
                "C", "Never share your passwords, even with friends."),
            new QuizQuestion("What does 2FA stand for?",
                new[] { "A) Two-Factor Authentication", "B) Two-Firewall Access", "C) Two-Faced Attack", "D) Two-Feature Algorithm" },
                "A", "2FA adds an extra layer of security beyond just a password."),
            new QuizQuestion("True or False: Public Wi-Fi is always safe if it doesn’t ask for a password.",
                null, "False", "Public Wi-Fi can be dangerous because attackers can monitor your activity."),
            new QuizQuestion("Which is a sign of a phishing email?",
                new[] { "A) Perfect grammar", "B) From a known contact", "C) Urgent tone and spelling errors", "D) Secure HTTPS link" },
                "C", "Phishing emails often create urgency and contain grammatical errors."),
            new QuizQuestion("True or False: You should install antivirus software only after getting a virus.",
                null, "False", "Antivirus should be installed *before* you're infected."),
            new QuizQuestion("What is social engineering in cybersecurity?",
                new[] { "A) Hacking software tools", "B) People manipulating others to give up information", "C) Engineering online games", "D) Building secure websites" },
                "B", "Social engineering exploits human psychology rather than technical hacking."),
            new QuizQuestion("True or False: HTTPS websites are safer than HTTP ones.",
                null, "True", "HTTPS encrypts the data between your browser and the website."),
            new QuizQuestion("What should you do after realizing you clicked a suspicious link?",
                new[] { "A) Ignore it", "B) Restart your device", "C) Run antivirus and change passwords", "D) Delete the email only" },
                "C", "Immediately scan your device and change your passwords.")
        };

        public static string ProcessInput(string input)
        {
            input = input.ToLower();

            if (string.IsNullOrEmpty(username))
            {
                username = input;
                return $"Hello {username}! You can now ask about cybersecurity, manage tasks, or type 'start quiz' to test your knowledge.";
            }

            if (quizActive)
            {
                return HandleQuizAnswer(input);
            }

            if (input.Contains("activity log") || input.Contains("what have you done"))
            {
                return ShowActivityLog();
            }

            if (input.Contains("start quiz") || input.Contains("quiz") || input.Contains("test my knowledge") || input.Contains("quiz me"))
            {
                quizActive = true;
                currentQuestionIndex = 0;
                quizScore = 0;
                AddToActivityLog("Quiz started.");
                return AskNextQuizQuestion();
            }

            if (input.Contains("remind me") || input.Contains("add task") || input.Contains("create task") || input.Contains("add a task to") || int.TryParse(input.Trim(), out _))
            {
                if (int.TryParse(input.Trim(), out int taskNum))
                {
                    return AddPredefinedTask(taskNum);
                }
                if (input.Contains("password")) return AddPredefinedTask(1);
                if (input.Contains("two-factor") || input.Contains("2fa")) return AddPredefinedTask(2);
                if (input.Contains("privacy")) return AddPredefinedTask(3);
                if (input.Contains("antivirus")) return AddPredefinedTask(4);
                if (input.Contains("backup")) return AddPredefinedTask(5);

                return BeginTaskSelection();
            }

            if (input.Contains("view tasks") || input.Contains("show my tasks") || input.Contains("list tasks"))
                return ViewTasks();

            if (input.Contains("delete task"))
                return DeleteTask(input);

            if (input.Contains("complete task"))
                return CompleteTask(input);

            if (input == "yes" || input.Contains("set reminder"))
            {
                return "Please enter the reminder date in yyyy-MM-dd format.";
            }

            if (DateTime.TryParse(input, out DateTime reminderDate))
            {
                return SetReminder(input);
            }

            if (input.Contains("password") && input.Contains("remind"))
                return AddPredefinedTask(1);

            string sentiment = DetectSentiment(input);
            if (!string.IsNullOrEmpty(sentiment)) return SentimentResponse(sentiment);

            if (input.Contains("what is phishing"))
                return "Phishing is a type of attack where attackers trick you into giving up personal information. Be cautious of suspicious emails or links.";

            return "Sorry, I didn't understand. You can type: 'add task', 'view tasks', 'delete task', 'complete task', 'start quiz', or 'show activity log'.";
        }

        private static string AskNextQuizQuestion()
        {
            if (currentQuestionIndex >= quizQuestions.Count)
            {
                quizActive = false;
                string result = $"Quiz complete! You scored {quizScore}/{quizQuestions.Count}.\n";

                if (quizScore >= 9)
                    result += "You are a cybersecurity pro! 🔐";
                else if (quizScore >= 6)
                    result += "Great job! You're doing well! 💪";
                else
                    result += "Keep learning to stay safe online! 📘";

                AddToActivityLog("Quiz completed.");
                return result;
            }

            var q = quizQuestions[currentQuestionIndex];
            string question = $"Question {currentQuestionIndex + 1}: {q.Text}\n";
            if (q.Choices != null)
                question += string.Join("\n", q.Choices) + "\n";

            question += "Your answer:";
            return question;
        }

        private static string HandleQuizAnswer(string input)
        {
            var q = quizQuestions[currentQuestionIndex];
            string userAns = input.Trim().ToUpper();
            bool isCorrect = userAns == q.Answer.ToUpper();

            if (isCorrect) quizScore++;

            string feedback = isCorrect ? "Correct!" : $"Incorrect. The correct answer is: {q.Answer}";
            feedback += $"\nExplanation: {q.Explanation}\n";

            currentQuestionIndex++;
            return feedback + "\n" + AskNextQuizQuestion();
        }


        private static string BeginTaskSelection()
        {
            return "Please select a task number to add:\n" +
                   "1. Change password regularly\n" +
                   "2. Set up two-factor authentication\n" +
                   "3. Review account privacy settings\n" +
                   "4. Install antivirus software\n" +
                   "5. Backup important data\n\n" +
                   "Type the number of the task you want to add.";
        }

        public static string AddPredefinedTask(int taskNumber)
        {
            var taskList = new List<cyberTask>
            {
                new cyberTask { Title = "Change password regularly", Description = "Update your passwords every 3 months." },
                new cyberTask { Title = "Set up two-factor authentication", Description = "Enable 2FA on your important accounts." },
                new cyberTask { Title = "Review account privacy settings", Description = "Check privacy settings on social platforms." },
                new cyberTask { Title = "Install antivirus software", Description = "Protect your system from malware." },
                new cyberTask { Title = "Backup important data", Description = "Keep secure backups of your critical files." }
            };

            if (taskNumber < 1 || taskNumber > taskList.Count)
                return "Invalid task number.";

            cyberTask selectedTask = taskList[taskNumber - 1];
            tasks.Add(selectedTask);
            AddToActivityLog($"Task added: '{selectedTask.Title}'");

            return $"Task '{selectedTask.Title}' added. Do you want to set a reminder? (yes/no)";
        }

        public static string SetReminder(string input)
        {
            if (tasks.Count == 0) return "No task to set a reminder for.";

            if (DateTime.TryParse(input, out DateTime date))
            {
                tasks[tasks.Count - 1].ReminderDate = date;
                AddToActivityLog($"Reminder set for task: '{tasks[tasks.Count - 1].Title}' on {date.ToShortDateString()}");
                return $"Reminder set for {date.ToShortDateString()}!";
            }
            return "Invalid date. Please enter the date in yyyy-MM-dd format.";
        }

        private static string ViewTasks()
        {
            if (!tasks.Any())
                return "You currently have no cybersecurity tasks.";

            return string.Join("\n", tasks.Select((t, i) => $"{i + 1}. {t}"));
        }

        private static string DeleteTask(string input)
        {
            if (int.TryParse(input.Replace("delete task", "").Trim(), out int index) &&
                index > 0 && index <= tasks.Count)
            {
                string removed = tasks[index - 1].Title;
                tasks.RemoveAt(index - 1);
                AddToActivityLog($"Task deleted: '{removed}'");
                return $"Task '{removed}' deleted.";
            }
            return "Invalid task number.";
        }

        private static string CompleteTask(string input)
        {
            if (int.TryParse(input.Replace("complete task", "").Trim(), out int index) &&
                index > 0 && index <= tasks.Count)
            {
                tasks[index - 1].IsCompleted = true;
                AddToActivityLog($"Task marked as completed: '{tasks[index - 1].Title}'");
                return $"Task '{tasks[index - 1].Title}' marked as completed.";
            }
            return "Invalid task number.";
        }

        private static string DetectSentiment(string input)
        {
            if (worriedKeywords.Any(w => input.Contains(w))) return "worried";
            if (curiousKeywords.Any(w => input.Contains(w))) return "curious";
            if (frustratedKeywords.Any(w => input.Contains(w))) return "frustrated";
            return null;
        }

        private static string SentimentResponse(string sentiment)
        {
            switch (sentiment)
            {
                case "worried":
                    return "It's normal to feel worried. Cybersecurity can be scary, but you're taking the right steps by learning!";
                case "curious":
                    return "Curiosity is the first step toward digital safety. Keep asking questions!";
                case "frustrated":
                    return "Cybersecurity can be overwhelming. Don't worry — I'm here to guide you through it.";
                default:
                    return string.Empty;
            }
        }

        private static void AddToActivityLog(string action)
        {
            if (activityLog.Count >= 10) activityLog.RemoveAt(0);
            activityLog.Add($"[{DateTime.Now:HH:mm}] {action}");
        }

        private static string ShowActivityLog()
        {
            if (activityLog.Count == 0) return "Your activity log is currently empty.";
            return "Here's a summary of recent actions:\n" + string.Join("\n", activityLog);
        }
    }

    public class QuizQuestion
    {
        public string Text { get; }
        public string[] Choices { get; }
        public string Answer { get; }
        public string Explanation { get; }

        public QuizQuestion(string text, string[] choices, string answer, string explanation)
        {
            Text = text;
            Choices = choices;
            Answer = answer;
            Explanation = explanation;
        }
    }
}
