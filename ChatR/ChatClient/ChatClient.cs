﻿using System;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using ChatR.Common;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace ChatR.ChatClient
{
    class ChatClient
    {
        StreamReader _reader = null;
        StreamWriter _writer = null;
        Control _control = null;
        IChatREvents _eventSink = null;

        public void Connect(Control control, IChatREvents eventSink)
        {
            _control = control;
            _eventSink = eventSink;

            // Connect to chat server
            var ip = Properties.Settings.Default.IP;
            var client = new TcpClient(ip, Shared.port);
            var stream = client.GetStream();
            _reader = new StreamReader(stream);
            _writer = new StreamWriter(stream);
            _writer.AutoFlush = true;
            var thread = new Thread (HandleIncoming_messagesListView);
            thread.Start(this);
        }

        public void Send(string message)
        {
            _writer.WriteLine("MSG " + message);
        }

        public void Join(string name, string URL)
        {
            _writer.WriteLine("JOIN " + name + "," + URL);
        }


        private void HandleMessageOnUIThread(object[] parameters)
        {
            var message = (string)parameters[0];
            var raw_tokens = message.Split(' ');
            var tokens = new List<string>();
            foreach (var t in raw_tokens)
            {
                if (t != "")
                {
                    tokens.Add(t);
                }
            }
            
            switch (tokens[0])
            {
                case "MSG":
                {
                    var text = string.Join(" ", tokens.Skip(2));
                    _eventSink.OnMessage(tokens[1], text);
                    break;
                }
                case "JOIN":
                {
                    _eventSink.OnJoin(tokens[1]);
                    break;
                }
                case "LEAVE":
                {
                    _eventSink.OnLeave(tokens[1]);
                    break;
                }
                case "LIST":
                {
                    _eventSink.OnList(tokens.Skip(1).ToArray());
                    break;
                }
                case "PING":
                {
                    _writer.WriteLine("PONG");
                    break;
                }
            }
        }

        private void HandleMessage(string message)
        {
            _control.Invoke((MethodInvoker)(() => this.HandleMessageOnUIThread(new object[]{message})));
        }

        static void HandleIncoming_messagesListView(object o)
        {
            var client = (ChatClient)o;
            while (true) 
            {
                try
                {
                    var message = client._reader.ReadLine();
                    client.HandleMessage(message);
                }
                catch (Exception e) 
                {
                    return;
                }
            }
        }
    }
}

