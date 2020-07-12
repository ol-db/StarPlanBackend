using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.HTTP.Routing
{
    public class MethodRouteList
    {

        private List<MethodRoute> methodRoutes;

        private readonly MethodRoute ROUTE_NOT_FOUND;


        //<todo>
        //figure out 404 responses
        //</todo>
        public MethodRouteList() {
            this.methodRoutes = new List<MethodRoute>();
            
        }

        /// <summary>
        /// validates each method route
        /// checks if there is a route for that method
        /// 
        /// if there is:    return true
        /// if not:         return false
        /// </summary>
        /// <param name="method"></param>
        private bool IfMethodExists(HTTPRequest.RequestMethodType method) {
            MethodRoute route = GetRouteFromMethod(method);

            //if method route doesn't exist
            //
            //if method route exists
            if (route == null) {
                return false;
            }
            else
            {
                return true;
            }
        }

        private MethodRoute GetRouteFromMethod(HTTPRequest.RequestMethodType method)
        {
            foreach (MethodRoute methodRoute in methodRoutes)
            {
                if (methodRoute.GetMethod() == method)
                {
                    return methodRoute;
                }
            }
            return null;
        }

        /// <todo>
        ///     <title>
        ///         potential refactor or
        ///         setter method/validation
        ///         for method routing
        ///     </title>
        ///     <desc>
        ///         making sure the same method isn't
        ///         routed twice for the same resources
        ///     </desc>
        /// </todo>
        public void AddMethodRoute(MethodRoute methodRouteToAdd) {

            //making sure the same method route isn't added twice
            if(IfMethodExists(methodRouteToAdd.GetMethod())){
                throw new ArgumentException("route already exists for this method");
            }

            methodRoutes.Add(methodRouteToAdd);
        }


        public MethodRoute GetMethodRouteByMethod(HTTPRequest.RequestMethodType method) {
            MethodRoute methodRoute = (GetRouteFromMethod(method));

            if (methodRoute != null)
            {
                return methodRoute;
            }

            //no method has been found for that route
            //
            //send HTTP error 404 back to user
            else
            {
                
                throw new ArgumentException("no such method exists in the route");
            }
        }

    }
}
