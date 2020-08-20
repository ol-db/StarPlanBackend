using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarPlan.Models;

namespace UnitTesting.Star_Plan_Logic_Testing.Space_Logic_Testing
{
    [TestClass]
    public class GalaxyTesting
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
        public void TestCorrectToStringMethod(int id, string name, string desc, string expectedGalaxyString) {
            Galaxy galaxy = new Galaxy(id, name, desc);

            string actualGalaxyString = galaxy.ToString();

            Console.WriteLine("Expected");
            Console.Write(expectedGalaxyString);
            Console.WriteLine("Actual");
            Console.Write(actualGalaxyString);

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
        [DataRow("aaaaaaaaaaaaaaaaaaaaa", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaa")]
        [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        #endregion
        public void TestInvalidNameAndDescLength(string name,string desc) {
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
        [DataRow("aaaaaaaaaaaaaaaaaaaa",
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaa")]
        [DataRow("aaaaaa",
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            )]
        #endregion
        public void TestValidNameAndDescLength(string name,string desc)
        {
            //arrange
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
    }
}
