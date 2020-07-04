using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.HTTP.Routing
{
    public class Resource
    {
        //the resource denotes the part of the URI that
        private string resourceName;
        private MethodRouteList methodRoutes;
        private ResourceList subResrouces;

        public Resource(string resourceName) {
            this.resourceName = resourceName;
        }

        public void AddMethodRoute(MethodRoute methodRoute) {
            methodRoutes.AddMethodRoute(methodRoute);
        }

        public void AddSubResource(Resource resource) {
            subResrouces.AddResource(resource);
        }

        #region getters
        public HTTPResponse GetMethodRouteResponseByMethod(HTTPRequest.RequestMethodType requestMethod) {
            try
            {
                return methodRoutes.GetMethodRouteByMethod(requestMethod).GetResponse();
            }
            catch (ArgumentException ae) {
                return new HTTPResponse(HTTPResponse.StatusCodes.NOT_FOUND);
            }
        }

        public string GetResourceName() {
            return this.resourceName;
        }

        public Resource GetSubResourceByName(string resourceName) {
            return subResrouces.GetResourceByName(resourceName);
        }
        #endregion
    }
}
