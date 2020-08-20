using StarPlan.Models.Perks;
using StarPlan.Models.Space.Planets;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Exceptions.PlanetExceptions
{

    [Serializable]
    public class PerkCountOutOfRange : Exception
    {
        public PerkCountOutOfRange(int perkNum, Planet planet)
        : base(String.Format(
            "region out of range, there must be {0} perks in planet 'name: {1} id: {2}'",
            perkNum, planet.GetName(), planet.GetId()))
        {

        }


    }

    [Serializable]
    public class RegionsPlanetDoesntMatch : Exception
    {
        public RegionsPlanetDoesntMatch(RegionList regions, Planet planet)
            : base(String.Format(
                "the regions belong to planet 'id: {0}' not planet 'id: {1}'",
                regions.GetPlanetId(), planet.GetId()))
        {

        }
    }

    [Serializable]
    public class PerksPlanetDoesntMatch : Exception
    {
        public PerksPlanetDoesntMatch(PerkList perks, Planet planet)
            : base(String.Format(
                "the perks belong to planet 'id: {0}' not planet 'id: {1}'",
                perks.GetPlanetId(), planet.GetId()))
        {

        }

    }

    [Serializable]
    public class PlanetNotFound : Exception
    {
        public PlanetNotFound(int id)
            : base(String.Format("Planet Not Found With The Id: {0}", id))
        {

        }
    }

    [Serializable]
    public class PlanetAlreadyExists : Exception
    {
        public PlanetAlreadyExists(int id)
            : base(String.Format("Planet Already Exists With The Id: {0}", id))
        {

        }
    }
}

