using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace WebServer
{
    namespace HTTP
    {
        public class HTTPRequest : HTTPMessage
        {
            //    <todo>
            //      <title>
            //          change attributes to enum types
            //      </title>
            //      <list>
            //          1.must change content type to MIME type enum
            //          2.must change request method to enum type
            //      </list>
            //      <desc>
            //          to avoid hardcoding
            //      </desc>
            //    </todo>
            private string requestMethod;
            private string contentType;
            private string body;
            private string URI;

            public enum RequestMethodType
            {
                GET,
                POST,
                PUT,
                HEAD,
                DELETE,
                PATCH,
                OPTIONS
            }

            public HTTPRequest(string HTTPRequest) {
                DecodeStatusLine(HTTPRequest);
                DecodeHeaders(HTTPRequest);
                DecodeBody(HTTPRequest);
            }

            #region decode
            //    <todo>
            //      <title>
            //          further validation
            //      </title>
            //      <desc>
            //          whitespace
            //          HTTP version
            //          etc etc...
            //      </desc>
            //    </todo>
            public void DecodeStatusLine(string HTTPRequest) {

                ArgumentException malformedPacket = new ArgumentException("malformed packet");

                string statusLine = new StringReader(HTTPRequest).ReadLine();
                string[] statusTokens;
                statusTokens = statusLine.Split(' ');
                
                if (!(statusTokens.Length == 3))
                {
                    throw malformedPacket;
                }
                else {
                    string statusToken;
                    for(int i=0; i < statusTokens.Length; i++) {
                        statusToken = statusTokens[i];

                        switch (i)
                        {
                            case 0:
                                if (
                                    statusToken.Equals(RequestMethodType.GET.ToString())||
                                    statusToken.Equals(RequestMethodType.POST.ToString())||
                                    statusToken.Equals(RequestMethodType.PUT.ToString())||
                                    statusToken.Equals(RequestMethodType.HEAD.ToString())||
                                    statusToken.Equals(RequestMethodType.DELETE.ToString())||
                                    statusToken.Equals(RequestMethodType.PATCH.ToString())||
                                    statusToken.Equals(RequestMethodType.OPTIONS.ToString()))
                                {
                                    requestMethod = statusToken;
                                }
                                else {
                                    throw malformedPacket;
                                }
                                break;
                            case 1:
                                URI = statusToken;
                                break;
                            case 2:
                                if (!(statusToken == "HTTP/1.1"))
                                {
                                    throw malformedPacket;
                                }
                                break;
                        }
                    }
                }
            }

            public void DecodeHeaders(string HTTPRequest) {
                StringReader HTTPReader = new StringReader(HTTPRequest);
                string line;
                string[] headerTokens;
                while((line = HTTPReader.ReadLine()).Length>0) {
                    line = line.Replace(" ", String.Empty);

                    headerTokens = line.Split(':');
                    if (headerTokens[0].Equals(GetGenericHTTPHeader(GeneralHeaderType.CONTENT_TYPE))) {
                        if (headerTokens[1].Equals(getHTTPMIMEType(MIMETypes.JSON)) ||
                            headerTokens[1].Equals(getHTTPMIMEType(MIMETypes.PLAIN_TEXT)) ||
                            headerTokens[1].Equals(getHTTPMIMEType(MIMETypes.HTML)) ||
                            headerTokens[1].Equals(getHTTPMIMEType(MIMETypes.XML))) {

                            contentType = headerTokens[1];
                        }
                    }
                }
            }

            public void DecodeBody(string HTTPRequest) {
                StringReader HTTPReader = new StringReader(HTTPRequest);
                string line;
                while ((line = HTTPReader.ReadLine()).Length > 0)
                {
                    //until it reaches body
                }
                while ((line = HTTPReader.ReadLine())!=null)
                {
                    body += line + "\n";
                }
            }

            #endregion

            #region getters
            public string GetURI() {
                return this.URI;
            }

            public string GetRequestMethod() {
                return this.requestMethod;
            }

            public string GetBody()
            {
                return this.body;
            }

            public string GetContentType() {
                return this.contentType;
            }

            #endregion
        }
    }
}
