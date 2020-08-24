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

        [TestMethod]
        public void fdsafsda()
        {
            Mock<ISqlStoredProc> mockStoredProc = new Mock<ISqlStoredProc>(MockBehavior.Loose);

            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add("foo", SqlDbType.Int);
            cmd.Parameters.Add("bar", SqlDbType.Int);

            Mock<IDataReader> mockReader = new Mock<IDataReader>();
            mockReader.Setup(x => x["id"]).Returns(0);
            mockReader.Setup(x => x["name"]).Returns("hello");
            mockReader.Setup(x => x["size"]).Returns("dwarf");

            IDataReader rdr = mockReader.Object;

            mockStoredProc.Setup(x => x.GetCmd()).Returns(cmd);
            mockStoredProc.Setup(x => x.ExcecRdr()).Returns(rdr);

            ISqlStoredProc proc = mockStoredProc.Object;

            SqlCommand cmd_ = proc.GetCmd();

            IDataReader rdr_ = proc.ExcecRdr();

            int id = (int)rdr_["id"];
        }


        #endregion
    }
}
