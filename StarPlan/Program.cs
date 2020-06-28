using System;
using WebServer.Server;
using WebServer.HTTP;

namespace StarPlan
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("starting server...\n");
            Server server = new Server(3000);
            server.StartServer(10);
            Console.WriteLine("started server on \n\t{0}:{1}",server.GetIPString(),server.GetPORT());
            Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("listening for users");
                Console.WriteLine("-------------------------------------------------------");

                string serverName = "ServerName";
                string body = "{4:4}";

                HTTPResponse testResponse = new HTTPResponse(
                HTTPResponse.StatusCodes.OK,
                HTTPMessage.MIMETypes.JSON,
                body,
                serverName);

            HTTPRequest testRequest;

            testRequest = server.RecieveOnHTTPStream();
            Console.WriteLine(
                "-------------------------------------------------------" +
                "\nrequest recieved" +
                "\nrequest method\t{0}\nURI\t\t{1}" +
                "\ncontent type\t{2}\nbody\n{3}" +
                "\n-------------------------------------------------------",
                testRequest.GetRequestMethod(),testRequest.GetURI(),
                testRequest.GetContentType(),testRequest.GetBody());

            server.SendOnHTTPStream(testResponse);
            Console.WriteLine(
                "-------------------------------------------------------" +
                "\nresponse sent" +
                "\nstatus code\t{0}\ncontent\t\t{1}" +
                "\n-------------------------------------------------------",
                testResponse.GetStatusCode(),testResponse.GetBody());
        }
    }
}
