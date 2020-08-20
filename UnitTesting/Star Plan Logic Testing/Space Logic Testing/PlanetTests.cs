using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarPlan.Models.Space.Planets;
using StarPlan.Exceptions.PlanetExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using StarPlan.Models.Perks;
using System.Diagnostics;
using StarPlan.Exceptions.RegionExceptions;

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
    }
}
