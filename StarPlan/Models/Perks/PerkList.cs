using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Models.Perks
{
    public class PerkList
    {
        /// <todo>
        /// remove planetId
        /// for when normal/planet perks are available
        /// </tod>
        private List<Perk> perks;
        private int planetId;

        public PerkList(int planetId) {
            Init(planetId);
        }



        #region init
        private void Init(int planetId) {
            perks = new List<Perk>();
            SetPlanetId(planetId);
        }
        #endregion

        #region setters
        private void SetPlanetId(int planetId) {
            this.planetId = planetId;
        }
        #endregion

        #region getters
        public int GetPerkCount()
        {
            return perks.Count;
        }
        #endregion
    }
}
