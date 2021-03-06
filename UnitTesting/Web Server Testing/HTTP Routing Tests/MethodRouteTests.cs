﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebServer.HTTP;
using WebServer.HTTP.Routing;

namespace UnitTesting.Web_Server_Testing.Routing_Tests
{
    /// <summary>
    /// tests HTTP routing for methods on a resource
    /// such as
    ///     GET
    ///     POST
    ///     PUT
    ///     DELETE
    ///     etc etc...
    /// </summary>

    //    <todo>
    //      <title>
    //          implement mocking
    //      </title>
    //      <desc>
    //          to avoid integration testing
    //      </desc>
    //    </todo>
    [TestClass]
    public class MethodRouteTests
    {
        #region add method tests
        /// <summary>
        /// adding the same method route twice
        /// expected to throw an error
        /// </summary>
        [TestMethod]
        public void AddMethodRoute_DuplicateAction_ArgumentException() {

            //arrange
            MethodRouteList methodRoutes = new MethodRouteList();

            MethodRoute methodRoute = new MethodRoute(
                    HTTPRequest.RequestMethodType.GET,
                    new HTTPResponse(
                        HTTPResponse.StatusCodes.OK,
                        HTTPMessage.MIMETypes.PLAIN_TEXT,
                        "TEXT_TEXT_TEXT"
                        )
                    );

            //act
            methodRoutes.AddMethodRoute(methodRoute);
            try {
                methodRoutes.AddMethodRoute(methodRoute);

                //assert
                Assert.Fail("Two routes added for the same resources with the same method");
            } catch (ArgumentException ae) {
                //test passes
            }
        }

        [TestMethod]
        public void AddMethodRoute_DifferentActions_NoArgumentException()
        {

            //arrange
            MethodRouteList methodRoutes = new MethodRouteList();

            #region params
            MethodRoute methodRoute1 = new MethodRoute(
                    HTTPRequest.RequestMethodType.GET,
                    new HTTPResponse(
                        HTTPResponse.StatusCodes.OK,
                        HTTPMessage.MIMETypes.PLAIN_TEXT,
                        "TEXT_TEXT_TEXT"
                        )
                    );
            MethodRoute methodRoute2 = new MethodRoute(
                    HTTPRequest.RequestMethodType.DELETE,
                    new HTTPResponse(
                        HTTPResponse.StatusCodes.OK,
                        HTTPMessage.MIMETypes.PLAIN_TEXT,
                        "TEXT_TEXT_TEXT"
                        )
                    );
            #endregion

            //act
            methodRoutes.AddMethodRoute(methodRoute1);
            try
            {
                methodRoutes.AddMethodRoute(methodRoute2);
                //test passes
            }
            catch (ArgumentException ae)
            {
                //assert
                Assert.Fail("Two routes cant be added for the same resources with the different methods");
            }
        }
        #endregion
    }
}
