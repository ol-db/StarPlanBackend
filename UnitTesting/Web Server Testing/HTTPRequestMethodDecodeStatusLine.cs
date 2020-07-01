using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebServer.HTTP;

namespace UnitTesting
{
    [TestClass]
    public class HTTPRequestTestStatusLine
    {
        [TestMethod]
        public void MalformedURI()
        {
            //not implemented
            throw new Exception("METHOD NOT IMPLEMENTED");
        }
        [TestMethod]
        public void CorrectURI() {

            string fakeRequest1 = "GET /dog/cat HTTP/1.1";
            string fakeRequest2 = "GET * HTTP/1.1";

            HTTPRequest req1 = new HTTPRequest(fakeRequest1);
            HTTPRequest req2 = new HTTPRequest(fakeRequest2);

            #region parameters
            //expected
            string[] URIExpected1 = {"dog","cat"};
            //actual
            string[] URIActual1 = req1.GetURI();

            //expected
            string[] URIExpected2 = {"*"};
            //actual
            string[] URIActual2 = req2.GetURI();

            #endregion

            //test assert
            for (int i= 0;i < URIExpected1.Length;i++) {
                Assert.AreEqual(URIExpected1[i], URIActual1[i]);
            }
            //test assert
            for (int i = 0; i < URIExpected2.Length; i++)
            {
                Assert.AreEqual(URIExpected2[i], URIActual2[i]);
            }
        }
    }
}
