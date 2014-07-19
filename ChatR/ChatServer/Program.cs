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
        static void Main (string[] args)
        {
            Console.WriteLine ("Server Started...");

            MessageHandler msgHandler = new MessageHandler ();
            var port = Shared.port;

            // Create single TcpListener
            var listener = new TcpListener (IPAddress.Any, port);
            listener.Start ();
            while (true) 
            {
                // Wait for client connection (accept socket)
                var socket = listener.AcceptSocket ();

                // Create stream
                var stream = new NetworkStream (socket);

                 // Create reader
                var streamr = new StreamReader (stream);

                // Create writer
                var streamw = new StreamWriter (stream);
                streamw.AutoFlush = true;

                // Add new client to MessageHandler
                msgHandler.AddClient (streamw, socket);

                // Create a thread that handles reading from the client (pass the reader)
                var thread = new Thread (HandleClient);
                thread.Start(Tuple.Create<StreamReader, MessageHandler, Socket>(streamr, msgHandler, socket));
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
                    msgHandler.OnMessage (socket, message);
                }
                catch (Exception e) 
                {
                    msgHandler.RemoveClient (socket);
                    return;
                }

            }
        }
    }
}

