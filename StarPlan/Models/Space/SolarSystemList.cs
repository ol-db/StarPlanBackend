﻿using StarPlan.Exceptions.SolarSystemExceptions;
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

        public void LoadMapFromDB(SqlConnection conn)
        {
            GetAllFromDB(conn);
            foreach (SolarSystem solarSystem in solarSystems)
            {
                solarSystem.GetPlanets().LoadMapFromDB(conn);
            }
        }

        /// <summary>
        ///     gets all the solar systems
        ///     belonging to the parent
        ///     galaxy
        /// </summary>
        /// <param name="conn"></param>
        public void GetAllFromDB(SqlConnection conn)
        {
            
            using (SqlCommand cmd = new SqlCommand("LoadSolarSystems", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@galaxyId", SqlDbType.Int).Value = GetGalaxyId();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            Add(new SolarSystem(id));
                        }
                    }
                    else
                    {
                        //no solar systems exist
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
