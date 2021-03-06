﻿using StarPlan.Exceptions.PlanetExceptions;
using StarPlan.Exceptions.RegionExceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
    public class RegionList
    {
        private int planetId;
        private List<Region> regions;
        private Point bounds;
        private void VerifyBounds()
        {
            int count = GetCount();
            if ((count >= bounds.X) && (count <= bounds.Y))
            {
                //passed
            }
            else
            {
                throw new RegionOutOfRange(bounds);
            }
        }

        public RegionList(int planetId,Point bounds) {
            Init(planetId,bounds);
        }

        #region init
        private void Init(int planetId,Point bounds) {
            SetBounds(bounds);
            regions = new List<Region>();
            SetPlanetId(planetId);
        }
        #endregion

        #region DB methods

        #region get

        public void LoadMapFromDB(ISqlStoredProc proc)
        {
            GetAllFromDB(proc);
        }

        public void GetAllFromDB(ISqlStoredProc proc)
        {
            proc.SetProcName("LoadRegions");

            //set params
            SpaceAccess.SetPlanetParam
            (
                new Tuple<object, Planet.FeildType>(GetPlanetId(), Planet.FeildType.ID),
                proc.GetParams()
            );

            try
            {
                IDataReader reader = proc.ExcecRdr();
                while (reader.Read())
                {
                    int id = SpaceAccess.GetRegionFeild_FromReader(
                        Region.FeildType.ID, reader);

                    Add(new Region(id)).GetFromDB(reader);
                }

                //checks bounds
                VerifyBounds();

                reader.Close();
            }
            catch (SqlException se)
            {
                throw new InvalidOperationException("something went wrong");
            }
        }

        #endregion

        #endregion

        #region in memory methods
        /// <todo>
        /// add binary searching
        /// </todo>
        public Region Add(Region regionToAdd) {


            int count = GetCount();

            //verify the region exists
            foreach (Region region in regions)
            {
                if (region.GetId().Equals(regionToAdd.GetId()))
                {
                    throw new ArgumentException("region already exists");
                }
            }

            
            //add the region
            regions.Add(regionToAdd);
            return regionToAdd;
        }
        #endregion

        #region representation

        public List<object> ToObj()
        {
            List<object> regions = new List<object>();
            foreach (Region region in this.regions)
            {
                regions.Add(region.ToObj());
            }
            return regions;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject
            (
                ToObj()
            );
        }

        #endregion

        #region setters

        private void SetPlanetId(int planetId) {
            this.planetId = planetId;
        }

        private void SetBounds(Point bounds)
        {
            this.bounds = bounds;
        }

        #endregion

        #region getters
        public int GetCount()
        {
            return regions.Count;
        }

        public int GetPlanetId()
        {
            return planetId;
        }
        #endregion
    }
}
