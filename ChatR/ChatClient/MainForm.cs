﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChatR.ChatClient
{
    using settings = Properties.Settings;
    using System.IO;
    using System.Net;
    using System.Collections.Specialized;
    using System.Web.Script.Serialization;

    public partial class MainForm : Form
    {
        ChatClient _client = null;
        ListView _usersListView = null;
        ListView _messagesListView = null;
        ImageList _avatars = null;
        Dictionary<string, int> _userAvatars = null;

        public MainForm()
        {
            InitializeComponent();

            _avatars = new ImageList();
            _userAvatars = new Dictionary<string, int>();

            _usersListView = CreateListView();
            this.SplitContainer2.Panel2.Controls.Add(_usersListView);

            _messagesListView = CreateListView();
            this.SplitContainer.Panel1.Controls.Add(_messagesListView);

            _client = new ChatClient();

            var nickname = Properties.Settings.Default.Nickname;

            InputBox.Enabled = false;
            NameBox.Enabled = true;
            NameBox.Text = nickname;
            NameBox.Focus();

        }

        public static string CreateRandomName(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        private ListView CreateListView()
        {
            // Create a new ListView control.
            var listView = new ListView();
            listView.Dock = DockStyle.Fill;

            // Set the view to show details.
            listView.View = View.List;
            // Allow the user to edit item text.
            listView.LabelEdit = false;
            // Allow the user to rearrange columns.
            listView.AllowColumnReorder = true;
            // Display check boxes.
            listView.CheckBoxes = false;
            // Select the item and subitems when selection is made.
            listView.FullRowSelect = true;
            // Display grid lines.
            listView.GridLines = false;
            // Sort the items in the list in ascending order.
            listView.Sorting = SortOrder.Ascending;

            // Create columns for the items
            // Width of -2 indicates auto-size.
            listView.Columns.Add("The Column", -2, HorizontalAlignment.Left);

            // Create two ImageList objects.
            ImageList imageList = new ImageList();

            // Initialize the ImageList objects with bitmaps.
            imageList.Images.Add(GetAvatarBitmap("j"));

            //Assign the ImageList objects to the ListView.
            listView.SmallImageList = imageList;

            // Add the ListView to the control collection. 
            return listView;
        }

        private static Image GetAvatarBitmap(string url)
        {
            var filePath = @"Avatars/" + Properties.Settings.Default.Avatar;
            if (File.Exists(filePath))
            {
                return Bitmap.FromFile(filePath);
            }

            // fetch avatar from URL
            using (var w = new WebClient())
            {
                w.Headers.Add("Authorization", ImagePoster.IMGUR_CLIENT_ID);
                var json = w.DownloadString(url);
                var jss = new JavaScriptSerializer();
                dynamic data = jss.DeserializeObject(json);
                var link = data["data"]["link"];
                var filename = CreateRandomName(5) + ".png";
                w.DownloadFile(link, filename);
            }
            return Bitmap.FromFile(settings.Default.Avatar);
        }

        private void SplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            // nothing here
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void NameBox_TextChanged(object sender, EventArgs e)
        {
            ConnectButton.Enabled = NameBox.Text.Length > 0;
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
            {
                e.Handled = e.SuppressKeyPress = true;
                if (e.KeyCode == Keys.Enter)
                {
                    var t = (TextBox)sender;
                    string text = t.Text;
                    _client.Send(text);
                    InputBox.Text = "";
                }
            }
        }

        private void NameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
            {
                e.Handled = e.SuppressKeyPress = true;
                if (e.KeyCode == Keys.Enter)
                {
                    Connect();
                }
            }
        }

        private void Connect()
        {
            try
            {
                if (NameBox.Text != settings.Default.Nickname)
                {
                    settings.Default.Nickname = NameBox.Text;
                    settings.Default.Save();
                }
                var url = ImagePoster.PostToImgur("Avatars/" + settings.Default.Avatar);
                _client.Connect(this, this);
                _client.Join(NameBox.Text, url);
                ConnectButton.Enabled = false;
                NameBox.Enabled = false;
                InputBox.Enabled = true;
                InputBox.Focus();
            }
            catch (Exception e)
            {
                _messagesListView.Items.Add("[ERROR]: " + e.Message);
            }
        }

        private void _messagesListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
