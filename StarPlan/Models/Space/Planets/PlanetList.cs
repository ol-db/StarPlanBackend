using StarPlan.Exceptions.PlanetExceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using StarPlanDBAccess.ORM;
using StarPlan.DataAccess;
using StarPlanDBAccess.Procedures;

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

        public void LoadMapFromDB(ISqlStoredProc proc)
        {
            GetAllFromDB(proc);
            foreach (Planet planet in planets)
            {
                planet.GetRegions().LoadMapFromDB(proc);
            }
        }

        public void GetAllFromDB(ISqlStoredProc proc)
        {
            proc.SetProcName("LoadPlanets");
            SqlCommand cmd = proc.GetCmd();

            //set param
            SpaceAccess.SetSolarSystemParam(
                new Tuple<object, SolarSystem.FeildType>(
                    GetSolarSystemId(), SolarSystem.FeildType.ID),
                cmd.Parameters);

            try
            {
                IDataReader reader = proc.ExcecRdr();

                while (reader.Read())
                {
                    int id = SpaceAccess.GetPlanetFeild_FromReader(
                        Planet.FeildType.ID, reader);
                    string size = SpaceAccess.GetPlanetFeild_FromReader(
                        Planet.FeildType.SIZE, reader);

                    Add(PlanetFactory.GetPlanetFromSize(id, size)).GetFromDB(reader);
                }
                reader.Close();
            }
            catch (Exception se)
            {
                throw new InvalidOperationException("something went wrong");
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
