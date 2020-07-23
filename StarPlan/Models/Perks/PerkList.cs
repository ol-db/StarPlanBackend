using StarPlan.Exceptions.PerkExceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Models.Perks
{
    /// <todo>
    /// add binary searching to speed things up
    /// </todo>
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

        #region object data methods
        public void AddPerk(Perk perkToAdd)
        {
            foreach (Perk perk in perks) {
                if (perk.GetId().Equals(perkToAdd.GetId()))
                {
                    throw new PerkAlreadyExistsException(perk);
                }
            }

            perks.Add(perkToAdd);
        }
        #endregion

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

        public int GetPlanetId() {
            return planetId;
        }
        #endregion
    }
}
