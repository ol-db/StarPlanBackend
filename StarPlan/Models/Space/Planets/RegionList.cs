using System;
using System.Collections.Generic;
using System.Text;

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

        public RegionList(int planetId) {
            Init(planetId);
        }

        #region init
        private void Init(int planetId) {
            regions = new List<Region>();
            SetPlanetId(planetId);
        }
        #endregion

        #region DB store
        #endregion

        #region object store
        /// <todo>
        /// add binary searching
        /// </todo>
        public void AddRegion(Region regionToAdd) {

            //verify the region exists
            foreach (Region region in regions)
            {
                if (region.GetId().Equals(region.GetId()))
                {
                    throw new ArgumentException("region already exists");
                }
            }
            
            //add the region
            regions.Add(regionToAdd);
        }
        #endregion

        #region setters
        private void SetPlanetId(int planetId) {
            this.planetId = planetId;
        }
        #endregion

        #region getters
        public int GetNumberOfRegions()
        {
            return regions.Count;
        }
        #endregion
    }
}
