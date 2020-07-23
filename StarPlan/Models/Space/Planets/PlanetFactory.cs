using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Models.Space.Planets
{
    public class PlanetFactory
    {
        public static Planet GetPlanetFromSize(int id, string sizeStr) {
            Planet.PlanetSizeTypes size = Planet.PlanetStringToSizeType(sizeStr);
            switch (size)
            {
                case Planet.PlanetSizeTypes.DWARF:
                    return new DwarfPlanet(id);
                case Planet.PlanetSizeTypes.MEDIUM:
                    return new MediumPlanet(id);
                case Planet.PlanetSizeTypes.GIANT:
                    return new GiantPlanet(id);
                default:
                    throw new ArgumentException("invalid planet size");
            }
        }
    }
}
