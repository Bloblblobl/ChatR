namespace ChatR.ChatClient
{
    partial class MainForm : IChatREvents
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Messages = new System.Windows.Forms.ListBox();
            this.Input = new System.Windows.Forms.TextBox();
            this.Users = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Messages);
            this.splitContainer1.Panel1.Controls.Add(this.Input);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Users);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 602);
            this.splitContainer1.SplitterDistance = 800;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // Messages
            // 
            this.Messages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Messages.FormattingEnabled = true;
            this.Messages.Location = new System.Drawing.Point(0, 0);
            this.Messages.Name = "Messages";
            this.Messages.Size = new System.Drawing.Size(800, 582);
            this.Messages.TabIndex = 1;
            // 
            // Input
            // 
            this.Input.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Input.Location = new System.Drawing.Point(0, 582);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(800, 20);
            this.Input.TabIndex = 0;
            this.Input.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Input_KeyUp);
            // 
            // Users
            // 
            this.Users.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Users.FormattingEnabled = true;
            this.Users.Location = new System.Drawing.Point(0, 0);
            this.Users.Name = "Users";
            this.Users.Size = new System.Drawing.Size(204, 602);
            this.Users.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 602);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "ChatR Client";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox Messages;
        private System.Windows.Forms.TextBox Input;
        private System.Windows.Forms.ListBox Users;

        public void OnMessage(string name, string message)
        {
            Messages.Items.Add(string.Format("[{0}]: {1}", name, message));
        }

        public void OnJoin(string name)
        {
            throw new System.NotImplementedException();
        }

        public void OnLeave(string name)
        {
            throw new System.NotImplementedException();
        }

        public void OnList(string[] names)
        {
            throw new System.NotImplementedException();
        }
    }
}

