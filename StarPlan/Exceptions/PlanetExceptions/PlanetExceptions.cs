using StarPlan.Models.Space.Planets;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Exceptions.PlanetExceptions
{
    [Serializable]
    class RegionOutOfRangeException : Exception
    {
        public RegionOutOfRangeException(int max, int min, Planet planet)
            : base(String.Format(
                "region out of range, there must be {0}-{1} regions in planet 'name: {2} id: {3}'",
                min, max, planet.GetName(), planet.GetId()))
        {

        }
    }

    [Serializable]
    class PerkCountOutOfRangeException : Exception
    {
        public PerkCountOutOfRangeException(int perkNum, Planet planet)
        : base(String.Format(
            "region out of range, there must be {0} perks in planet 'name: {2} id: {3}'",
            perkNum, planet.GetName(), planet.GetId()))
        {

        }


    }
}

