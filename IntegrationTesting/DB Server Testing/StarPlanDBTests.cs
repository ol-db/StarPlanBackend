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

namespace IntegrationTesting.DB_Server_Testing
{
    [TestClass]
    public class StarPlanDBTests
    {
        /// <summary>
        ///     updates first record of Planet
        ///     then reverts changes
        /// </summary>
        
        [DynamicData("PlanetTestData")]
        [TestMethod]
        public void UpdatePlanet_FirstRecord_PlanetUpdated(string name,string size)
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
                    using (SqlCommand cmd = new SqlCommand("EditThenLoadFirstPlanet_Test", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        //gets param info from proc in DB
                        SqlCommandBuilder.DeriveParameters(cmd);

                        //set up params
                        SpaceAccess.SetPlanetParams(
                            new List<Tuple<object, Planet.FeildType>>() {
                                new Tuple<object, Planet.FeildType>(name,Planet.FeildType.NAME),
                                new Tuple<object, Planet.FeildType>(size, Planet.FeildType.SIZE)
                            }, 
                            cmd.Parameters);

                        SqlDataReader reader = cmd.ExecuteReader();

                        //read record
                        reader.Read();

                        //get current record
                        alteredName = SpaceAccess.GetPlanetFeild_FromReader(
                            Planet.FeildType.NAME,reader);
                        alteredSize = SpaceAccess.GetPlanetFeild_FromReader(
                            Planet.FeildType.SIZE,reader);

                        reader.Close();

                    }
                }
                catch (Exception se)
                {
                    throw new InvalidOperationException("planet was not altered");
                }

                //assert
                Assert.AreEqual(size, alteredSize);
                Assert.AreEqual(name, alteredName);

                //logging
                Console.WriteLine("size\n" + "expected: " + size + " actual: " + alteredSize);
                Console.WriteLine("name\n" + "expected: " + name + " actual: " + alteredName);

                conn.Close();
            }
        }

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
    }
}
