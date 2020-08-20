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
    public class HTTPResourceNotFound : Exception
    {
        public HTTPResourceNotFound(string resourceName)
            : base(String.Format("HTTP resource '{0}' not found", resourceName))
        {

        }
    }
}
