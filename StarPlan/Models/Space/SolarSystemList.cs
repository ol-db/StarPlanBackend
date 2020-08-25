using Newtonsoft.Json;
using StarPlan.DataAccess;
using StarPlan.Exceptions.SolarSystemExceptions;
using StarPlanDBAccess.Procedures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace StarPlan.Models
{
    /// <todo>
    ///     add binary searching
    ///     for searching through records
    /// </todo>

    /// <todo>
    ///     fix single responsibility pattern
    ///     find way around having galaxyId for
    ///     SolarSystemList class
    ///     
    ///     because the galaxyId may not be the only
    ///     foreign key in the class in the future...
    /// </todo>
    public class SolarSystemList
    {
        private List<SolarSystem> solarSystems;
        private int galaxyId; //reference to the galaxy for DB access
        public SolarSystemList(int galaxyId)
        {
            Init(galaxyId);
        }

        #region DB methods

        public void LoadMapFromDB(ISqlStoredProc proc)
        {
            GetAllFromDB(proc);
            foreach (SolarSystem solarSystem in solarSystems)
            {
                solarSystem.GetPlanets().LoadMapFromDB(proc);
            }
        }

        /// <summary>
        ///     gets all the solar systems
        ///     belonging to the parent
        ///     galaxy
        /// </summary>
        /// <param name="conn"></param>
        public void GetAllFromDB(ISqlStoredProc proc)
        {
            proc.SetProcName("LoadSolarSystems");

            SpaceAccess.SetGalaxyParam
            (
                new Tuple<object, Galaxy.FeildType>(GetGalaxyId(),Galaxy.FeildType.ID),
                proc.GetParams()
            );

            try
            {
                IDataReader reader = proc.ExcecRdr();
                while (reader.Read())
                {

                    int id = SpaceAccess.GetSolarSystemFeild_FromReader
                    (
                        SolarSystem.FeildType.ID,
                        reader
                    );

                    Add(new SolarSystem(id));
                }
                reader.Close();
            }
            catch (SqlException se)
            {
                throw new InvalidOperationException("something went wrong");
            }
        }

        #endregion

        #region in memory methods

        public SolarSystem GetById(int id)
        {
            foreach (SolarSystem solarSystem in solarSystems)
            {
                if (solarSystem.GetId() == id)
                {
                    return solarSystem;
                }
            }
            throw new SolarSystemNotFound(id);
        }

        private SolarSystem Add(SolarSystem solarSystem)
        {
            int id = solarSystem.GetId();
            try
            {
                GetById(id);
                throw new SolarSystemAlreadyExists(id);
            }
            catch (SolarSystemNotFound ssnf)
            {
                //solar system doesn't already exist
                solarSystems.Add(solarSystem);
                return solarSystem;
            }

        }

        #endregion

        #region init
        private void Init(int galaxyId) {
            this.solarSystems = new List<SolarSystem>();
            SetGalaxyId(galaxyId);
        }
        #endregion

        #region representation

        public List<object> ToObj()
        {
            List<object> systems = new List<object>();
            foreach (SolarSystem system in this.solarSystems)
            {
                systems.Add(system.ToObj());
            }
            return systems;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject
            (
                ToObj()
            );
        }

        public List<object> ToObjSingle()
        {
            List<object> systems = new List<object>();
            foreach (SolarSystem system in this.solarSystems)
            {
                systems.Add(system.ToObjSingle());
            }
            return systems;
        }

        public string ToJsonSingle()
        {
            return JsonConvert.SerializeObject
            (
                ToObjSingle()
            );
        }

        #endregion

        #region setters
        private void SetGalaxyId(int galaxyId) {
            this.galaxyId = galaxyId;
        }
        #endregion

        #region getters
        public int GetGalaxyId() {
            return this.galaxyId;
        }
        #endregion
    }
}
