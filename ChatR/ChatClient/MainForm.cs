using System;
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

        public MainForm()
        {
            InitializeComponent();
            CreateUsersListView();
            _client = new ChatClient();

            var nickname = Properties.Settings.Default.Nickname;

            InputBox.Enabled = false;
            NameBox.Enabled = true;
            NameBox.Text = nickname;
            NameBox.Focus();

        }

        private void CreateUsersListView()
        {
            // Create a new ListView control.
            _usersListView = new ListView();
            _usersListView.Bounds = new Rectangle(new Point(10, 10), new Size(300, 200));
            _usersListView.Dock = DockStyle.Fill;

            // Set the view to show details.
            _usersListView.View = View.List;
            // Allow the user to edit item text.
            _usersListView.LabelEdit = false;
            // Allow the user to rearrange columns.
            _usersListView.AllowColumnReorder = true;
            // Display check boxes.
            _usersListView.CheckBoxes = false;
            // Select the item and subitems when selection is made.
            _usersListView.FullRowSelect = true;
            // Display grid lines.
            _usersListView.GridLines = false;
            // Sort the items in the list in ascending order.
            _usersListView.Sorting = SortOrder.Ascending;

            // Create columns for the items
            // Width of -2 indicates auto-size.
            _usersListView.Columns.Add("Users", -2, HorizontalAlignment.Left);

            // Create two ImageList objects.
            ImageList imageListLarge = new ImageList();

            // Initialize the ImageList objects with bitmaps.
            imageListLarge.Images.Add(GetAvatarBitmap());

            //Assign the ImageList objects to the ListView.
            _usersListView.SmallImageList = imageListLarge;

            // Add the ListView to the control collection. 
            this.SplitContainer2.Panel2.Controls.Add(_usersListView);
        }

        private static Image GetAvatarBitmap()
        {
            var filePath = @"Avatars/" + Properties.Settings.Default.Avatar;
            //if (File.Exists(filePath))
            //{
            //    return Bitmap.FromFile(filePath);
            //}
            // fetch avatar from URL
            var url = settings.Default.AvatarURL;
            using (var w = new WebClient())
            {
                w.Headers.Add("Authorization", ImagePoster.IMGUR_CLIENT_ID);
                var json = w.DownloadString(url);
                var jss = new JavaScriptSerializer();
                dynamic data = jss.DeserializeObject(json);
                var link = data["data"]["link"];
                w.DownloadFile(link, settings.Default.Avatar);
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
                _client.Join(NameBox.Text);
                ConnectButton.Enabled = false;
                NameBox.Enabled = false;
                InputBox.Enabled = true;
                InputBox.Focus();
            }
            catch (Exception e)
            {
                Messages.Items.Add("[ERROR]: " + e.Message);
            }
        }
    }
}
