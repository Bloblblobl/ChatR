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
    public partial class MainForm : Form
    {
        ChatClient _client = null;
        ListView _usersListView = null;

        public MainForm()
        {
            InitializeComponent();
            CreateUsersListView();
            _client = new ChatClient();
            InputBox.Enabled = false;
            NameBox.Enabled = true;
            NameBox.Location = new Point(NameBox.Location.X, NameBox.Location.Y + 2);
            NameBox.Focus();
        }

        //private void CreateUsersListView()
        //{
        //    // Create a new ListView control.
        //    ListView listView1 = new ListView();
        //    listView1.Bounds = new Rectangle(new Point(10, 10), new Size(300, 200));

        //    // Set the view to show details.
        //    listView1.View = View.Details;
        //    // Allow the user to edit item text.
        //    listView1.LabelEdit = true;
        //    // Allow the user to rearrange columns.
        //    listView1.AllowColumnReorder = true;
        //    // Display check boxes.
        //    listView1.CheckBoxes = true;
        //    // Select the item and subitems when selection is made.
        //    listView1.FullRowSelect = true;
        //    // Display grid lines.
        //    listView1.GridLines = true;
        //    // Sort the items in the list in ascending order.
        //    listView1.Sorting = SortOrder.Ascending;

        //    // Create three items and three sets of subitems for each item.
        //    ListViewItem item1 = new ListViewItem("item1", 0);
        //    // Place a check mark next to the item.
        //    item1.Checked = true;
        //    item1.SubItems.Add("1");
        //    item1.SubItems.Add("2");
        //    item1.SubItems.Add("3");
        //    ListViewItem item2 = new ListViewItem("item2", 1);
        //    item2.SubItems.Add("4");
        //    item2.SubItems.Add("5");
        //    item2.SubItems.Add("6");
        //    ListViewItem item3 = new ListViewItem("item3", 0);
        //    // Place a check mark next to the item.
        //    item3.Checked = true;
        //    item3.SubItems.Add("7");
        //    item3.SubItems.Add("8");
        //    item3.SubItems.Add("9");

        //    // Create columns for the items and subitems. 
        //    // Width of -2 indicates auto-size.
        //    listView1.Columns.Add("Item Column", -2, HorizontalAlignment.Left);
        //    listView1.Columns.Add("Column 2", -2, HorizontalAlignment.Left);
        //    listView1.Columns.Add("Column 3", -2, HorizontalAlignment.Left);
        //    listView1.Columns.Add("Column 4", -2, HorizontalAlignment.Center);

        //    //Add the items to the ListView.
        //    listView1.Items.AddRange(new ListViewItem[] { item1, item2, item3 });

        //    // Create two ImageList objects.
        //    ImageList imageListSmall = new ImageList();
        //    ImageList imageListLarge = new ImageList();

        //    // Initialize the ImageList objects with bitmaps.
        //    imageListSmall.Images.Add(Bitmap.FromFile(@"E:\Pictures\Magic\Altered Lands\Card Back_Final.png"));
        //    imageListSmall.Images.Add(Bitmap.FromFile(@"E:\Pictures\Magic\Altered Lands\Card Back_Final.png"));
        //    imageListLarge.Images.Add(Bitmap.FromFile(@"E:\Pictures\Magic\Altered Lands\Card Back_Final.png"));
        //    imageListLarge.Images.Add(Bitmap.FromFile(@"E:\Pictures\Magic\Altered Lands\Card Back_Final.png"));

        //    //Assign the ImageList objects to the ListView.
        //    listView1.LargeImageList = imageListLarge;
        //    listView1.SmallImageList = imageListSmall;

        //    // Add the ListView to the control collection. 
        //    this.SplitContainer2.Panel2.Controls.Add(listView1);
        //}

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

            // Create three items
            //ListViewItem item1 = new ListViewItem("UserNameA", 0);
            //ListViewItem item2 = new ListViewItem("UserNameB", 1);
            //ListViewItem item3 = new ListViewItem("UserNameC", 0);

            // Create columns for the items
            // Width of -2 indicates auto-size.
            _usersListView.Columns.Add("Users", -2, HorizontalAlignment.Left);

            //Add the items to the ListView.
            //usersListView.Items.AddRange(new ListViewItem[] { item1, item2, item3 });

            // Create two ImageList objects.
            ImageList imageListLarge = new ImageList();

            // Initialize the ImageList objects with bitmaps.
            imageListLarge.Images.Add(Bitmap.FromFile(@"E:\Pictures\Wallpapers\The Sun Will Shine.jpg"));
            imageListLarge.Images.Add(Bitmap.FromFile(@"E:\Pictures\Wallpapers\Tree Stars Wallpaper.jpg"));

            //Assign the ImageList objects to the ListView.
            _usersListView.SmallImageList = imageListLarge;

            // Add the ListView to the control collection. 
            this.SplitContainer2.Panel2.Controls.Add(_usersListView);
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
