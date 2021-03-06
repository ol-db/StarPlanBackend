﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer
{
    namespace HTTP
    {
        public abstract class HTTPMessage
        {
            public enum MIMETypes
            {
                JSON,
                PLAIN_TEXT,
                HTML,
                XML,
                NONE
            }

            public enum GeneralHeaders
            {
                CONTENT_TYPE
            }

            //returns string values for status codes
            public static string getHTTPMIMEType(MIMETypes MIMEType)
            {
                switch (MIMEType)
                {
                    case MIMETypes.JSON:
                        return "application/json";
                    case MIMETypes.PLAIN_TEXT:
                        return "text/plain";
                    case MIMETypes.HTML:
                        return "text/html";
                    case MIMETypes.XML:
                        return "application/xml";
                    default:
                        return null;
                }
            }
            public string GetGenericHeader(GeneralHeaders header)
            {
                switch (header)
                {
                    case GeneralHeaders.CONTENT_TYPE:
                        return "Content-Type";
                    default:
                        return null;
                }
            }
        }
    }
}
