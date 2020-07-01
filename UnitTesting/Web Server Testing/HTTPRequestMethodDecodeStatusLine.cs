using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebServer.HTTP;

namespace UnitTesting
{
    [TestClass]
    public class HTTPRequestTestStatusLine
    {
        [TestMethod]
        public void MalformedStatusLine()
        {
            //not implemented
            throw new Exception("METHOD NOT IMPLEMENTED");
        }
        [TestMethod]
        public void CorrectStatusLine() {

            string fakeRequest = "GET /dog/cat HTTP/1.1";

            HTTPRequest req = new HTTPRequest(fakeRequest);

            #region parameters
            //expected
            string[] URIExpected = {"dog","cat"};
            //actual
            string[] URIActual = req.GetURI();
            #endregion

            //test assert
            for (int i= 0;i < URIExpected.Length;i++) {
                Assert.AreEqual(URIExpected[i], URIActual[i]);
            }
        }
    }
}
