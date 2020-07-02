using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.HTTP.Routing
{
    public class MethodRouteList
    {

        private List<MethodRoute> methodRoutes;

        public MethodRouteList() {
            this.methodRoutes = new List<MethodRoute>();
        }

        public void AddMethodRoute(MethodRoute methodRouteToAdd) {
            foreach (MethodRoute methodRoute in methodRoutes)
            {
                if (methodRoute.GetMethod() == methodRouteToAdd.GetMethod())
                {
                    throw new ArgumentException("a route for this method already exists");
                }
            }
        }

        public MethodRoute GetMethodRouteByMethod(HTTPRequest.RequestMethodType method) {
            foreach (MethodRoute methodRoute in methodRoutes) {
                if (methodRoute.GetMethod() == method)
                {
                    return methodRoute;
                }
            }

            //no method has been found for that route
            //
            //send HTTP error 4xx back to user
            throw new ArgumentException("no such method exists in the route");
        }

    }
}
