using StarPlan.Exceptions.PlanetExceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace StarPlan.Models.Space.Planets
{
    /// <todo>
    ///     add binary searching
    ///     for searching through records
    /// </todo>
    public class PlanetList
    {
        private List<Planet> planets;
        private int solarSystemId;

        public PlanetList(int solarSystemId) {
            Init(solarSystemId);
        }

        #region DB methods

        public void LoadMapFromDB(SqlConnection conn)
        {
            GetAllFromDB(conn);
            foreach (Planet planet in planets)
            {
                planet.GetRegions().LoadMapFromDB(conn);
            }
        }

        public void GetAllFromDB(SqlConnection conn)
        {

            using (SqlCommand cmd = new SqlCommand("LoadPlanets", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@systemId", SqlDbType.Int).Value = GetSolarSystemId();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            string size = reader.GetString("size");
                            
                            Add(PlanetFactory.GetPlanetFromSize(id, size)).GetFromDB(reader);
                        }
                    }
                    else
                    {
                        //no planets exist
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

        public Planet GetById(int id)
        {
            foreach (Planet planet in planets)
            {
                if (planet.GetId() == id)
                {
                    return planet;
                }
            }
            throw new PlanetNotFound(id);
        }

        private Planet Add(Planet planet)
        {
            int id = planet.GetId();
            try
            {
                GetById(id);
                throw new PlanetAlreadyExists(id);
            }
            catch (PlanetNotFound pnf)
            {
                planets.Add(planet);
                return planet;
            }

        }

        #endregion

        #region init
        private void Init(int solarSystemId)
        {
            planets = new List<Planet>();
            SetSolarSystemId(solarSystemId);
        }
        #endregion

        #region setters
        private void SetSolarSystemId(int solarSystemId)
        {
            this.solarSystemId = solarSystemId;
        }
        #endregion

        #region getters
        public int GetCount()
        {
            return planets.Count;
        }

        public int GetSolarSystemId()
        {
            return solarSystemId;
        }
        #endregion
    }
}
