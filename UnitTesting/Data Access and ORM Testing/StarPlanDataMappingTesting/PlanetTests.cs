using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StarPlan.DataAccess;
using StarPlan.Models.Space.Planets;
using StarPlanDBAccess.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace UnitTesting.Data_Access_and_ORM_Testing.StarPlanDataMappingTesting
{
    [TestClass]
    public class PlanetTests
    {
        [DynamicData("PlanetTestData")]
        [TestMethod]
        public void SetPlanetParams_ValidParams_ReturnParamNames(int id, string name,string size)
        {
            //arrange
            string paramIdName = "@id";
            string paramSizeName = "@size";
            string paramNameName = "@name";

            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add(paramIdName, SqlDbType.Int);
            cmd.Parameters.Add(paramNameName, SqlDbType.VarChar);
            cmd.Parameters.Add(paramSizeName, SqlDbType.VarChar);

            //act
            SpaceAccess.SetPlanetParams(
                new List<Tuple<object, Planet.FeildType>>() {
                    new Tuple<object, Planet.FeildType>(id, Planet.FeildType.ID),
                    new Tuple<object, Planet.FeildType>(name,Planet.FeildType.NAME),
                    new Tuple<object, Planet.FeildType>(size, Planet.FeildType.SIZE)
                },
                cmd.Parameters);

            int actualId = (int)cmd.Parameters[paramIdName].Value;
            string actualName = (string)cmd.Parameters[paramNameName].Value;
            string actualSize = (string)cmd.Parameters[paramSizeName].Value;

            //assert
            Assert.AreEqual(actualId, id);
            Assert.AreEqual(actualName, name);
            Assert.AreEqual(actualSize, size);
        }

        [DynamicData("PlanetTestData")]
        [TestMethod]
        public void SetPlanetParams_InvalidParams_ReturnParamNames(int id,string name, string size)
        {
            //arrange
            string paramIdName = "@id_";
            string paramSizeName = "@size_";
            string paramNameName = "@name_";

            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add(paramIdName, SqlDbType.Int);
            cmd.Parameters.Add(paramNameName, SqlDbType.VarChar);
            cmd.Parameters.Add(paramSizeName, SqlDbType.VarChar);

            try
            {
                //act
                SpaceAccess.SetPlanetParams(
                    new List<Tuple<object, Planet.FeildType>>() {
                    new Tuple<object, Planet.FeildType>(id, Planet.FeildType.ID),
                    new Tuple<object, Planet.FeildType>(name,Planet.FeildType.NAME),
                    new Tuple<object, Planet.FeildType>(size, Planet.FeildType.SIZE)
                    },
                    cmd.Parameters);

                //assert
                Assert.Fail("parameters shouldn't match");
            }
            catch (ParamNotFound pnf)
            {
                //test passes
            }
        }

        public static IEnumerable<object[]> PlanetTestData
        {
            get
            {
                return new[]
                {
                    new object[] {0, "test",Planet.PlanetSizeTypeToString(Planet.SizeTypes.DWARF) },
                    new object[] {1, "test",Planet.PlanetSizeTypeToString(Planet.SizeTypes.MEDIUM) },
                    new object[] {2, "test",Planet.PlanetSizeTypeToString(Planet.SizeTypes.GIANT) },
                    new object[] {3, "",Planet.PlanetSizeTypeToString(Planet.SizeTypes.DWARF) },
                    new object[] {4, "",Planet.PlanetSizeTypeToString(Planet.SizeTypes.MEDIUM) },
                    new object[] {5, "",Planet.PlanetSizeTypeToString(Planet.SizeTypes.GIANT) }
                };
            }
        }
    }
}
