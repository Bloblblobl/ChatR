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
        private Dictionary<Socket, Client> _clients = null;

        public Dictionary<Socket, Client> Clients
        {
            get { return _clients; }
        }

        private object _lock = new object();

        public MessageHandler()
        {
            _clients = new Dictionary<Socket, Client>();
        }

        public void OnMessage(Socket sender, string message)
        {
            lock (_lock)
            {
                var tokens = message.Split(' ');
                var command = tokens[0];
                var content = string.Join(" ", tokens.Skip(1).ToArray());

                var client = _clients[sender];

                if (command == "JOIN")
                {
                    // Set nickname of sender to specified nickname
                    var contents = content.Split(',');
                    client.Name = contents[0];
                    client.URL = contents[1];
                    var users = new List<string>();

                    foreach (KeyValuePair<Socket, Client> c in _clients)
                    {
                        var sw = c.Value.StreamWriter;
                        Send("JOIN " + client.Name + "," + client.URL, c.Value);
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

                    Send(text, client);
                }

                if (command == "LEAVE")
                {
                    _clients.Remove(sender);

                    foreach (KeyValuePair<Socket, Client> c in _clients)
                    {
                        var sw = c.Value.StreamWriter;
                        Send("LEAVE " + client.Name, c.Value);
                    }
                }

                if (command == "MSG")
                {
                    var toRemove = new List<Socket>();

                    foreach (var c in _clients)
                    {
                        try
                        {
                            var sw = c.Value.StreamWriter;
                            var msgContent = string.Join(" ", message.Split(' ').Skip(1).ToArray());
                            var sentMessage = "MSG " + _clients[sender].Name + " " + msgContent;
                            Send(sentMessage, c.Value);
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
            lock (_lock)
            {
                _clients[s] = new Client(w, s);
            }
        }

        public void Send(string message, Client c)
        {
            try
            {
                var sw = c.StreamWriter;
                sw.WriteLine(message);
            }
            catch (Exception e)
            {
                RemoveClient(c.Socket);
            }
        }

        public void RemoveClient(Socket s)
        {
            lock (_lock)
            {
                if (!_clients.ContainsKey(s))
                {
                    return;
                }

                var exClient = _clients[s];
                _clients.Remove(s);

                var disconnectedClients = new List<Socket>();

                foreach (KeyValuePair<Socket, Client> c in _clients)
                {
                    try
                    {
                        var sw = c.Value.StreamWriter;
                        sw.WriteLine("LEAVE " + exClient.Name);
                    }
                    catch (Exception e)
                    {
                        // If a client disconnects in the middle, remove it as well
                        disconnectedClients.Add(c.Key);
                    }
                }

                foreach (var soc in disconnectedClients)
                {
                    RemoveClient(soc);
                }
            }
        }
    }
}