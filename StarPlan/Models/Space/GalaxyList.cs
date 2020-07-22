using System;
using System.Collections.Generic;
using System.Text;
using StarPlan.Exceptions.GalaxyExceptions;

namespace StarPlan.Models
{
    public class GalaxyList
    {
        private List<Galaxy> galaxies;

        public GalaxyList() {
            this.galaxies = new List<Galaxy>();
        }

        public void AddGalaxy(Galaxy galaxy) {
            galaxies.Add(galaxy);
        }

        public void RemoveGalaxy(int id) {
            galaxies.Remove(GetGalaxyById(id));
        }

        public Galaxy GetGalaxyById(int id)
        {
            foreach (Galaxy galaxy in galaxies) {
                if (galaxy.GetId() == id) {
                    return galaxy;
                }
            }
            throw new GalaxyNotFound(id);
        }

        override
        public string ToString()
        {
            string ToString = "";
            foreach (Galaxy galaxy in galaxies) {
                ToString += galaxy.ToString();
            }
            return ToString;
        }
    }
}
