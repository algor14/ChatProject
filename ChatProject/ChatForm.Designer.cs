namespace ChatProject
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSend = new System.Windows.Forms.Button();
            this.tbChat = new System.Windows.Forms.TextBox();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.listUsers = new System.Windows.Forms.ListBox();
            this.btnPrivateChat = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btnSend.Location = new System.Drawing.Point(520, 404);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(148, 61);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbChat
            // 
            this.tbChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.tbChat.Location = new System.Drawing.Point(12, 12);
            this.tbChat.Multiline = true;
            this.tbChat.Name = "tbChat";
            this.tbChat.ReadOnly = true;
            this.tbChat.Size = new System.Drawing.Size(502, 386);
            this.tbChat.TabIndex = 1;
            // 
            // tbMessage
            // 
            this.tbMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.tbMessage.Location = new System.Drawing.Point(13, 404);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(501, 61);
            this.tbMessage.TabIndex = 2;
            // 
            // listUsers
            // 
            this.listUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.listUsers.FormattingEnabled = true;
            this.listUsers.ItemHeight = 16;
            this.listUsers.Location = new System.Drawing.Point(522, 95);
            this.listUsers.Name = "listUsers";
            this.listUsers.Size = new System.Drawing.Size(146, 292);
            this.listUsers.TabIndex = 3;
            // 
            // btnPrivateChat
            // 
            this.btnPrivateChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnPrivateChat.Location = new System.Drawing.Point(520, 42);
            this.btnPrivateChat.Name = "btnPrivateChat";
            this.btnPrivateChat.Size = new System.Drawing.Size(146, 47);
            this.btnPrivateChat.TabIndex = 4;
            this.btnPrivateChat.Text = "Start Private Chat";
            this.btnPrivateChat.UseVisualStyleBackColor = true;
            this.btnPrivateChat.Click += new System.EventHandler(this.btnPrivateChat_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(572, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(96, 24);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit Chat";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 477);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrivateChat);
            this.Controls.Add(this.listUsers);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.tbChat);
            this.Controls.Add(this.btnSend);
            this.Name = "ChatForm";
            this.Text = "MainChatForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbChat;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.ListBox listUsers;
        private System.Windows.Forms.Button btnPrivateChat;
        private System.Windows.Forms.Button btnExit;
    }
}