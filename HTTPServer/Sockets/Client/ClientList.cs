using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace WebServer
{
    namespace Networking
    {
        //<todo>
        //    <title>
        //        clean up memory
        //    </title>
        //    <desc>
        //        when client disconnects
        //        remove it from list
        //    </desc>
        //</todo>
        public class ClientList
        {
            private List<Client> clients;
            private List<int> id;
            private int idCount;

            public ClientList()
            {
                this.clients = new List<Client>();
                this.id = new List<int>();
                this.idCount = 0;
            }

            //adds client and gives them an id
            public int AddClient(Socket clientSocket)
            {
                //calculate id for client
                //increment id for next client
                int socketId = idCount;
                idCount++;

                //add new client to list
                //with their id
                clients.Add(new Client(clientSocket));
                id.Add(socketId);
                return socketId;

            }

            //recieves over transport layer
            public string receiveFromClient(int id)
            {
                return clients[id].ReceiveFromClientSocket();
            }

            //sends payload over transport layer
            public void sendToClient(int id, string payload)
            {
                clients[id].SendToClientSocket(payload);
            }
        }
    }
}
