using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace CyberSecurityChatbotGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PlayGreetingSound();
            DisplayAsciiArt();
            rtbChatLog.AppendText("Welcome to SecureBuddy - Your Cybersecurity Assistant!\n");
            rtbChatLog.AppendText("Please type your name to begin:\n\n");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string userInput = txtUserInput.Text.Trim();

            if (!string.IsNullOrEmpty(userInput))
            {
                rtbChatLog.AppendText($"You: {userInput}\n");

                if (string.IsNullOrEmpty(Part2.username))
                {
                    Part2.username = userInput;
                    rtbChatLog.AppendText($"Bot: Hello {Part2.username}! You can now ask about cybersecurity or manage your tasks.\n\n");
                }
                else
                {
                    string response = Part2.ProcessInput(userInput);
                    rtbChatLog.AppendText($"Bot: {response}\n\n");
                }

                txtUserInput.Clear();
                txtUserInput.Focus();
            }
        }

        private void PlayGreetingSound()
        {
            string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "greeting.wav");

            if (File.Exists(audioPath))
            {
                try
                {
                    using (SoundPlayer player = new SoundPlayer(audioPath))
                    {
                        player.Play();
                    }
                }
                catch
                {
                    MessageBox.Show("Could not play greeting sound.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void DisplayAsciiArt()
        {
            string artPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ascii_art..txt");

            if (File.Exists(artPath))
            {
                string ascii = File.ReadAllText(artPath);
                rtbChatLog.AppendText(ascii + "\n\n");
            }
            else
            {
                rtbChatLog.AppendText("Welcome to SecureBuddy ChatBot!\n\n");
            }
        }
            private void rtbChatLog_TextChanged(object sender, EventArgs e)
        {
        }
    }
}

