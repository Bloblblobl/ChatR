using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
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
            this.InputBox = new System.Windows.Forms.TextBox();
            this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
            this.AvatarButton = new System.Windows.Forms.Button();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.NameBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).BeginInit();
            this.SplitContainer2.Panel1.SuspendLayout();
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
            // InputBox
            // 
            this.InputBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.InputBox.Location = new System.Drawing.Point(0, 582);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(800, 20);
            this.InputBox.TabIndex = 0;
            this.InputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Input_KeyDown);
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
            this.SplitContainer2.Panel1.Controls.Add(this.AvatarButton);
            this.SplitContainer2.Panel1.Controls.Add(this.ConnectButton);
            this.SplitContainer2.Panel1.Controls.Add(this.NameBox);
            this.SplitContainer2.Size = new System.Drawing.Size(204, 602);
            this.SplitContainer2.SplitterDistance = 25;
            this.SplitContainer2.TabIndex = 0;
            // 
            // AvatarButton
            // 
            this.AvatarButton.Location = new System.Drawing.Point(4, 0);
            this.AvatarButton.Name = "AvatarButton";
            this.AvatarButton.Size = new System.Drawing.Size(24, 24);
            this.AvatarButton.TabIndex = 1;
            this.AvatarButton.UseVisualStyleBackColor = true;
            this.AvatarButton.Click += new System.EventHandler(this.AvatarButton_Click);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ConnectButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ConnectButton.Enabled = false;
            this.ConnectButton.Location = new System.Drawing.Point(132, 0);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(69, 24);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // NameBox
            // 
            this.NameBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NameBox.Location = new System.Drawing.Point(34, 2);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(92, 20);
            this.NameBox.TabIndex = 0;
            this.NameBox.TextChanged += new System.EventHandler(this.NameBox_TextChanged);
            this.NameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NameBox_KeyDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 602);
            this.Controls.Add(this.SplitContainer);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "ChatR Client";
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel1.PerformLayout();
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.SplitContainer2.Panel1.ResumeLayout(false);
            this.SplitContainer2.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).EndInit();
            this.SplitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion



        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.TextBox InputBox;

        public void OnMessage(string name, string message)
        {
            var item = new ListViewItem(string.Format("[{0}]: {1}", name, message), name);
            _messagesListView.Items.Append(item);
        }

        public void OnJoin(string user)
        {
            AddUserToUsersList(user);

            var name = user.Split(',')[0];
            var item = new ListViewItem(string.Format("User [{0}] has joined the ChatRoom", name), name);
            item.ForeColor = Color.Green;
            _messagesListView.Items.Append(item);
        }

        private void AddUserToUsersList(string user)
        {
            var tokens = user.Split(',');
            var name = tokens[0];
            var url = tokens[1];

            var image = GetAvatarImage(url);

            // add the image indexed by username to the imagelist
            _avatars.Images.Add(name, image);

            // add the new user to the users list [Key, String, ImageKey]
            _usersListView.Items.Append(name, name, name);
        }

        public void OnLeave(string name)
        {
            var item = new ListViewItem(string.Format("User [{0}] has left the ChatRoom", name), name);
            item.ForeColor = Color.Red;
            _messagesListView.Items.Append(item);
            _usersListView.Items.RemoveByKey(name);
        }

        public void OnList(string[] users)
        {
            foreach (string u in users)
            {
                AddUserToUsersList(u);
            }
        }

        private System.Windows.Forms.SplitContainer SplitContainer2;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TextBox NameBox;
        private Button AvatarButton;


       
    }
}

