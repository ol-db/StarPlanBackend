﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Text;
using WebServer.HTTP;
using WebServer.HTTP.Routing;
using System.Collections;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using System.Dynamic;
using WebServer.Exceptions.HTTP;

namespace UnitTesting.Web_Server_Testing.HTTP_Routing_Tests
{
    [TestClass]
    public class ResourceRouteTests
    {

        [TestMethod]
        [DataRow("")]
        [DataRow("foo")]
        [DataRow("123")]
        [DataRow(null)]
        public void GetResourceByName_ResourceNotInList_ResourceNotFound(string resourceName) {
            //arrange
            ResourceList newResources = new ResourceList();

            try
            {
                //act
                newResources.GetResourceByName(resourceName);

                //assert
                Assert.Fail(string.Format("{0} shouldn't exist", resourceName));
            }
            catch (HTTPResourceNotFound rne) {

                //logging
                System.Diagnostics.Debug.WriteLine(rne.Message);
            }
        }

        [TestMethod]
        [DataRow("foo")]
        [DataRow("123")]
        public void GetResourceByName_ResourceInList_ResourceFound(string resourceName)
        {
            //arrange
            ResourceList newResources = new ResourceList();
            newResources.AddResource(new Resource(resourceName));

            try
            {
                //act
                newResources.GetResourceByName(resourceName);

                //logging
                System.Diagnostics.Debug.WriteLine(string.Format("{0} is found", resourceName));
            }
            catch (HTTPResourceNotFound rne)
            {
                //assert
                Assert.Fail(string.Format("{0} should exist", resourceName));
            }
        }

        #region add sub resrouces

        /// <summary>
        /// adds sub resource
        /// then searches for it
        /// within the resource it 
        /// was added to
        /// 
        /// checks if the resource exists
        /// </summary>
        [TestMethod]
        public void GetSubResourceByName_SubResourceInResource_ReturnsSubResource()
        {
            //arrange
            Resource foo = new Resource("foo");
            Resource bar = new Resource("bar");

            //act
            foo.AddSubResource(bar);

            //assert
            try
            {
                Assert.AreEqual(foo.GetSubResourceByName("bar").GetResourceName(), "bar","sub resource name does not match");
            } catch (ArgumentException ae) {
                Assert.Fail("sub resource should exist but doesn't...");
            }
        }

        /// <summary>
        /// adds sub resource
        /// then searches for it
        /// within the resource it 
        /// was added to
        /// 
        /// checks if the resource exists
        /// </summary>
        [TestMethod]
        public void GetSubResourceByName_SubResourceNotInResource_ArgumentException()
        {
            //arrange
            Resource foo = new Resource("foo");
            Resource bar = new Resource("bar");

            //act
            foo.AddSubResource(bar);

            //assert
            try
            {
                foo.GetSubResourceByName("fooBar");
                Assert.Fail("sub resource shouldn't exist but does...");
            }
            catch (HTTPResourceNotFound rne)
            {
                //test passes
            }
        }

        /// <summary>
        /// adds sub region
        /// in the format
        ///     
        ///     foo={
        ///         bar={
        ///             fooBar={}
        ///             }
        ///         }
        /// 
        /// checks if the resources exist inside foo
        /// </summary>
        [TestMethod]
        public void GetSubResourceByName_AddNestedSubResource_ReturnsSubResource()
        {
            //arrange
            Resource foo = new Resource("foo");
            Resource bar = new Resource("bar");
            Resource fooBar = new Resource("fooBar");

            //act
            bar.AddSubResource(fooBar);
            foo.AddSubResource(bar);

            //assert
            try
            {
                Assert.AreEqual(foo.GetSubResourceByName("bar").GetResourceName(), "bar", "sub resource name does not match");
                Assert.AreEqual(foo.GetSubResourceByName("bar").GetSubResourceByName("fooBar").GetResourceName(), "fooBar", "sub resource name does not match");
            }
            catch (ArgumentException ae)
            {
                Assert.Fail("sub resource should exist but doesn't...");
            }
        }

        /// <summary>
        /// adds sub region
        /// in the format
        ///     
        ///     shop=
        ///         {
        ///             products=
        ///                 {
        ///                     electronics={},
        ///                     food={}
        ///                 },
        ///             staff=
        ///                 {
        ///                     owner={},
        ///                     employees={}
        ///                 }
        ///         }
        /// 
        /// checks if the resources exist inside shop
        /// </summary>
        [TestMethod]
        public void GetSubResourceByName_AddNestedSubResources_ReturnsSubResource()
        {
            //arrange
            Resource shop = new Resource("shop");
            Resource products = new Resource("products");
            Resource electronics = new Resource("electronics");
            Resource food = new Resource("food");
            Resource staff = new Resource("staff");
            Resource owner = new Resource("owner");
            Resource employees = new Resource("employees");

            //act
            products.AddSubResource(food);
            products.AddSubResource(electronics);

            staff.AddSubResource(owner);
            staff.AddSubResource(employees);

            shop.AddSubResource(staff);
            shop.AddSubResource(products);

            //assert
            try
            {
                //getting all items in shop
                Assert.AreEqual(shop.GetSubResourceByName("products").GetResourceName(), "products");
                Assert.AreEqual(shop.GetSubResourceByName("staff").GetResourceName(), "staff");

                //getting all items in products
                Assert.AreEqual(shop.GetSubResourceByName("products").GetSubResourceByName("food").GetResourceName(), "food");
                Assert.AreEqual(shop.GetSubResourceByName("products").GetSubResourceByName("electronics").GetResourceName(), "electronics");

                //getting all items in staff
                Assert.AreEqual(shop.GetSubResourceByName("staff").GetSubResourceByName("owner").GetResourceName(), "owner");
                Assert.AreEqual(shop.GetSubResourceByName("staff").GetSubResourceByName("employees").GetResourceName(), "employees");
            }
            catch (ArgumentException ae)
            {
                Assert.Fail("sub resource should exist but doesn't...");
            }
        }

        #endregion
    }
}
