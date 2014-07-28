using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using System.Web.Script.Serialization;

namespace ChatR.ChatClient
{
    class ImagePoster
    {
        public const string IMGUR_CLIENT_ID = "Client-ID fd159015b8aec94";

        public static string PostToImgur(string imageFilePath)
        {
            using (var w = new WebClient())
            {
                var values = new NameValueCollection();
                values["image"] = Convert.ToBase64String(File.ReadAllBytes(imageFilePath));

                w.Headers.Add("Authorization", IMGUR_CLIENT_ID);
                var response = w.UploadValues("https://api.imgur.com/3/upload.json", values);
                var json = Encoding.ASCII.GetString(response);
                var jss = new JavaScriptSerializer();
                dynamic data = jss.DeserializeObject(json);            
                var id = data["data"]["id"];
                return "https://api.imgur.com/3/image/" + id;
            }
        }
    }
}
