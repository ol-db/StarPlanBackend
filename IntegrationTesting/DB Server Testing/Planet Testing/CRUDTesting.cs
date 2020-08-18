using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarPlan.StarPlanConfig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;

namespace IntegrationTesting.DB_Server_Testing.Planet_Testing
{
    [TestClass]
    public class CRUDTesting
    {
        [TestMethod]
        [DataRow(0,"name","giant")]
        public void EditPlanet(int id,string name,string size) 
        {
            //<todo>
            //  prevent hard coding of DB names
            //</todo>
            string connStr = StarPlanConfig.GetDB("StarPlanDB");
            using (SqlConnection conn = new SqlConnection(
                connStr
                ))
            {

                using (SqlCommand cmd = new SqlCommand("EditPlanet", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("@size", SqlDbType.VarChar).Value = size;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }
    }
}
