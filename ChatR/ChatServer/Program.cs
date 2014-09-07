using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using ChatR.Common;

namespace ChatR.ChatServer
{
    class Program
    {
        const int SOCKET_TIMEOUT = 5000; // In milliseconds
        static void Main(string[] args)
        {
            Console.WriteLine("Server Started...");

            MessageHandler msgHandler = new MessageHandler();
            var port = Shared.port;

            // Create single TcpListener
            var listener = new TcpListener (IPAddress.Any, port);
            listener.Start();
            new Thread(PingClients).Start(msgHandler);
            
            while (true) 
            {
                // Wait for client connection (accept socket)
                var socket = listener.AcceptSocket();
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, SOCKET_TIMEOUT);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, SOCKET_TIMEOUT);

                // Create stream
                var stream = new NetworkStream(socket);

                 // Create reader
                var streamr = new StreamReader(stream);

                // Create writer
                var streamw = new StreamWriter(stream);
                streamw.AutoFlush = true;

                // Add new client to MessageHandler
                msgHandler.AddClient(streamw, socket);

                // Create a thread that handles reading from the client (pass the reader)
                var thread = new Thread (HandleClient);
                thread.Start(Tuple.Create<StreamReader, MessageHandler, Socket>(streamr, msgHandler, socket));
            }
        }

        static void PingClients(object o)
        {
            var msgHandler = (MessageHandler)o;
            var clients = msgHandler.Clients;

            while (true)
            {
                Thread.Sleep(3000);
                foreach (var c in clients)
                {
                    var client = c.Value;
                    var socket = c.Key;

                    try
                    {
                        msgHandler.Send("PING", client);
                    }
                    catch (Exception e)
                    {
                        msgHandler.RemoveClient(socket);
                    }
                }
            }
        }

        static void HandleClient(object o)
        {
            var tuple = (Tuple<StreamReader, MessageHandler, Socket>)o;
            var reader = tuple.Item1;
            var msgHandler = tuple.Item2;
            var socket = tuple.Item3;

            while (true) 
            {
                try
                {
                    var message = Shared.ReadLine(reader);
                    if (message != "PONG")
                    {
                        msgHandler.OnMessage(socket, message);
                    }
                }
                catch (Exception e) 
                {
                    msgHandler.RemoveClient(socket);

                    return;
                }

            }
        }
    }
}

