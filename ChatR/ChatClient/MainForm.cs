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
        }

        private void SplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            // nothing here
        }

        private void Input_KeyUp(object sender, KeyEventArgs e)
        {

            
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            _client.Connect(this, this);
            _client.Join(NameBox.Text);
        }

        private void NameBox_TextChanged(object sender, EventArgs e)
        {
            ConnectButton.Enabled = NameBox.Text.Length > 0;
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
            {
                // this.SelectNextControl(InputBox, true, true, true, true);
                e.Handled = e.SuppressKeyPress = true;
                if (e.KeyCode == Keys.Enter)
                {
                    var t = (TextBox)sender;
                    string text = t.Text;
                    _client.Send(text);
                }
            }
        }
    }
}
