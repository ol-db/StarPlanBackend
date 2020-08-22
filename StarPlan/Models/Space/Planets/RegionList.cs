using StarPlan.Exceptions.PlanetExceptions;
using StarPlan.Exceptions.RegionExceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using StarPlanDBAccess.ORM;
using StarPlan.DataAccess;

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

        public void LoadMapFromDB(SqlConnection conn)
        {
            GetAllFromDB(conn);
        }

        public void GetAllFromDB(SqlConnection conn)
        {

            using (SqlCommand cmd = new SqlCommand("LoadRegions", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                //get param info
                SqlCommandBuilder.DeriveParameters(cmd);

                //set params
                SpaceAccess.SetPlanetParam(
                    new Tuple<object, Planet.FeildType>(GetPlanetId(), Planet.FeildType.ID),
                    cmd.Parameters
                    );

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = SpaceAccess.GetRegionFeild_FromReader(
                                Region.FeildType.ID, reader);

                            Add(new Region(id)).GetFromDB(reader);
                        }

                        //checks bounds
                        VerifyBounds();
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
