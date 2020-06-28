using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer
{
    namespace HTTP
    {
        public class HTTPResponse : HTTPMessage
        {

            private StatusCodes statusCode;
            private MIMETypes contentType;
            private string server;
            private string body;

            private string statusLine;
            private string headers;

            //stores
            public enum StatusCodes
            {
                OK = 200,
                BAD_REQUEST = 400,
                NOT_FOUND = 404
            }

            //returns string values for status codes
            public static string GetStatusCodeDesc(StatusCodes statusCode)
            {
                switch (statusCode)
                {
                    case StatusCodes.OK:
                        return "OK";
                    case StatusCodes.BAD_REQUEST:
                        return "BAD REQUEST";
                    case StatusCodes.NOT_FOUND:
                        return "NOT FOUND";
                    default:
                        return null;
                }
            }

            //    <todo>
            //      <title>
            //          constructor with params: statusCode
            //      </title>
            //      <desc>
            //          to create simpler responses
            //      </desc>
            //    </todo>
            public HTTPResponse(StatusCodes statusCode, MIMETypes contentType, string body, string server)
            {
                this.statusCode = statusCode;
                this.contentType = contentType;
                this.body = body;
                this.server = server;
            }

            //    <todo>
            //      <title>
            //          change ToString to create different
            //          structured requests
            //      </title>
            //      <desc>
            //          remove/add headers
            //          remove/add body
            //          etc etc...
            //      </desc>
            //    </todo>
            public override string ToString()
            {
                return string.Format(
                    "HTTP/1.1 {0} {1}\n" +       //HTTP status line
                    "Server: {2}\n" +            //server header
                    "Content-Type: {3}\n" +      //content type of the body
                    "\n" +                       //new line (to represent body)
                    "{4}"                       //body
                    , statusCode.GetHashCode(), GetStatusCodeDesc(statusCode),
                    server,
                    getHTTPMIMEType(contentType),
                    body);
            }

            #region getters
            public int GetStatusCode () {
                return this.statusCode.GetHashCode();
            }

            public string GetBody() {
                return this.body;
            }
            #endregion
        }
    }
}
