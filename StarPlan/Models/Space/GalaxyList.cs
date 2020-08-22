using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using StarPlan.Exceptions.GalaxyExceptions;
using StarPlanDBAccess.ORM;
using StarPlan.DataAccess;

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

        //<todo>
        //  add exception for no galaxies
        //<todo/>

        /// <summary>
        ///     reads 
        /// </summary>
        /// <param name="conn"></param>

        public void LoadMapFromDB(SqlConnection conn)
        {
            GetAllFromDB(conn);
            foreach (Galaxy galaxy in galaxies)
            {
                galaxy.GetSolarSystems().LoadMapFromDB(conn);
            }
        }

        public void GetAllFromDB(SqlConnection conn)
        {
            //gets denormalised database to read
            //with all galaxies, solar systems, planets and regions
            using (SqlCommand cmd = new SqlCommand("LoadGalaxies", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //get feild
                            int id = SpaceAccess.GetGalaxyFeild_FromReader(
                                Galaxy.FeildType.ID, reader);

                            Add(new Galaxy(id)).GetFromDB(reader);
                        }
                    }
                    else
                    {
                        //no galaxies exist
                    }
                    reader.Close();
                }
                catch (SqlException se)
                {
                    throw new InvalidOperationException("something went wrong");
                }
            }
        }

        #endregion

        #region in memory methods

        public Galaxy GetById(int id)
        {
            foreach (Galaxy galaxy in galaxies) {
                if (galaxy.GetId() == id) {
                    return galaxy;
                }
            }
            throw new GalaxyNotFound(id);
        }

        private Galaxy Add(Galaxy galaxy)
        {
            int id = galaxy.GetId();
            try
            {
                GetById(id);
                throw new GalaxyAlreadyExists(id);
            }
            catch (GalaxyNotFound gnf) 
            {
                galaxies.Add(galaxy);
                return galaxy;
            }
            
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
