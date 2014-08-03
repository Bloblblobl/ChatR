using System;
using System.IO;
using System.Net.Sockets;

namespace ChatR.ChatServer
{
    public class Client
    {
        public StreamWriter StreamWriter;
        public string Name;
        public string URL;
        public Socket Socket;

        public Client(StreamWriter sw, Socket socket)
        {
            StreamWriter = sw;
            Name = null;
            URL = null;
            Socket = socket;
        }
    }
}

