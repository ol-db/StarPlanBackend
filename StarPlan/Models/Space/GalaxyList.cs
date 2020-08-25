using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using StarPlan.Exceptions.GalaxyExceptions;
using StarPlanDBAccess.ORM;
using StarPlan.DataAccess;
using StarPlanDBAccess.Procedures;
using Newtonsoft.Json;

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

        public void LoadMapFromDB(ISqlStoredProc proc)
        {
            GetAllFromDB(proc);
            foreach (Galaxy galaxy in galaxies)
            {
                galaxy.GetSolarSystems().LoadMapFromDB(proc);
            }
        }

        public void GetAllFromDB(ISqlStoredProc proc)
        {
            proc.SetProcName("LoadGalaxies");

            try
            {
                IDataReader reader = proc.ExcecRdr();

                while (reader.Read())
                {
                    //get galaxy id
                    int id = SpaceAccess.GetGalaxyFeild_FromReader(
                        Galaxy.FeildType.ID, reader);

                    //add new galaxy
                    //then populate galaxy from DB
                    Add(new Galaxy(id)).GetFromDB(reader);
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

        public List<object> ToObj()
        {
            List<object> galaxies = new List<object>();
            foreach (Galaxy galaxy in this.galaxies)
            {
                galaxies.Add(galaxy.ToObj());
            }
            return galaxies;
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
            List<object> galaxies = new List<object>();
            foreach (Galaxy galaxy in this.galaxies)
            {
                galaxies.Add(galaxy.ToObjSingle());
            }
            return galaxies;
        }

        public string ToJsonSingle()
        {
            return JsonConvert.SerializeObject
            (
                ToObjSingle()
            );
        }

        #endregion
    }
}
