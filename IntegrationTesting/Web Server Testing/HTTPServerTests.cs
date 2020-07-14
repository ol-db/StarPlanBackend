using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServer.HTTP;
using WebServer.HTTP.Routing;

namespace UnitAndIntegrationTesting.Web_Server_Testing
{
    [TestClass]
    public class HTTPServerTests
    {
        [TestMethod]
        public async System.Threading.Tasks.Task RetreiveResponseFromRouteAsync() {
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

            string[] URI = { "school","pupils" };

            //act
            HTTPServer Server = new HTTPServer(3000, new Resource("school"));

            Server.AddSubRoute(pupils);

            HTTPResponse actualResponse = Server.getHTTPRoute(method, URI);
            //assert
            Assert.AreEqual(actualResponse.GetBody(), response.GetBody());
            Assert.AreEqual(actualResponse.GetStatusCode(), response.GetStatusCode());

            Server.Listen();

            HttpClient client = new HttpClient();

            var result = await client.GetAsync("http://192.168.1.116:3000/school/pupils");
            string content = await result.Content.ReadAsStringAsync();

            //Assert.AreEqual(content, "pupils");
        }
    }
}
