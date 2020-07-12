using System;
using System.Collections.Generic;
using System.Text;
using WebServer.HTTP;
using WebServer.HTTP.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Threading;

namespace UnitAndIntegrationTesting.Web_Server_Testing
{
    [TestClass]
    class HTTPServerIntegrationTests
    {
        [TestMethod]
        public void GetRequestOnExistingRouteAsync() {
            Resource pupils = new Resource("pupils");

            HTTPResponse pupilsResponse = new HTTPResponse(
                        HTTPResponse.StatusCodes.OK,
                        HTTPMessage.MIMETypes.PLAIN_TEXT,
                        "pupils"
                        );

            pupils.AddMethodRoute(new MethodRoute(
                    HTTPRequest.RequestMethodType.GET,
                    pupilsResponse
                    )
                );

            Resource year9 = new Resource("year9");

            HTTPResponse year9Response = new HTTPResponse(
                        HTTPResponse.StatusCodes.OK,
                        HTTPMessage.MIMETypes.PLAIN_TEXT,
                        "year 9"
                        );

            year9.AddMethodRoute(new MethodRoute(
                    HTTPRequest.RequestMethodType.GET,
                    year9Response
                    )
                );

            Resource year10 = new Resource("year10");

            HTTPResponse year10Response = new HTTPResponse(
                        HTTPResponse.StatusCodes.OK,
                        HTTPMessage.MIMETypes.PLAIN_TEXT,
                        "year 10"
                        );

            year10.AddMethodRoute(new MethodRoute(
                    HTTPRequest.RequestMethodType.GET,
                    year10Response
                    )
                );

            pupils.AddSubResource(year10);
            pupils.AddSubResource(year9);

            Resource school = new Resource("school");

            HTTPResponse schoolResponse = new HTTPResponse(
                        HTTPResponse.StatusCodes.OK,
                        HTTPMessage.MIMETypes.PLAIN_TEXT,
                        "school"
                        );

            school.AddMethodRoute(new MethodRoute(
                    HTTPRequest.RequestMethodType.GET,
                    schoolResponse
                    )
                );

            HTTPServer Server = new HTTPServer(3000, school);

            Server.AddSubRoute(pupils);

            Server.Listen();
        }
    }
}
