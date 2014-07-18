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
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.Messages = new System.Windows.Forms.ListBox();
            this.InputBox = new System.Windows.Forms.TextBox();
            this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.Users = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).BeginInit();
            this.SplitContainer2.Panel1.SuspendLayout();
            this.SplitContainer2.Panel2.SuspendLayout();
            this.SplitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.Messages);
            this.SplitContainer.Panel1.Controls.Add(this.InputBox);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.SplitContainer2);
            this.SplitContainer.Size = new System.Drawing.Size(1008, 602);
            this.SplitContainer.SplitterDistance = 800;
            this.SplitContainer.TabIndex = 0;
            this.SplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer_SplitterMoved);
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
            // InputBox
            // 
            this.InputBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.InputBox.Location = new System.Drawing.Point(0, 582);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(800, 20);
            this.InputBox.TabIndex = 0;
            this.InputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Input_KeyDown);
            this.InputBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Input_KeyUp);
            // 
            // SplitContainer2
            // 
            this.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer2.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer2.Name = "SplitContainer2";
            this.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer2.Panel1
            // 
            this.SplitContainer2.Panel1.Controls.Add(this.ConnectButton);
            this.SplitContainer2.Panel1.Controls.Add(this.NameBox);
            // 
            // SplitContainer2.Panel2
            // 
            this.SplitContainer2.Panel2.Controls.Add(this.Users);
            this.SplitContainer2.Size = new System.Drawing.Size(204, 602);
            this.SplitContainer2.SplitterDistance = 25;
            this.SplitContainer2.TabIndex = 0;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.ConnectButton.Enabled = false;
            this.ConnectButton.Location = new System.Drawing.Point(108, 0);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(96, 25);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(3, 2);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(99, 20);
            this.NameBox.TabIndex = 0;
            this.NameBox.TextChanged += new System.EventHandler(this.NameBox_TextChanged);
            // 
            // Users
            // 
            this.Users.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Users.FormattingEnabled = true;
            this.Users.Location = new System.Drawing.Point(0, 0);
            this.Users.Name = "Users";
            this.Users.Size = new System.Drawing.Size(204, 573);
            this.Users.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 602);
            this.Controls.Add(this.SplitContainer);
            this.Name = "MainForm";
            this.Text = "ChatR Client";
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel1.PerformLayout();
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.SplitContainer2.Panel1.ResumeLayout(false);
            this.SplitContainer2.Panel1.PerformLayout();
            this.SplitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).EndInit();
            this.SplitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.ListBox Messages;
        private System.Windows.Forms.TextBox InputBox;

        public void OnMessage(string name, string message)
        {
            Messages.Items.Add(string.Format("[{0}]: {1}", name, message));
        }

        public void OnJoin(string name)
        {
            Messages.Items.Add(string.Format("User [{0}] has joined the ChatRoom", name));
            Users.Items.Add(name);
        }

        public void OnLeave(string name)
        {
            Messages.Items.Add(string.Format("User [{0}] has left the ChatRoom", name));
            Users.Items.Remove(name);
        }

        public void OnList(string[] names)
        {
            Users.Items.AddRange(names);
        }

        private System.Windows.Forms.SplitContainer SplitContainer2;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.ListBox Users;
    }
}

