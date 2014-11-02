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
    using settings = Properties.Settings;
    using System.Web.Script.Serialization;
    using System.Net;

    public partial class AvatarSelector : Form
    {
        static string _filePath = @"Avatars/" + Properties.Settings.Default.Avatar;

        public AvatarSelector()
        {
            InitializeComponent();
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

        public static Image GetAvatarImage(string url)
        {

            if (File.Exists(_filePath))
            {
                return Bitmap.FromFile(_filePath);
            }

            return FetchAvatarFromImgur(url);
        }

        private static Image FetchAvatarFromImgur(string url)
        {
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

        private static Image DownloadImage(string url)
        {

            // fetch image from URL
            using (var w = new WebClient())
            {
                w.DownloadFile(url, _filePath);
            }
            return Bitmap.FromFile(settings.Default.Avatar);
        }

        public static void UpdateAvatarImage()
        {
            

            var url = ImagePoster.PostToImgur(_filePath);

            Properties.Settings.Default.AvatarURL = url;
            Properties.Settings.Default.Save();
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

            if (text.StartsWith("http"))
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
                DownloadImage(url);
            }
            else
            {
                // If it is a file
                if (!File.Exists(text))
                {
                    TextBox.Text = "Invalid Filename or URL";
                    return;
                }
                url = ImagePoster.PostToImgur(text);
                File.Copy(text, _filePath, true);
                TextBox.Text = url;
            }

            UpdateAvatarImage();
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
