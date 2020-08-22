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
    public class GalaxyTests
    {
        [DynamicData("GalaxyTestData")]
        [TestMethod]
        public void SetGalaxyParams_ValidParams_ReturnParamNames(int id, string name, string desc)
        {
            //arrange
            string paramIdName = "@id";
            string paramNameName = "@name";
            string paramDescName = "@desc";

            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add(paramIdName, SqlDbType.Int);
            cmd.Parameters.Add(paramNameName, SqlDbType.VarChar);
            cmd.Parameters.Add(paramDescName, SqlDbType.VarChar);

            //act
            SpaceAccess.SetGalaxyParams(
                new List<Tuple<object, Galaxy.FeildType>>() {
                    new Tuple<object, Galaxy.FeildType>(id, Galaxy.FeildType.ID),
                    new Tuple<object, Galaxy.FeildType>(name,Galaxy.FeildType.NAME),
                    new Tuple<object, Galaxy.FeildType>(desc, Galaxy.FeildType.DESC)
                },
                cmd.Parameters);

            int actualId = (int)cmd.Parameters[paramIdName].Value;
            string actualName = (string)cmd.Parameters[paramNameName].Value;
            string actualDesc = (string)cmd.Parameters[paramDescName].Value;

            //assert
            Assert.AreEqual(actualId, id);
            Assert.AreEqual(actualName, name);
            Assert.AreEqual(actualDesc, desc);
        }

        [DynamicData("GalaxyTestData")]
        [TestMethod]
        public void SetGalaxyParams_InvalidParams_ReturnParamNames(int id, string name, string desc)
        {
            //arrange
            string paramIdName = "@id_";
            string paramNameName = "@name_";
            string paramDescName = "@desc_";

            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add(paramIdName, SqlDbType.Int);
            cmd.Parameters.Add(paramNameName, SqlDbType.VarChar);
            cmd.Parameters.Add(paramDescName, SqlDbType.VarChar);

            try
            {
                //act
                SpaceAccess.SetGalaxyParams(
                    new List<Tuple<object, Galaxy.FeildType>>() {
                    new Tuple<object, Galaxy.FeildType>(id, Galaxy.FeildType.ID),
                    new Tuple<object, Galaxy.FeildType>(name,Galaxy.FeildType.NAME),
                    new Tuple<object, Galaxy.FeildType>(desc, Galaxy.FeildType.DESC)
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

        public static IEnumerable<object[]> GalaxyTestData
        {
            get
            {
                return new[]
                {
                    new object[] {0, "test","desc desc desc desc"},
                    new object[] {1, "test","desc desc desc desc"},
                    new object[] {2, "test","desc desc desc desc"},
                    new object[] {3, "","desc desc desc desc"},
                    new object[] {4, "","desc desc desc desc"},
                    new object[] {5, "", "desc desc desc desc" }
                };
            }
        }
    }
}
