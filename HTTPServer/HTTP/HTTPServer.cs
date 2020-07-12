using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using WebServer.HTTP.Routing;
using WebServer.Networking;

namespace WebServer.HTTP
{


    public class HTTPServer
    {
        private Server TCPServer;
        private Resource baseResource;

        #region server name
        private static readonly string serverName="Aidan's Server";

        public static string GetServerName() {
            return serverName;
        }
        #endregion

        //creates new server to listen on port 5000
        //with serverName "StarPlanWebServer"
        public HTTPServer(int port,Resource baseResource) {
            TCPServer = new Server(port);
            TCPServer.StartServer(10);
            this.baseResource = baseResource;
        }

        #region ApplicationLayer

        private void Listening() {
            int ClientId = TCPServer.AcceptConnection();
            string data = TCPServer.ReceiveFromClient(ClientId);

            //decode and handle HTTP request
            HTTPResponse response;
            try
            {
                HTTPRequest request = new HTTPRequest(data);
                string[] URI = request.GetURI();
                HTTPRequest.RequestMethodType method = request.GetRequestMethod();

                response = getHTTPRoute(method, URI);
            }

            //check for malformed packet
            catch (ArgumentException ae) {
                Console.WriteLine(ae.Message+"...");
                response = new HTTPResponse(HTTPResponse.StatusCodes.BAD_REQUEST);
            }

            Console.WriteLine("HTTP Response {0} {1}", response.GetStatusCodeInt().ToString(),HTTPResponse.GetStatusCodeDesc(response.GetStatusCode()));
            TCPServer.SendToClient(ClientId, response.ToString());
            Listening();
        }

        public void Listen() {
            new Thread(() => { Listening(); }).Start();
        }

        public HTTPResponse getHTTPRoute(HTTPRequest.RequestMethodType method, string[] URI) {

            //HTTP response
            HTTPResponse response;

            Resource URIResource = baseResource;
            try
            {
                for (int i =0;i<URI.Length;i++)
                {
                    if (i == 0)
                    {
                        if (!(URI[i].Equals(URIResource.GetResourceName())))
                        {
                            throw new ArgumentException("resource doesn't exist");
                        }
                    }
                    else
                    {
                        URIResource = URIResource.GetSubResourceByName(URI[i]);
                    }
                }
                response = URIResource.GetMethodRouteResponseByMethod(method);
            }
            catch (ArgumentException ae)
            {
                //if resource doesn't exist
                //send back 404 response
                response = new HTTPResponse(HTTPResponse.StatusCodes.NOT_FOUND);
            }
            return response;
        }

        #endregion

        #region setters

        public void AddSubRoute(Resource resource) {
            this.baseResource.AddSubResource(resource);
        }

        #endregion
    }
}
