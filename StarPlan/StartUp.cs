using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using StarPlan.Models;
using StarPlan.Models.Space.Planets;
using StarPlan.StarPlanConfig;

namespace StarPlan
{
    public class StartUp
    {
        /// <summary>
        /// start up class
        /// where the main thread is run
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            GalaxyList galaxies = new GalaxyList();
            using (SqlConnection conn = new SqlConnection(StarPlanConfig.StarPlanConfig.GetDB("StarPlanDB")))
            {
                conn.Open();
                galaxies.LoadMapFromDB(conn);
                conn.Close();
            }

            throw new NotImplementedException("nothing to start...");
        }
    }
}
