using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ChatR.ChatClient
{
    public partial class AvatarSelector : Form
    {
        public AvatarSelector()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "png files (*.png)|*.png|jpg files (*.jpg)|*.jpg";
            var result = dialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            var filename = dialog.FileName;
            TextBox.Text = filename;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            var text = TextBox.Text;
            var url = "";
            if (!text.StartsWith("http"))
            {
                // If it is a file
                if (!File.Exists(text))
                {
                    TextBox.Text = "Invalid Filename or URL";
                    return;
                }
                url = ImagePoster.PostToImgur(text);
                TextBox.Text = url;
            }
            else
            {
                // If it is a URL
                try
                {
                    var uri = new Uri(text);
                    if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    TextBox.Text = ex.Message;
                    return;
                }
                url = text;
            }
            Properties.Settings.Default.AvatarURL = url;
            Properties.Settings.Default.Save();
            MainForm.UpdateAvatarImage(url);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            OKButton.Enabled = TextBox.Text != "";
        }
    }
}
