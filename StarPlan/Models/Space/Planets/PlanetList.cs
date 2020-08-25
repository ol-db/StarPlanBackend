using StarPlan.Exceptions.PlanetExceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using StarPlanDBAccess.ORM;
using StarPlan.DataAccess;
using StarPlanDBAccess.Procedures;
using Newtonsoft.Json;

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

        #region get

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

            //set param
            SpaceAccess.SetSolarSystemParam(
                new Tuple<object, SolarSystem.FeildType>(
                    GetSolarSystemId(), SolarSystem.FeildType.ID),
                proc.GetParams());

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

        #region representation

        #region nested Objs

        public List<object> ToObj()
        {
            List<object> planets = new List<object>();
            foreach (Planet planet in this.planets)
            {
                planets.Add(planet.ToObj());
            }
            return planets;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject
            (
                ToObj()
            );
        }

        #endregion

        #region single Obj

        public List<object> ToObjSingle()
        {
            List<object> planets = new List<object>();
            foreach (Planet planet in this.planets)
            {
                planets.Add(planet.ToObjSingle());
            }
            return planets;
        }

        public string ToJsonSingle()
        {
            return JsonConvert.SerializeObject
            (
                ToObjSingle()
            );
        }

        #endregion

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
