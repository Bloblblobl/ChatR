using System;
using System.IO;

namespace ChatR.Common
{
    public static class Shared
    {
        public static int port = 55555;

        public static string ReadLine(StreamReader sr)
        {
            var message = sr.ReadLine();
            if (message == null)
            {
                throw new Exception("! :: ERR0R READING FR0M STREAM :: !");
            }
            return message;
        }
    }
}

