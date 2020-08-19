using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using StarPlan.Exceptions.GalaxyExceptions;

namespace StarPlan.Models
{
    /// <todo>
    ///     add binary searching
    ///     for searching through records
    /// </todo>
    public class GalaxyList
    {
        private List<Galaxy> galaxies;

        public GalaxyList() {
            this.galaxies = new List<Galaxy>();
        }

        #region DB methods

        public void LoadMapFromDB(SqlConnection conn)
        {
            //gets denormalised database to read
            //with all galaxies, solar systems, planets and regions
            using (SqlCommand cmd = new SqlCommand("LoadMap", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        //reads until the final record
                        while (reader.Read())
                        {
                            int id = reader.GetInt16("galaxyId");
                            try
                            {
                                GetGalaxyById(id).LoadMapFromDB(reader);
                            }
                            catch (GalaxyNotFound gnf)
                            {
                                AddGalaxy(new Galaxy(id)).LoadMapFromDB(reader);
                            }
                        }
                    }
                }
                catch (SqlException se)
                {
                    throw new InvalidOperationException("something went wrong");
                }
                conn.Close();
            }
        }

        #endregion

        #region in memory methods

        public Galaxy GetGalaxyById(int id)
        {
            foreach (Galaxy galaxy in galaxies) {
                if (galaxy.GetId() == id) {
                    return galaxy;
                }
            }
            throw new GalaxyNotFound(id);
        }

        private Galaxy AddGalaxy(Galaxy galaxy)
        {
            galaxies.Add(galaxy);
            return galaxy;
        }

        #endregion

        #region representation

        override
        public string ToString()
        {
            string ToString = "";
            foreach (Galaxy galaxy in galaxies) {
                ToString += galaxy.ToString();
            }
            return ToString;
        }

        #endregion
    }
}
