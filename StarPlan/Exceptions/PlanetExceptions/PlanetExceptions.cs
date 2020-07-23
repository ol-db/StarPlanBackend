using StarPlan.Models.Perks;
using StarPlan.Models.Space.Planets;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Exceptions.PlanetExceptions
{
    [Serializable]
    public class RegionOutOfRangeException : Exception
    {
        public RegionOutOfRangeException(int max, int min, Planet planet)
            : base(String.Format(
                "region out of range, there must be {0}-{1} regions in planet 'name: {2} id: {3}'",
                min, max, planet.GetName(), planet.GetId()))
        {

        }
    }

    [Serializable]
    public class PerkCountOutOfRangeException : Exception
    {
        public PerkCountOutOfRangeException(int perkNum, Planet planet)
        : base(String.Format(
            "region out of range, there must be {0} perks in planet 'name: {1} id: {2}'",
            perkNum, planet.GetName(), planet.GetId()))
        {

        }


    }

    [Serializable]
    public class RegionsPlanetDoesntMatchException : Exception
    {
        public RegionsPlanetDoesntMatchException(RegionList regions, Planet planet)
            : base(String.Format(
                "the regions belong to planet 'id: {0}' not planet 'id: {1}'",
                regions.GetPlanetId(), planet.GetId()))
        {

        }
    }

    [Serializable]
    public class PerksPlanetDoesntMatchException : Exception
    {
        public PerksPlanetDoesntMatchException(PerkList perks, Planet planet)
            : base(String.Format(
                "the perks belong to planet 'id: {0}' not planet 'id: {1}'",
                perks.GetPlanetId(), planet.GetId()))
        {

        }


    }
}

