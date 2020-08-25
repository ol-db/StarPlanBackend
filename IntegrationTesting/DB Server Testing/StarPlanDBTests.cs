using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarPlan.Models.Space.Planets;
using StarPlan.StarPlanConfig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using StarPlanDBAccess.ORM;
using StarPlan.DataAccess;
using StarPlanDBAccess.Procedures;
using StarPlan.Models;

namespace IntegrationTesting.DB_Server_Testing
{
    [TestClass]
    public class StarPlanDBTests
    {
        /// <summary>
        ///     updates first record of Planet
        ///     then reverts changes
        /// </summary>

        #region planet

        [DynamicData("PlanetTestData")]
        [TestMethod]
        public void UpdatePlanet_FirstRecord_PlanetUpdated(string name, string size)
        {

            //values from the altered record
            string alteredName;
            string alteredSize;

            using (SqlConnection conn = new SqlConnection(StarPlanConfig.GetDB("StarPlanDB")))
            {

                conn.Open();

                //alter previous record
                try
                {
                    SqlStoredProc proc = new SqlStoredProc(conn);
                    proc.SetProcName("EditThenLoadFirstPlanet_Test");

                    //set up params
                    SpaceAccess.SetPlanetParams(
                        new List<Tuple<object, Planet.FeildType>>() {
                            new Tuple<object, Planet.FeildType>(name,Planet.FeildType.NAME),
                            new Tuple<object, Planet.FeildType>(size, Planet.FeildType.SIZE)
                        },
                        proc.GetParams());

                    IDataReader reader = proc.ExcecRdr();

                    //read record
                    reader.Read();

                    //get current record
                    alteredName = SpaceAccess.GetPlanetFeild_FromReader(
                        Planet.FeildType.NAME, reader);
                    alteredSize = SpaceAccess.GetPlanetFeild_FromReader(
                        Planet.FeildType.SIZE, reader);

                    reader.Close();
                }
                catch (Exception se)
                {
                    throw new InvalidOperationException("planet was not altered");
                }

                //logging
                Console.WriteLine("size\n" + "expected: " + size + " actual: " + alteredSize);
                Console.WriteLine("name\n" + "expected: " + name + " actual: " + alteredName);

                //assert
                Assert.AreEqual(size, alteredSize);
                Assert.AreEqual(name, alteredName);

                conn.Close();
            }
        }

        #region test data

        public static IEnumerable<object[]> PlanetTestData
        {
            get
            {
                return new[]
                {
                    new object[] { "test",Planet.PlanetSizeTypeToString(Planet.SizeTypes.DWARF) },
                    new object[] { "test",Planet.PlanetSizeTypeToString(Planet.SizeTypes.MEDIUM) },
                    new object[] { "test",Planet.PlanetSizeTypeToString(Planet.SizeTypes.GIANT) },
                    new object[] { "",Planet.PlanetSizeTypeToString(Planet.SizeTypes.DWARF) },
                    new object[] { "",Planet.PlanetSizeTypeToString(Planet.SizeTypes.MEDIUM) },
                    new object[] { "",Planet.PlanetSizeTypeToString(Planet.SizeTypes.GIANT) }
                };
            }
        }

        #endregion

        #endregion

        #region region

        [DataRow("name1")]
        [DataRow("")]
        [DataRow("000")]
        [TestMethod]
        public void UpdateRegion_FirstRecord_RegionUpdated(string name)
        {

            //values from the altered record
            string alteredName;

            using (SqlConnection conn = new SqlConnection(StarPlanConfig.GetDB("StarPlanDB")))
            {

                conn.Open();

                //alter previous record
                try
                {
                    SqlStoredProc proc = new SqlStoredProc(conn);
                    proc.SetProcName("EditThenLoadFirstRegion_Test");

                    //set up param
                    SpaceAccess.SetRegionParam(
                        new Tuple<object, Region.FeildType>(name, Region.FeildType.NAME),
                        proc.GetParams());

                    IDataReader reader = proc.ExcecRdr();

                    //read record
                    reader.Read();

                    //get current record
                    alteredName = SpaceAccess.GetRegionFeild_FromReader(
                        Region.FeildType.NAME, reader);

                    reader.Close();
                }
                catch (Exception se)
                {
                    throw new InvalidOperationException("region was not altered");
                }

                //logging
                Console.WriteLine("name\n" + "expected: " + name + " actual: " + alteredName);

                //assert
                Assert.AreEqual(name, alteredName);

                conn.Close();
            }
        }

        #endregion

        #region galaxy

        [DataRow("name1","desc1")]
        [DataRow("","desc2 desc 2 desc 2")]
        [DataRow("000","000000__000")]
        [TestMethod]
        public void UpdateGalaxy_FirstRecord_GalaxyUpdated(string name,string desc)
        {

            //values from the altered record
            string alteredName;
            string alteredDesc;

            using (SqlConnection conn = new SqlConnection(StarPlanConfig.GetDB("StarPlanDB")))
            {

                conn.Open();

                //alter previous record
                try
                {
                    SqlStoredProc proc = new SqlStoredProc(conn);
                    proc.SetProcName("EditThenLoadFirstGalaxy_Test");

                    //set up param
                    SpaceAccess.SetGalaxyParams(
                        new List<Tuple<object, Galaxy.FeildType>>()
                        {
                            new Tuple<object, Galaxy.FeildType>(name, Galaxy.FeildType.NAME),
                            new Tuple<object, Galaxy.FeildType>(desc, Galaxy.FeildType.DESC)
                        },
                        proc.GetParams());

                    IDataReader reader = proc.ExcecRdr();

                    //read record
                    reader.Read();

                    //get current record
                    alteredName = SpaceAccess.GetGalaxyFeild_FromReader(
                        Galaxy.FeildType.NAME, reader);
                    alteredDesc = SpaceAccess.GetGalaxyFeild_FromReader(
                        Galaxy.FeildType.DESC, reader);

                    reader.Close();
                }
                catch (Exception se)
                {
                    throw new InvalidOperationException("galaxy was not altered");
                }

                //logging
                Console.WriteLine("name\n" + "expected: " + name + " actual: " + alteredName);
                Console.WriteLine("desc\n" + "expected: " + desc + " actual: " + alteredDesc);

                //assert
                Assert.AreEqual(name, alteredName);
                Assert.AreEqual(desc, alteredDesc);

                conn.Close();
            }
        }

        #endregion

    }
}
