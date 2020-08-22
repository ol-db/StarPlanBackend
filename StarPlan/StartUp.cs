using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using StarPlan.Models;
using StarPlan.Models.Space.Planets;
using StarPlan.StarPlanConfig;
using WebServer.HTTP;
using WebServer.HTTP.Routing;

namespace StarPlan
{
    public class StartUp
    {
        /// <summary>
        /// start up class
        /// where the main thread is run
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //arrange
            Resource pupils = new Resource("pupils");

            HTTPResponse response = new HTTPResponse(
                        HTTPResponse.StatusCodes.OK,
                        HTTPMessage.MIMETypes.PLAIN_TEXT,
                        "pupils"
                        );

            pupils.AddMethodRoute(new MethodRoute(
                    HTTPRequest.RequestMethodType.GET,
                    response
                    )
                );

            HTTPRequest.RequestMethodType method = HTTPRequest.RequestMethodType.GET;

            string[] URI = { "school", "pupils" };

            //act
            int port = 3000;
            HTTPServer Server = new HTTPServer(port, new Resource("school"));

            Server.AddSubRoute(pupils);

            HTTPResponse actualResponse = Server.getHTTPRoute(method, URI);
            //assert

            Server.Listen();

            //throw new NotImplementedException("nothing to start...");
        }
    }
}
