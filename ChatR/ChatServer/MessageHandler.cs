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
                var content = message.Substring(5);

                var client = _clients[sender];

                if (command == "JOIN ")
                {
                    // Set nickname of sender to specified nickname
                    var contents = content.Split(',');
                    client.Name = contents[0];
                    client.URL = contents[1];
                    var users = new List<string>();

                    foreach (KeyValuePair<Socket, Client> c in _clients)
                    {
                        var sw = c.Value.StreamWriter;
                        sw.WriteLine("JOIN " + client.Name + "," + client.URL);
                        if (c.Value.Name == _clients[sender].Name)
                        {
                            continue;
                        }
                        users.Add(c.Value.Name + "," + c.Value.URL);
                    }

                    var splitter = "";

                    if (users.Capacity > 1)
                    {
                        splitter = " ";
                    }

                    var text = "LIST " + string.Join(splitter, users);

                    client.StreamWriter.WriteLine(text);
                }

                if (command == "LEAVE")
                {
                    _clients.Remove(sender);

                    foreach (KeyValuePair<Socket, Client> c in _clients)
                    {
                        var sw = c.Value.StreamWriter;
                        sw.WriteLine("LEAVE " + client.Name);
                    }
                }

                if (command == "MSG  ")
                {
                    var toRemove = new List<Socket>();

                    foreach (var c in _clients)
                    {
                        try
                        {
                            var sw = c.Value.StreamWriter;
                            var msgContent = message.Substring(5);
                            message = "MSG " + _clients[sender].Name + " " + msgContent;
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