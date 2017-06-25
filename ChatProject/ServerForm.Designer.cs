namespace ChatProject
{
    partial class ServerForm
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
            this.tbLogging = new System.Windows.Forms.TextBox();
            this.listUsers = new System.Windows.Forms.ListBox();
            this.lbLogging = new System.Windows.Forms.Label();
            this.lbUsers = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.btnShutdown = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbLogging
            // 
            this.tbLogging.Location = new System.Drawing.Point(12, 91);
            this.tbLogging.Multiline = true;
            this.tbLogging.Name = "tbLogging";
            this.tbLogging.Size = new System.Drawing.Size(210, 485);
            this.tbLogging.TabIndex = 0;
            // 
            // listUsers
            // 
            this.listUsers.FormattingEnabled = true;
            this.listUsers.Location = new System.Drawing.Point(287, 91);
            this.listUsers.Name = "listUsers";
            this.listUsers.Size = new System.Drawing.Size(157, 485);
            this.listUsers.TabIndex = 1;
            // 
            // lbLogging
            // 
            this.lbLogging.AutoSize = true;
            this.lbLogging.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.lbLogging.Location = new System.Drawing.Point(77, 64);
            this.lbLogging.Name = "lbLogging";
            this.lbLogging.Size = new System.Drawing.Size(79, 24);
            this.lbLogging.TabIndex = 2;
            this.lbLogging.Text = "Logging";
            // 
            // lbUsers
            // 
            this.lbUsers.AutoSize = true;
            this.lbUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.lbUsers.Location = new System.Drawing.Point(288, 64);
            this.lbUsers.Name = "lbUsers";
            this.lbUsers.Size = new System.Drawing.Size(156, 24);
            this.lbUsers.TabIndex = 3;
            this.lbUsers.Text = "Connected Users";
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lbTitle.Location = new System.Drawing.Point(113, 9);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(236, 31);
            this.lbTitle.TabIndex = 4;
            this.lbTitle.Text = "Server is started...";
            // 
            // btnShutdown
            // 
            this.btnShutdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btnShutdown.Location = new System.Drawing.Point(80, 583);
            this.btnShutdown.Name = "btnShutdown";
            this.btnShutdown.Size = new System.Drawing.Size(291, 43);
            this.btnShutdown.TabIndex = 5;
            this.btnShutdown.Text = "Shutdown";
            this.btnShutdown.UseVisualStyleBackColor = true;
            this.btnShutdown.Click += new System.EventHandler(this.btnShutdown_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 638);
            this.Controls.Add(this.btnShutdown);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.lbUsers);
            this.Controls.Add(this.lbLogging);
            this.Controls.Add(this.listUsers);
            this.Controls.Add(this.tbLogging);
            this.Name = "ServerForm";
            this.Text = "ServerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbLogging;
        private System.Windows.Forms.ListBox listUsers;
        private System.Windows.Forms.Label lbLogging;
        private System.Windows.Forms.Label lbUsers;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Button btnShutdown;
    }
}