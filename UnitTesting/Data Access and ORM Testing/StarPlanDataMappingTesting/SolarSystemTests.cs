using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarPlan.DataAccess;
using StarPlan.Models;
using StarPlanDBAccess.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace UnitTesting.Data_Access_and_ORM_Testing.StarPlanDataMappingTesting
{
    [TestClass]
    public class SolarSystemTests
    {
        [DynamicData("SolarSystemTestData")]
        [TestMethod]
        public void SetSolarSystemParams_ValidParams_ReturnParamNames(int id)
        {
            //arrange
            string paramIdName = "@id";

            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add(paramIdName, SqlDbType.Int);

            //act
            SpaceAccess.SetSolarSystemParams(
                new List<Tuple<object, SolarSystem.FeildType>>() {
                    new Tuple<object, SolarSystem.FeildType>(id, SolarSystem.FeildType.ID)
                },
                cmd.Parameters);

            int actualId = (int)cmd.Parameters[paramIdName].Value;

            //assert
            Assert.AreEqual(actualId, id);
        }

        [DynamicData("SolarSystemTestData")]
        [TestMethod]
        public void SetSolarSystemParams_InvalidParams_ReturnParamNames(int id)
        {
            //arrange
            string paramIdName = "@id_";

            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add(paramIdName, SqlDbType.Int);

            try
            {
                //act
                SpaceAccess.SetSolarSystemParams(
                    new List<Tuple<object, SolarSystem.FeildType>>() {
                    new Tuple<object, SolarSystem.FeildType>(id, SolarSystem.FeildType.ID)
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

        public static IEnumerable<object[]> SolarSystemTestData
        {
            get
            {
                return new[]
                {
                    new object[] {0},
                    new object[] {1},
                    new object[] {2},
                    new object[] {3},
                    new object[] {4},
                    new object[] {5}
                };
            }
        }
    }
}
