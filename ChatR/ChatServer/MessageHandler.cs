using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace ChatR.ChatServer
{
    class MessageHandler
    {
        Dictionary<Socket, Client> _clients = null;

        private object _lock = new object();

        public MessageHandler()
        {
            _clients = new Dictionary<Socket, Client>();
        }

        public void OnMessage(Socket sender, string message)
        {
            lock (_lock)
            {
                var command = message.Substring(0, 5);
                var content = message.Remove(5);

                var client = _clients[sender];

                if (command == "JOIN ")
                {
                    // Set nickname of sender to specified nickname
                    client.Name = content;

                }
                if (command == "LEAVE")
                {
                }
                if (command == "LIST ")
                {
                }
                if (command == "MSG  ")
                {
                    var toRemove = new List<Socket>();

                    foreach (var c in _clients)
                    {
                        if (c.Key == sender)
                        {
                            continue;
                        }
                        try
                        {
                            var sw = c.Value.StreamWriter;
                            sw.WriteLine(message);
                        }
                        catch (Exception)
                        {
                            toRemove.Add(c.Key);
                        }
                    }
                    foreach (var s in toRemove)
                    {
                        RemoveClient(s);
                    }
                }
            }
        }

        // public void SendMessage(Socket sender, string message)

        public void AddClient(StreamWriter w, Socket s)
        {
            _clients[s] = new Client(w, s);
        }

        public void RemoveClient(Socket s)
        {
            _clients.Remove(s);
        }
    }
}