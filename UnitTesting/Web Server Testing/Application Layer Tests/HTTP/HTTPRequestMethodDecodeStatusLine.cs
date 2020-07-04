using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebServer.HTTP;

namespace UnitTesting.Web_Server_Testing
{
    [TestClass]
    public class HTTPRequestTestStatusLine
    {
        /// <summary>
        /// checks that decoding the HTTP status line
        /// returns valid URI
        /// </summary>
        /// <param name="URIExpected"></param>
        /// <param name="requestStr"></param>
        [TestMethod]
        [DataRow((new string[] { "*" }), "GET * HTTP/1.1")]
        [DataRow((new string[] { "dog", "cat" }), "GET /dog/cat HTTP/1.1")]
        public void CorrectURI(string[] URIExpected, string requestStr) {

            HTTPRequest request = new HTTPRequest(requestStr);

            #region parameters
            //actual
            string[] URIActual1 = request.GetURI();
            #endregion

            //test assert
            for (int i= 0;i < URIExpected.Length;i++) {
                Assert.AreEqual(URIExpected[i], URIActual1[i]);
            }
        }
    }
}
