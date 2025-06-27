using System;

namespace CyberSecurityChatbotGUI
{
    public class cyberTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; } = false;

        public override string ToString()
        {
            string status = IsCompleted ? "[COMPLETED]" : "[PENDING]";
            string reminder = ReminderDate.HasValue ? $" (Reminder: {ReminderDate.Value.ToShortDateString()})" : "";
            return $"{status} {Title}: {Description}{reminder}";
        }
    }
}

