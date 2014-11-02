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
    using ChatR.Common;
    using System.Threading;

    public partial class AvatarSelector : Form
    {
        public AvatarSelector()
        {
            InitializeComponent();
        }

        public static string CreateFileName(string url)
        {
            var filename = "Avatars/" + Shared.EncodeURL(url) + ".png";
            return filename;
        }

        public static Image GetAvatarImage(string url)
        {
            // generate avatar filename based on url
            var filename = CreateFileName(url);
            try
            {
                if (!File.Exists(filename))
                {
                    // fetch avatar from URL
                    using (var w = new WebClient())
                    {
                        w.Headers.Add("Authorization", ImagePoster.IMGUR_CLIENT_ID);
                        var json = w.DownloadString(url);
                        var jss = new JavaScriptSerializer();
                        dynamic data = jss.DeserializeObject(json);
                        var link = data["data"]["link"];
                        DownloadFile(link, filename);
                    }
                }
                return Bitmap.FromFile(filename);
            }
            catch (Exception e)
            {
                // if failed to retrieve custom avatar, return default avatar
                var defaultFile = "Avatars/Avatar.png";
                return Bitmap.FromFile(defaultFile);
            }
        }

        private static void DownloadFile(string url, string filename)
        {
            var w = new WebClient();
            for (var i = 0; i < 5; i++)
            {
                try
                {
                    w.DownloadFile(url, filename);
                }
                catch (Exception e)
                {
                    if (i > 3)
                    {
                        throw;
                    }
                    Thread.Sleep(100);
                }
            }
        }

        private static Image DownloadImage(string url)
        {

            // fetch image from URL
            using (var w = new WebClient())
            {
                w.DownloadFile(url, CreateFileName(url));
            }
            return Bitmap.FromFile(settings.Default.Avatar);
        }

        public static void UpdateAvatarImage(string filename)
        {
            

            var url = ImagePoster.PostToImgur(filename);

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
            var filename = Properties.Settings.Default.Avatar;
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
                filename = CreateFileName(url);
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
                filename = CreateFileName(url);
                File.Copy(text, filename, true);
                TextBox.Text = url;
            }

            UpdateAvatarImage(filename);
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
