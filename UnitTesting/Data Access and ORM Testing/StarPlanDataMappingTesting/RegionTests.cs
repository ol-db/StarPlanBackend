using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class RegionTests
    {
        [DynamicData("RegionTestData")]
        [TestMethod]
        public void SetRegionParams_ValidParams_ReturnParamNames(int id, string name)
        {
            //arrange
            string paramIdName = "@id";
            string paramNameName = "@name";

            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add(paramIdName, SqlDbType.Int);
            cmd.Parameters.Add(paramNameName, SqlDbType.VarChar);

            //act
            SpaceAccess.SetRegionParams(
                new List<Tuple<object, Region.FeildType>>() {
                    new Tuple<object, Region.FeildType>(id, Region.FeildType.ID),
                    new Tuple<object, Region.FeildType>(name,Region.FeildType.NAME)
                },
                cmd.Parameters);

            int actualId = (int)cmd.Parameters[paramIdName].Value;
            string actualName = (string)cmd.Parameters[paramNameName].Value;

            //assert
            Assert.AreEqual(actualId, id);
            Assert.AreEqual(actualName, name);
        }

        [DynamicData("RegionTestData")]
        [TestMethod]
        public void SetRegionParams_InvalidParams_ReturnParamNames(int id,string name)
        {
            //arrange
            string paramIdName = "@id_";
            string paramNameName = "@name_";

            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add(paramIdName, SqlDbType.Int);
            cmd.Parameters.Add(paramNameName, SqlDbType.VarChar);

            try
            {
                //act
                SpaceAccess.SetRegionParams(
                    new List<Tuple<object, Region.FeildType>>() {
                    new Tuple<object, Region.FeildType>(id, Region.FeildType.ID),
                    new Tuple<object, Region.FeildType>(name,Region.FeildType.NAME)
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

        public static IEnumerable<object[]> RegionTestData
        {
            get
            {
                return new[]
                {
                    new object[] { 0,"test"},
                    new object[] { 1,"test"},
                    new object[] { 2,"test" },
                    new object[] { 3,""},
                    new object[] { 4,""},
                    new object[] { 5,""}
                };
            }
        }
    }
}
