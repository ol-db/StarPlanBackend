using StarPlan.Models.Space.Planets;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Models
{
    public class SolarSystem
    {
        private int id;
        private PlanetList planets;

        public SolarSystem(int id) {
            init(id);
        }

        #region init
        private void init(int id) {
            SetId(id);
        }
        #endregion

        #region representation
        override
        public string ToString()
        {
            return string.Format(
                "START_OF_SOLAR_SYSTEM\n"+
                "id:{0}\n"+
                "END_OF_SOLAR_SYSTEM\n"
                ,id);
        }
        #endregion

        #region setters
        private void SetId(int id) {
            this.id = id;
        }
        #endregion

        #region getters
        public int GetId() {
            return id;
        }
        #endregion
    }
}
