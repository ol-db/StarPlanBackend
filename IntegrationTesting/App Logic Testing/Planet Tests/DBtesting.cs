using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarPlan.Models;
using StarPlan.Models.Space.Planets;
using StarPlan.StarPlanConfig;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace IntegrationTesting.App_Logic_Testing.Planet_Tests
{
    /// <todo>
    ///     structure integration tests
    /// </todo>
    [TestClass]
    public class DBtesting
    {
        [TestMethod]
        [DataRow("name")]
        [DataRow("namenamename")]
        [DataRow("")]
        [DataRow("06")]
        public void EditPlanetTest(string name)
        {
            //<todo>
            //  prevent hard coding of DB names
            //</todo>
            string connStr = StarPlanConfig.GetDB("StarPlanDB");

            //reference to planet where id=0
            DwarfPlanet planet = new DwarfPlanet(0, "");
            using (SqlConnection conn = new SqlConnection(
                connStr
                ))
            {
                planet.Edit(name, conn);
            }
        }
    }
}
