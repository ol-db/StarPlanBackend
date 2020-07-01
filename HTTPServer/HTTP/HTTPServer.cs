using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WebServer.Networking;

namespace WebServer.HTTP
{
    public class HTTPServer
    {
        private Server TCPServer;

        private readonly string serverName;

        //creates new server to listen on port 5000
        //with serverName "StarPlanWebServer"
        public HTTPServer() {
            TCPServer = new Server(5000);
            serverName = "StarPlanWebServer";
        }

        #region ApplicationLayer

        public void Listen() {
            TCPServer.AcceptConnection();
        }

        public HTTPRequest ReceiveHTTPRequest() {
            string data = TCPServer.ReceiveFromClient(TCPServer.AcceptConnection());
            HTTPRequest request = new HTTPRequest(data);
            return request;
        }



        #endregion
    }
}
