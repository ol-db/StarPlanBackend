﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using StarPlan.Models;
using StarPlan.Models.Space.Planets;
using StarPlanDBAccess.Procedures;

namespace UnitTesting.Star_Plan_Logic_Testing.Space_Logic_Testing
{
    [TestClass]
    public class GalaxyTests
    {

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

        #region get

        [TestMethod]
        public void GetAllFromDB_ValidRecords_ReturnGalaxyListJson()
        {
            //arrange
            ISqlStoredProc proc = MockLoadGalaxies();
            GalaxyList galaxies = new GalaxyList();
            List<object> galaxiesExpected = TestLoadGalaxyObject();
            string galaxiesExpectedJson = JsonConvert.SerializeObject(galaxiesExpected);

            //act
            galaxies.GetAllFromDB(proc);

            //logging
            Console.WriteLine("expected: {0}", galaxiesExpectedJson);
            Console.WriteLine("actual: {0}", galaxies.ToJson());

            //assert
            Assert.AreEqual(galaxiesExpectedJson, galaxies.ToJsonSingle());
        }

        #region test data

        private ISqlStoredProc MockLoadGalaxies()
        {
            List<dynamic> galaxyData = TestLoadGalaxyObject();

            Mock<IDataReader> mockReader = new Mock<IDataReader>();
            mockReader.Setup(x => x.FieldCount).Returns(3);
            mockReader.Setup(x => x.GetName(0)).Returns("id");
            mockReader.Setup(x => x.GetName(1)).Returns("name");
            mockReader.Setup(x => x.GetName(2)).Returns("desc");
            mockReader.Setup(x => x["id"]).Returns(0);
            mockReader.Setup(x => x.Read()).Callback
            (
                () =>
                {
                    int id = (int)mockReader.Object["id"];

                    string name = galaxyData[id].name;
                    string desc = galaxyData[id].desc;

                    mockReader.Setup(x => x["id"]).Returns(id + 1);
                    mockReader.Setup(x => x["name"]).Returns(name);
                    mockReader.Setup(x => x["desc"]).Returns(desc);

                    if (id == 2)
                    {
                        mockReader.Setup(x => x.Read()).Returns(false);
                    }
                }
            ).Returns(true);

            Mock<ISqlStoredProc> mockStoredProc = new Mock<ISqlStoredProc>(MockBehavior.Loose);
            mockStoredProc.Setup(x => x.ExcecRdr()).Returns(mockReader.Object);

            return mockStoredProc.Object;
        }

        private List<dynamic> TestLoadGalaxyObject()
        {
            List<dynamic> galaxies = new List<dynamic>();
            galaxies.Add(new { id = 1, name = "row1", desc = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." });
            galaxies.Add(new { id = 2, name = "row2", desc = "Nam hendrerit a metus in maximus." });
            galaxies.Add(new { id = 3, name = "row3", desc = "Donec sit amet urna sed turpis scelerisque lacinia." });

            return galaxies;
        }

        #endregion

        #endregion

        #region edit

        [TestMethod]
        [DataRow("name2","desc2")]
        [DataRow("", "")]
        [DataRow("000", "000")]
        public void EditInDB_ValidData_ReturnJson(string name,string desc)
        {
            //arrange
            Galaxy galaxy = new Galaxy(0, "name1", "desc1");
            string actual = JsonConvert.SerializeObject(new { id = 0, name = name, desc = desc });

            //act
            galaxy.EditInDB(name, desc, MockEditGalaxy());
            string expected = galaxy.ToJsonSingle();

            //logging
            Console.WriteLine("expected: {0}",expected);
            Console.WriteLine("actual: {0}", actual);

            //assert
            Assert.AreEqual(expected, actual);
        }

        private ISqlStoredProc MockEditGalaxy()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters.Add("@name", SqlDbType.VarChar);
            cmd.Parameters.Add("@desc", SqlDbType.VarChar);

            Mock<ISqlStoredProc> mockStoredProc = new Mock<ISqlStoredProc>(MockBehavior.Loose);
            mockStoredProc.Setup(x => x.ExcecSql());
            mockStoredProc.Setup(x => x.GetParams()).Returns(cmd.Parameters);

            return mockStoredProc.Object;
        }

        [TestMethod]
        [DataRow("name2", "desc1")]
        [DataRow("", "desc2")]
        [DataRow("000", "desc3")]
        public void EditInDB_NoDBConn_RevertChanges(string name, string desc)
        {
            //arrange
            Galaxy galaxy = new Galaxy(0, "name", "desc");
            string expected = JsonConvert.SerializeObject
            (
                new
                {
                    id = 0,
                    name = "name",
                    desc = "desc"
                }
            );

            try
            {
                //act
                galaxy.EditInDB(name,desc, new SqlStoredProc());

                //assert
                Assert.Fail();
            }
            catch (Exception e)
            {
                //test passes
            }
            string actual = galaxy.ToJsonSingle();

            //assert
            Assert.AreEqual(expected, actual);
        }

    }

    #endregion

    #endregion

}
