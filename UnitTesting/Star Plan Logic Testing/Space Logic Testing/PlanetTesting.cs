using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarPlan.Models.Space.Planets;
using StarPlan.Exceptions.PlanetExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using StarPlan.Models.Perks;
using System.Diagnostics;

namespace UnitTesting.Star_Plan_Logic_Testing.Space_Logic_Testing
{
    [TestClass]
    public class PlanetTesting
    {
        #region test for region count
        #region dwarf planet
        /// <summary>
        /// tests for regions being in the range 2-3
        /// </summary>
        /// <param name="numOfRegions"></param>
        [DataRow(1)] //boundary
        [DataRow(4)] //boundary
        [DataRow(0)] //valid
        [DataRow(10)] //valid
        [TestMethod]
        public void VerifyDwarfPlanetRegionsInvalid(int numOfRegions) {
            Planet dwarfPlanet = new DwarfPlanet(10);
            RegionList regions = new RegionList(10);


            try
            {
                for (int i=0;i<numOfRegions;i++) {
                    regions.AddRegion(new Region(i, "newName"));
                }
                dwarfPlanet.SetRegions(regions);
                Assert.Fail();
            }
            catch (RegionOutOfRangeException roore)
            {
                Console.Write(roore.Message);
            }
        }

        /// <summary>
        /// tests for regions being in the range 2-3
        /// </summary>
        /// <param name="numOfRegions"></param>
        [DataRow(2)] //boundary
        [DataRow(3)] //boundary
        [TestMethod]
        public void VerifyDwarfPlanetRegionsValid(int numOfRegions)
        {
            Planet dwarfPlanet = new DwarfPlanet(10);
            RegionList regions = new RegionList(10);
            for (int i = 0; i < numOfRegions; i++)
            {
                regions.AddRegion(new Region(i, "newName"));
            }
            dwarfPlanet.SetRegions(regions);
        }
        #endregion
        #region medium planet
        /// <summary>
        /// tests for regions being in the range 4-6
        /// </summary>
        /// <param name="numOfRegions"></param>

        #region data
        [DataRow(3)] //boundary
        [DataRow(7)] //boundary
        
        [DataRow(10)] //valid
        [DataRow(8)] //valid

        [DataRow(0)] //extreme
        [DataRow(100)] //extreme
        #endregion

        [TestMethod]
        public void VerifyMediumPlanetRegionsInvalid(int numOfRegions)
        {
            Planet mediumPlanet = new MediumPlanet(10);
            RegionList regions = new RegionList(10);


            try
            {
                for (int i = 0; i < numOfRegions; i++)
                {
                    regions.AddRegion(new Region(i, "newName"));
                }
                mediumPlanet.SetRegions(regions);
                Assert.Fail();
            }
            catch (RegionOutOfRangeException roore)
            {
                Console.Write(roore.Message);
            }
        }

        /// <summary>
        /// tests for regions being in the range 4-6
        /// </summary>
        /// <param name="numOfRegions"></param>

        #region data
        [DataRow(4)] //boundary
        [DataRow(6)] //boundary

        [DataRow(5)] //valid
        #endregion

        [TestMethod]
        public void VerifyMediumPlanetRegionsValid(int numOfRegions)
        {
            Planet mediumPlanet = new MediumPlanet(10);
            RegionList regions = new RegionList(10);
            for (int i = 0; i < numOfRegions; i++)
            {
                regions.AddRegion(new Region(i, "newName"));
            }
            mediumPlanet.SetRegions(regions);
        }
        #endregion
        #region giant planet
        /// <summary>
        /// tests for regions being in the range 7-15
        /// </summary>
        /// <param name="numOfRegions"></param>

        #region data
        [DataRow(6)] //boundary
        [DataRow(16)] //boundary

        [DataRow(3)] //valid
        [DataRow(20)] //valid

        [DataRow(0)] //extreme
        [DataRow(100)] //extreme
        #endregion

        [TestMethod]
        public void VerifyGiantPlanetRegionsInvalid(int numOfRegions)
        {
            Planet giantPlanet = new GiantPlanet(10);
            RegionList regions = new RegionList(10);


            try
            {
                for (int i = 0; i < numOfRegions; i++)
                {
                    regions.AddRegion(new Region(i, "newName"));
                }
                giantPlanet.SetRegions(regions);
                Assert.Fail();
            }
            catch (RegionOutOfRangeException roore)
            {
                Console.Write(roore.Message);
            }
        }

        /// <summary>
        /// tests for regions being in the range 7-15
        /// </summary>
        /// <param name="numOfRegions"></param>

        #region data
        [DataRow(7)] //boundary
        [DataRow(15)] //boundary

        [DataRow(8)] //valid
        [DataRow(10)] //valid
        [DataRow(12)] //valid

