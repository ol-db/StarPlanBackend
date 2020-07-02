using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer
{
    namespace HTTP
    {
        /// <summary>
        /// enables user to build a response
        /// by passing in
        ///     content type
        ///     body
        ///     server name
        ///     etc etc...
        /// </summary>
        public class HTTPResponse : HTTPMessage
        {

            private StatusCodes statusCode;
            private MIMETypes contentType;
            private string server;
            private string body;

            private string statusLine;
            private string headers;
            private string newLineAndContent;

            #region statusCode
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
            #endregion

            #region init

            public HTTPResponse(StatusCodes statusCode, MIMETypes contentType, string body, string server)
            {
                Edit(statusCode, contentType, body, server);
            }

            public HTTPResponse(StatusCodes statusCode, string server)
            {
                Edit(statusCode, MIMETypes.NONE, null, server);
            }

            private void Edit(StatusCodes statusCode, MIMETypes contentType, string body, string server)
            {
                this.statusCode = statusCode;
                this.contentType = contentType;
                this.body = body;
                this.server = server;

                ConstructResponseString();
            }

            private void ConstructResponseString() {

                //create status line
                statusLine = string.Format("HTTP/1.1 {0} {1}\n",
                    statusCode.GetHashCode(), GetStatusCodeDesc(statusCode));

                //create headers
                //
                //if headers exist
                headers = "";

                //if server name exists add header
                if (server != null)
                    headers += string.Format("Server: {0}\n",server);

                //if content type exists add header
                //
                //or if there is a body but no content type
                //add header of type text
                if (contentType != MIMETypes.NONE)
                {
                    headers += string.Format("Content-Type: {0}\n", getHTTPMIMEType(contentType));
                }
                else if (body != null) {
                    headers += string.Format("Content-Type: {0}\n", MIMETypes.PLAIN_TEXT);
                }

                //create new line and content with body
                //
                //if there is a body
                newLineAndContent = "";
                if (body != null) {
                    newLineAndContent += string.Format("\n{0}", body);
                }
            }

            #endregion

            public override string ToString()
            {

                return string.Format(
                    statusLine +            //HTTP status line
                    headers +               //server header
                    newLineAndContent       //content type of the body
                    );
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
