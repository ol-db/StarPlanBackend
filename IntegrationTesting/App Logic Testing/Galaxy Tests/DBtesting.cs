using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarPlan.Models;
using StarPlan.StarPlanConfig;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace IntegrationTesting.App_Logic_Testing.Galaxy_Tests
{
    [TestClass]
    public class DBtesting
    {
        [TestMethod]
        [DataRow("name", "desc desc desc desc desc desc desc desc desc")]
        public void EditGalaxyTest(string name, string desc)
        {
            //<todo>
            //  prevent hard coding of DB names
            //</todo>
            string connStr = StarPlanConfig.GetDB("StarPlanDB");
        }
    }
}
