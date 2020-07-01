using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            private RequestMethodType requestMethod;
            private string contentType;
            private string body;
            private string[] URI;

            private bool hasBody;

            #region Request_Method

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

            public static RequestMethodType RequestMethodTypeStringToValue(string methodType)
            {

                //enum request methods
                const RequestMethodType GET = RequestMethodType.GET;
                const RequestMethodType POST = RequestMethodType.POST;
                const RequestMethodType PUT = RequestMethodType.PUT;
                const RequestMethodType HEAD = RequestMethodType.HEAD;
                const RequestMethodType DELETE = RequestMethodType.DELETE;
                const RequestMethodType PATCH = RequestMethodType.PATCH;
                const RequestMethodType OPTIONS = RequestMethodType.OPTIONS;

                //string request methods
                string get = RequestMethodType.GET.ToString();
                string post = RequestMethodType.POST.ToString();
                string put = RequestMethodType.PUT.ToString();
                string head = RequestMethodType.HEAD.ToString();
                string delete = RequestMethodType.DELETE.ToString();
                string patch = RequestMethodType.PATCH.ToString();
                string options = RequestMethodType.OPTIONS.ToString();

                #region If_Statements

                if (get.Equals(methodType))
                {
                    return GET;
                }
                else if (post.Equals(methodType))
                {
                    return POST;
                }
                else if (put.Equals(methodType))
                {
                    return PUT;
                }
                else if (head.Equals(methodType))
                {
                    return HEAD;
                }
                else if (delete.Equals(methodType))
                {
                    return DELETE;
                }
                else if (patch.Equals(methodType))
                {
                    return PATCH;
                }
                else if (options.Equals(methodType))
                {
                    return OPTIONS;
                }
                else
                {
                    throw new ArgumentException("method type invalid");
                }

                #endregion

            }

            #endregion

            public HTTPRequest(string HTTPRequest) {
                hasBody=false;
                DecodeStatusLine(HTTPRequest);
                DecodeHeaders(HTTPRequest);
                if (hasBody)
                {
                    DecodeBody(HTTPRequest);
                }
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
                    for (int i = 0; i < statusTokens.Length; i++) {
                        statusToken = statusTokens[i];

                        switch (i)
                        {
                            //checks the request method is valid otherwise
                            //the packet must be malformed
                            case 0:
                                try
                                {
                                    requestMethod = RequestMethodTypeStringToValue(statusToken);
                                }
                                catch (ArgumentException) {
                                    throw malformedPacket;
                                }
                                break;
                            case 1:
                                URI = statusToken.Split('/');
                                URI = URI.Skip(1).ToArray();
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

            //    <todo>
            //      <title>
            //          validate HTTP request without body
            //      </title>
            //      <desc>
            //          do check for null line
            //          when there's no empty line present
            //          otherwise a null exception occurs
            //      </desc>
            //    </todo>
            public void DecodeHeaders(string HTTPRequest) {
                StringReader HTTPReader = new StringReader(HTTPRequest);
                string line;
                string[] headerTokens;
                while (((line = HTTPReader.ReadLine()) != null)) {
                    if (line.Length == 0) {
                        hasBody = true;
                        //if body is reached
                        //stop decoding HTTP headers
                        break;
                    }
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
                while (((line = HTTPReader.ReadLine()).Length) > 0)
                {
                    //until it reaches body
                }
                while ((line = HTTPReader.ReadLine()) != null)
                {
                    body += line + "\n";
                }
            }

            #endregion

            #region getters
            public string[] GetURI() {
                return this.URI;
            }

            public string GetRequestMethodString() {
                return this.requestMethod.ToString();
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
