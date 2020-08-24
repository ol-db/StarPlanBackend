﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StarPlan.Models;
using StarPlan.Models.Space.Planets;

namespace UnitTesting.Star_Plan_Logic_Testing.Space_Logic_Testing
{
    [TestClass]
    public class GalaxyTests
    {
        /// <summary>
        /// create a new galaxy
        /// check that the string representation of the class
        /// works properly
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="expectedGalaxyString"></param>
        [TestMethod]
        #region data
        [DataRow(0, "name", "desc desc desc",
                (
                    "START_OF_GALAXY\n" +
                    "id:0\n" +
                    "name:name\n" +
                    "desc:desc desc desc\n" +
                    "" +
                    "END_OF_GALAXY\n"
                )
            )]
        [DataRow(1, "newGalaxy", "this is a new galaxy",
                (
                    "START_OF_GALAXY\n" +
                    "id:1\n" +
                    "name:newGalaxy\n" +
                    "desc:this is a new galaxy\n" +
                    "" +
                    "END_OF_GALAXY\n"
                )
            )]
        #endregion
        public void ToString_ValidObject_ValidString(int id, string name, string desc, string expectedGalaxyString) {
            
            //arrange
            Galaxy galaxy = new Galaxy(id, name, desc);

            //act
            string actualGalaxyString = galaxy.ToString();

            //logging
            Console.WriteLine("Expected");
            Console.Write(expectedGalaxyString);
            Console.WriteLine("Actual");
            Console.Write(actualGalaxyString);

            //assert
            Assert.AreEqual(expectedGalaxyString, actualGalaxyString);
        }

        #region validate name and desc
        /// <summary>
        /// checking if entering a name and desc that are
        /// too many charecters long
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        [TestMethod]
        #region data

        //boundary
        [DataRow("aaaaaaaaaaaaaaaaaaaaa", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaa")]

        //valid
        [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        #endregion
        public void Constructor_NameDescTooLong_ArgumentException(string name,string desc) {
            try
            {
                //arrange & act
                Galaxy galaxy = new Galaxy(0, name, desc);

                //assert
                Assert.Fail();
            }
            catch (ArgumentException ae) {
                //test passed
            }
        }

        [TestMethod]
        #region data

        //boundary
        [DataRow("aaaaaaaaaaaaaaaaaaaa",
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaa")]

        //valid
        [DataRow("aaaaaa",
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            )]
        #endregion
        public void Constructor_NameDescValidLength_NoArgumentException(string name,string desc)
        {
            try
            {
                //act & arrange
                Galaxy galaxy = new Galaxy(0,name,desc);
                //test passed
            }
            catch (ArgumentException ae)
            {
                //assert
                Assert.Fail();
            }
        }

        #endregion

        #region DB methods

        #endregion
    }
}
