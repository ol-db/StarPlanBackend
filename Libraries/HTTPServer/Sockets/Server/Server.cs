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
                this.PORT = PORT;
                this.serverIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1];
                this.serverEndpoint = new IPEndPoint(this.serverIP, this.PORT);

                this.socket = new Socket(this.serverIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                this.socket.Bind(serverEndpoint);

                this.clients = new ClientList();
            }

            public void StartServer(int backlog)
            {
                //starts the server running
                //backlog refers to max number of connecting users at once
                Console.WriteLine("Server Listening On Socket {0}:{1} at time {2}", serverIP, PORT,DateTime.Now);
                this.socket.Listen(backlog);
            }
            #endregion

            // <todo>
            //    <title>
            //        add async callback methods
            //    </title>
            //</todo>
            #region TransportLayer

            public int AcceptConnection()
            {
                int id = clients.AddClient(socket.Accept());
                Console.WriteLine("Connection Accpeted... at time {0}", DateTime.Now);
                return id;
            }

            public string ReceiveFromClient(int id)
            {
                string data = clients.receiveFromClient(id);
                Console.WriteLine("Received Data From User <id:{0}>... at time {1}",id, DateTime.Now);
                return data;
            }

            public void SendToClient(int id, string data)
            {
                clients.sendToClient(id, data);
                Console.WriteLine("Sent Data To User <id:{0}>... at time {1}", id, DateTime.Now);
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
