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

        public MainForm()
        {
            InitializeComponent();
            _client = new ChatClient();
            NameBox.Focus();
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
            _client.Connect(this, this);
            _client.Join(NameBox.Text);
            InputBox.Focus();
        }
    }
}
