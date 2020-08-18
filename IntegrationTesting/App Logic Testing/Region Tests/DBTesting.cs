using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarPlan.StarPlanConfig;
using StarPlan.Models.Space.Planets;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace IntegrationTesting.App_Logic_Testing.Region_Tests
{
    [TestClass]
    public class DBTesting
    {
        [TestMethod]
        [DataRow("name")]
        public void EditRegionTest(string name)
        {
            //<todo>
            //  prevent hard coding of DB names
            //</todo>
            string connStr = StarPlanConfig.GetDB("StarPlanDB");

            //reference to planet where id=0
            Region region = new Region(0, "");
            using (SqlConnection conn = new SqlConnection(
                connStr
                ))
            {
                region.Edit(name, conn);
            }
        }
    }
}
