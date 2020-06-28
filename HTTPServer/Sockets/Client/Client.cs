using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace WebServer
{
    namespace Networking
    {
        public class Client
        {
            private Socket socket;

            public Client(Socket clientSocket)
            {
                this.socket = clientSocket;
            }

            public string ReceiveFromClientSocket()
            {
                //receives socket request
                byte[] data = new byte[4056];
                Debug.WriteLine("client started recieving");
                this.socket.Receive(data);

                //decode socket request and return it
                string request = Encoding.UTF8.GetString(data);

                return request;
            }

            public void SendToClientSocket(string response)
            {
                //decodes response into UTF-8 bytes
                byte[] responseBytes = Encoding.UTF8.GetBytes(response.ToString());

                //sends response over client socket
                this.socket.Send(responseBytes);
            }

            public void CloseConnection()
            {

                //close the socket
                //releases all resources
                this.socket.Close();
                this.socket.Dispose();
            }

        }
    }
}
