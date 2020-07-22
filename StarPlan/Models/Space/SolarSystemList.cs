using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Models
{
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
        public void SetGalaxyId(int galaxyId) {
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
