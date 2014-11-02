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
    using System.IO;
    using System.Net;
    using System.Collections.Specialized;
    using System.Web.Script.Serialization;
    using settings = Properties.Settings;

    public partial class MainForm : Form
    {
        ChatClient _client = null;
        ListView _usersListView = null;
        ListView _messagesListView = null;
        ImageList _avatars = null;
        ToolTip _nameToolTip = new ToolTip();

        public MainForm()
        {
            InitializeComponent();

            // Set up the ToolTip

            _avatars = new ImageList();

            _usersListView = CreateListView("Users");
            this.SplitContainer2.Panel2.Controls.Add(_usersListView);

            _messagesListView = CreateListView("Messages");
            this.SplitContainer.Panel1.Controls.Add(_messagesListView);
            _messagesListView.Resize += new System.EventHandler(this._messagesListView_Resize);

            _client = new ChatClient();

            var nickname = Properties.Settings.Default.Nickname;

            UpdateSelectorButtonImage();

            InputBox.Enabled = false;
            NameBox.Enabled = true;
            NameBox.Text = nickname;
            NameBox.Focus();

        }

        private void UpdateSelectorButtonImage()
        {
            var image = AvatarSelector.GetAvatarImage(Properties.Settings.Default.AvatarURL);
            var avatar = image.GetThumbnailImage(AvatarButton.Width - 5, AvatarButton.Height - 5, null, IntPtr.Zero);
            AvatarButton.Image = avatar;
        }

        

        private ListView CreateListView(string title)
        {
            // Create a new ListView control.
            var listView = new ListView();
            listView.Dock = DockStyle.Fill;

            // Set the view to show details.
            listView.View = View.Details;
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
            //listView.Sorting = SortOrder.None;

            // Width of -2 indicates auto-size.
            listView.Columns.Add(title, -2, HorizontalAlignment.Left);

            //Assign the ImageList objects to the ListView.
            listView.SmallImageList = _avatars;

            listView.Items.Add(new ListViewItem());

            // Add the ListView to the control collection. 
            return listView;
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            
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
            if ((e.KeyCode == Keys.Oemcomma) || (e.KeyCode == Keys.Space))
            {
                e.SuppressKeyPress = true;
                _nameToolTip.Show("Space and Comma are forbidden", NameBox, 1000);
                return;
            }
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
                var url = settings.Default.AvatarURL;
                _client.Connect(this, this);
                _client.Join(NameBox.Text, url);
                ConnectButton.Enabled = false;
                NameBox.Enabled = false;
                InputBox.Enabled = true;
                InputBox.Focus();
            }
            catch (Exception e)
            {
                _messagesListView.Items.Append("[ERROR]: " + e.Message);
            }
        }

        private void _messagesListView_Resize(object sender, EventArgs e)
        {
            SizeLastColumn((ListView)sender);
        }

        private void _messagesListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SizeLastColumn(ListView lv)
        {
            lv.Columns[lv.Columns.Count - 1].Width = -2;
        }

        private void AvatarButton_Click(object sender, EventArgs e)
        {
            var dialog = new AvatarSelector();
            var result = dialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            UpdateSelectorButtonImage();
        }
    }
}
