using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatR.ChatClient
{
    interface IChatREvents
    {
        void OnMessage(string name, string message);
        void OnJoin(string name, string url);
        void OnLeave(string name);
        void OnList(string[] users);
    }
}
