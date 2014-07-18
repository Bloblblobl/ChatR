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
            _client.Connect(this, this);
            _client.Join("Joihn");
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void Input_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
            {
                return;
            }

            var t = (TextBox)sender;
            string text = t.Text;
            _client.Send(text);

        }
    }
}
