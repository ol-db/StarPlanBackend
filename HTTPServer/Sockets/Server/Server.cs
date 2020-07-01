using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
using WebServer.HTTP;

namespace WebServer
{
    namespace Networking
    {
        // <todo>
        //    <title>
        //        figure out async functionality
        //    </title>
        //    <desc>
        //        for handling many clients
        //        concurrently
        //    </desc>
        //</todo>

        public class Server
        {
            private ClientList clients;

            private Socket socket;

            private EndPoint serverEndpoint;
            private IPAddress serverIP;
            private int PORT;

            #region init
            //starts the server by inputting a port
            public Server(int PORT)
            {
                //creates a new socket endpoint
                //endpoint:     <local ipv4>:<PORT>
                //
                //creates a new TCP socket
                //socket:       accepts ipv4 addresses, sends variable data size (stream), sends/receives via TCP
                //binds the socket to the server endpoint
                Edit(PORT, Dns.GetHostEntry(
                    Dns.GetHostName()).AddressList[1],
                    new IPEndPoint(this.serverIP, this.PORT),
                    new Socket(this.serverIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp));
            }

            private void Edit(
                int PORT,
                IPAddress serverIP, EndPoint serverEndpoint,
                Socket socket)
            {
                this.PORT = PORT;
                this.serverIP = serverIP;
                this.serverEndpoint = serverEndpoint;

                this.socket = socket;
                this.socket.Bind(serverEndpoint);

                this.clients = new ClientList();
            }

            public void StartServer(int backlog)
            {
                //starts the server running
                //backlog refers to max number of connecting users at once
                this.socket.Listen(backlog);
            }
            #endregion

            #region TransportLayer

            public int AcceptConnection()
            {
                return clients.AddClient(socket.Accept());
            }

            public string ReceiveFromClient(int id)
            {
                return clients.receiveFromClient(id);
            }

            public void SendToClient(int id, string data)
            {
                clients.sendToClient(id, data);
            }


            #endregion

            #region getters
            public int GetPORT()
            {
                return this.PORT;
            }

            public string GetIPString()
            {
                return this.serverIP.ToString();
            }
            #endregion
        }
    }
}
