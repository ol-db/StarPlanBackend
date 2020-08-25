using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarPlan.Models.Space.Planets;
using StarPlan.Exceptions.PlanetExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using StarPlan.Models.Perks;
using System.Diagnostics;
using StarPlan.Exceptions.RegionExceptions;
using System.Data.SqlClient;
using Moq;
using System.Data;
using StarPlanDBAccess.Procedures;
using Newtonsoft.Json;
using StarPlan.Models;
using StarPlan.StarPlanConfig;

namespace UnitTesting.Star_Plan_Logic_Testing.Space_Logic_Testing
{
    [TestClass]
    public class PlanetTests
    {

        #region planet factory testing

        [TestMethod]
        public void GetPlanetFromSize_Dwarf_DwarfPlanet()
        {
            //arrange
            Planet planet;
            planet = PlanetFactory.GetPlanetFromSize(0, "dwarf");

            //act
            try
            {
                _ = (DwarfPlanet)planet;
            }
            catch (InvalidCastException ice) 
            {
                //assert
                Assert.Fail(ice.Message);
            }
        }

        [TestMethod]
        public void GetPlanetFromSize_Medium_MediumPlanet()
        {
            //arrange
            Planet planet;
            planet = PlanetFactory.GetPlanetFromSize(0, "medium");

            //act
            try
            {
                _ = (MediumPlanet)planet;
            }
            catch (InvalidCastException ice)
            {
                //assert
                Assert.Fail(ice.Message);
            }
        }

        [TestMethod]
        public void GetPlanetFromSize_Giant_GiantPlanet()
        {
            //arrange
            Planet planet;
            planet = PlanetFactory.GetPlanetFromSize(0, "giant");

            //act
            try
            {
                _ = (GiantPlanet)planet;
            }
            catch (InvalidCastException ice)
            {
                //assert
                Assert.Fail(ice.Message);
            }
        }

        [TestMethod]
        [DataRow("invalid")]
        [DataRow("")]
        public void GetPlanetFromSize_InvalidSize_ArgumentException(string planetType)
        {
            //arrange
            Planet planet;

            //act
            try
            {
                planet = PlanetFactory.GetPlanetFromSize(0, planetType);
                //assert
                Assert.Fail();
            }
            catch (ArgumentException ae)
            {
                //test passes
                Debug.Write(ae.Message);
            }
        }

        #endregion

        #region DB methods

        #region get

        [TestMethod]
        public void GetAllFromDB_ValidRecords_ReturnPlanetListJson()
        {
            //arrange
            ISqlStoredProc proc = MockLoadPlanets();
            PlanetList planets = new PlanetList(0);
            List<object> planetsExpected = TestLoadPlanetObject();
            string planetsExpectedJson = JsonConvert.SerializeObject(planetsExpected);

            //act
            planets.GetAllFromDB(proc);

            //logging
            Console.WriteLine("expected: {0}", planetsExpectedJson);
            Console.WriteLine("actual: {0}", planets.ToJson());

            //assert
            Assert.AreEqual(planetsExpectedJson, planets.ToJsonSingle());
        }

        #region test data

        private ISqlStoredProc MockLoadPlanets()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add("@systemId", SqlDbType.Int);

            List<dynamic> planetData = TestLoadPlanetObject();

            Mock<IDataReader> mockReader = new Mock<IDataReader>();
            mockReader.Setup(x => x.FieldCount).Returns(3);
            mockReader.Setup(x => x.GetName(0)).Returns("id");
            mockReader.Setup(x => x.GetName(1)).Returns("name");
            mockReader.Setup(x => x.GetName(2)).Returns("size");
            mockReader.Setup(x => x["id"]).Returns(0);
            mockReader.Setup(x => x.Read()).Callback
            (
                ()=>
                {
                    int id = (int)mockReader.Object["id"];

                    string name = planetData[id].name;
                    string size = planetData[id].size;

                    mockReader.Setup(x => x["id"]).Returns(id + 1);
                    mockReader.Setup(x => x["name"]).Returns(name);
                    mockReader.Setup(x => x["size"]).Returns(size);

                    if (id == 2)
                    {
                        mockReader.Setup(x => x.Read()).Returns(false);
                    }
                }
            ).Returns(true);

            Mock<ISqlStoredProc> mockStoredProc = new Mock<ISqlStoredProc>(MockBehavior.Loose);
            mockStoredProc.Setup(x => x.GetParams()).Returns(cmd.Parameters);
            mockStoredProc.Setup(x => x.ExcecRdr()).Returns(mockReader.Object);

            return mockStoredProc.Object;
        }

        private List<dynamic> TestLoadPlanetObject()
        {
            List<dynamic> planets = new List<dynamic>();
            planets.Add(new { id = 1, name = "row1", size = "giant" });
            planets.Add(new { id = 2, name = "row2", size = "dwarf" });
            planets.Add(new { id = 3, name = "row3", size = "medium" });

            return planets;
        }

        #endregion

        #endregion

        #region edit

        [TestMethod]
        [DataRow("name2","dwarf")]
        [DataRow("","medium")]
        [DataRow("000","giant")]
        public void EditInDB_ValidData_ReturnJson(string name, string size)
        {
            //arrange
            Planet planet = PlanetFactory.GetPlanetFromSize(0, size);
            string actual = JsonConvert.SerializeObject
            (
                new
                {
                    id = 0,
                    name = name,
                    size = Planet.PlanetToSizeString(planet)
                }
            );

            //act
            planet.EditInDB(name, MockEditPlanet());
            string expected = planet.ToJsonSingle();

            //logging
            Console.WriteLine("expected: {0}", expected);
            Console.WriteLine("actual: {0}", actual);

            //assert
            Assert.AreEqual(expected, actual);
        }

        #region test data

        private ISqlStoredProc MockEditPlanet()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters.Add("@name", SqlDbType.VarChar);
            cmd.Parameters.Add("@size", SqlDbType.VarChar);

            Mock<ISqlStoredProc> mockStoredProc = new Mock<ISqlStoredProc>(MockBehavior.Loose);
            mockStoredProc.Setup(x => x.ExcecSql());
            mockStoredProc.Setup(x => x.GetParams()).Returns(cmd.Parameters);

            return mockStoredProc.Object;
        }

        #endregion

        [TestMethod]
        [DataRow("name1","name2", "dwarf")]
        [DataRow("name1","", "medium")]
        [DataRow("name1","000", "giant")]
        public void EditInDB_NoDBConn_RevertChanges(string nameInit, string nameChange, string size)
        {
            //arrange
            Planet planet = PlanetFactory.GetPlanetFromSize(0, nameInit, size);
            string actual = JsonConvert.SerializeObject
            (
                new
                {
                    id = 0,
                    name = "name1",
                    size = Planet.PlanetToSizeString(planet)
                }
            );

            try
            {
                //act
                planet.EditInDB(nameChange, new SqlStoredProc());

                //assert
                Assert.Fail();
            }
            catch(Exception e)
            {
                //test passes
            }
            string expected = planet.ToJsonSingle();

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #endregion

    }
}
