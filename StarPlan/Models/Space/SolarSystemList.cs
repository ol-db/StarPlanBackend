using StarPlan.Exceptions.SolarSystemExceptions;
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

        public void LoadMapFromDB(SqlDataReader reader)
        {
            int id = reader.GetInt32("solarSystemId");
            try
            {
                //AddSolarSystem(new SolarSystem(id)).LoadMapFromDB(reader);
            }
            catch (SolarSystemAlreadyExists ssae)
            {
                //GetSolarSystemById(id).LoadMapFromDB(reader);
            }

        }

        #endregion

        #region in memory methods

        public SolarSystem GetSolarSystemById(int id)
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

        private SolarSystem AddSolarSystem(SolarSystem solarSystem)
        {
            int id = solarSystem.GetId();
            try
            {
                GetSolarSystemById(id);
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

        #region edit
        private void Edit(int galaxyId)
        {
            SetGalaxyId(galaxyId);
        }
        #endregion

        #region representation
        override
        public string ToString()
        {
            string value = "";
            foreach (SolarSystem solarSystem in solarSystems) {
                value += solarSystem.ToString();
            }
            return value;
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
