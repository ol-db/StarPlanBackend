using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.HTTP.Routing
{
    public class MethodRoute
    {
        private HTTPRequest.RequestMethodType method;
        private HTTPResponse response;

        public MethodRoute(HTTPRequest.RequestMethodType method, HTTPResponse response) {
            this.method = method;
            this.response = response;
        }

        #region getters

        public HTTPRequest.RequestMethodType GetMethod() {
            return this.method;
        }

        public HTTPResponse GetResponse()
        {
            return this.response;
        }

        #endregion

    }
}