        #endregion

        [TestMethod]
        public void VerifyGiantPlanetRegionsValid(int numOfRegions)
        {
            Planet giantPlanet = new GiantPlanet(10);
            RegionList regions = new RegionList(10);
            for (int i = 0; i < numOfRegions; i++)
            {
                regions.AddRegion(new Region(i, "newName"));
            }
            giantPlanet.SetRegions(regions);
        }
        #endregion
        #endregion

        #region test for perk count
        #region dwarf planet
        /// <summary>
        /// tests for perk being 1
        /// </summary>
        /// <param name="numOfRegions"></param>

        [DataRow(2)] //boundary
        [DataRow(0)] //boundary

        [DataRow(5)] //valid
        [DataRow(6)] //valid

        [DataRow(100)] //extreme

        [TestMethod]
        public void VerifyDwarfPlanetPerksInvalid(int numOfPerks)
        {
            Planet dwarfPlanet = new DwarfPlanet(10);
            PerkList perks = new PerkList(10);
            try
            {
                for (int i = 0; i < numOfPerks; i++)
                {
                    perks.AddPerk(new Perk(i));
                }
                dwarfPlanet.SetPerks(perks);
                Assert.Fail();
            }
            catch (PerkCountOutOfRangeException pcoore)
            {
                Console.Write(pcoore.Message);
            }
        }

        /// <summary>
        /// tests for perk being 1
        /// </summary>
        /// <param name="numOfRegions"></param>

        [TestMethod]
        public void VerifyDwarfPlanetPerksValid()
        {
            Planet dwarfPlanet = new DwarfPlanet(10);
            PerkList perks = new PerkList(10);
            perks.AddPerk(new Perk(0));
            dwarfPlanet.SetPerks(perks);
        }
        #endregion
        #region medium planet
        /// <summary>
        /// tests for perk being 3
        /// </summary>
        /// <param name="numOfRegions"></param>

        [DataRow(4)] //boundary
        [DataRow(2)] //boundary

        [DataRow(1)] //valid
        [DataRow(6)] //valid

        [DataRow(100)] //extreme
        [DataRow(0)] //extreme

        [TestMethod]
        public void VerifyMediumPlanetPerksInvalid(int numOfPerks)
        {
            Planet mediumPlanet = new MediumPlanet(10);
            PerkList perks = new PerkList(10);

            try
            {
                for (int i = 0; i < numOfPerks; i++)
                {
                    perks.AddPerk(new Perk(i));
                }
                mediumPlanet.SetPerks(perks);
                Assert.Fail();
            }
            catch (PerkCountOutOfRangeException pcoore)
            {
                Console.Write(pcoore.Message);
            }
        }

        /// <summary>
        /// tests for perk being 3
        /// </summary>
        /// <param name="numOfRegions"></param>

        [TestMethod]
        public void VerifyMediumPlanetPerksValid()
        {
            Planet mediumPlanet = new MediumPlanet(10);
            PerkList perks = new PerkList(10);

            for(int i = 0; i < 3; i++)
            {
                perks.AddPerk(new Perk(i));
            }
            mediumPlanet.SetPerks(perks);
        }
        #endregion
        #region giant planet
        /// <summary>
        /// tests for perk being 6
        /// </summary>
        /// <param name="numOfRegions"></param>

        [DataRow(7)] //boundary
        [DataRow(5)] //boundary

        [DataRow(3)] //valid
        [DataRow(10)] //valid

        [DataRow(100)] //extreme
        [DataRow(0)] //extreme

        [TestMethod]
        public void VerifyGiantPlanetPerksInvalid(int numOfPerks)
        {
            Planet giantPlanet = new GiantPlanet(10);
            PerkList perks = new PerkList(10);

            try
            {
                for (int i = 0; i < numOfPerks; i++)
                {
                    perks.AddPerk(new Perk(i));
                }
                giantPlanet.SetPerks(perks);
                Assert.Fail();
            }
            catch (PerkCountOutOfRangeException pcoore)
            {
                Console.Write(pcoore.Message);
            }
        }

        /// <summary>
        /// tests for perk being 6
        /// </summary>
        /// <param name="numOfRegions"></param>

        [TestMethod]
        public void VerifyGiantPlanetPerksValid()
        {
            Planet giantPlanet = new GiantPlanet(10);
            PerkList perks = new PerkList(10);

            for (int i = 0; i < 6; i++)
            {
                perks.AddPerk(new Perk(i));
            }
            giantPlanet.SetPerks(perks);
        }
        #endregion
        #endregion

        #region planet factory testing

        [TestMethod]
        public void GenerateDwarfPlanet()
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
        public void GenerateMediumPlanet()
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
        public void GenerateGiantPlanet()
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
        public void GenerateInvalidPlanet(string planetType)
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
