using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Exceptions.HTTP
{
    [Serializable]
    public class MalformedHTTPPacketException : Exception
    {
        public MalformedHTTPPacketException(string msg)
            : base(String.Format("Malformed HTTP Packet: {0}", msg))
        {

        }
    }

    [Serializable]
    public class HTTPResourceNotFoundException : Exception
    {
        public HTTPResourceNotFoundException(string resourceName)
            : base(String.Format("HTTP resource '{0}' not found", resourceName))
        {

        }
    }
}
