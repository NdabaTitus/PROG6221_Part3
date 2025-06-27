namespace CyberSecurityChatbotGUI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.RichTextBox rtbChatLog;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtUserInput;
        private System.Windows.Forms.Button btnSend;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.rtbChatLog = new System.Windows.Forms.RichTextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtUserInput = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(180, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(304, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Cybersecurity Chatbot";
            // 
            // rtbChatLog
            // 
            this.rtbChatLog.Location = new System.Drawing.Point(12, 50);
            this.rtbChatLog.Name = "rtbChatLog";
            this.rtbChatLog.ReadOnly = true;
            this.rtbChatLog.Size = new System.Drawing.Size(560, 320);
            this.rtbChatLog.TabIndex = 1;
            this.rtbChatLog.Text = "";
            this.rtbChatLog.TextChanged += new System.EventHandler(this.rtbChatLog_TextChanged);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(12, 390);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(89, 15);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Your Message:";
            // 
            // txtUserInput
            // 
            this.txtUserInput.Location = new System.Drawing.Point(120, 385);
            this.txtUserInput.Name = "txtUserInput";
            this.txtUserInput.Size = new System.Drawing.Size(360, 20);
            this.txtUserInput.TabIndex = 3;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(490, 384);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(80, 25);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(584, 421);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.rtbChatLog);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtUserInput);
            this.Controls.Add(this.btnSend);
            this.Name = "Form1";
            this.Text = "Cybersecurity Chatbot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
