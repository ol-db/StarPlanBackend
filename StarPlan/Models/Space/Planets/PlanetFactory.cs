using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Models.Space.Planets
{
    public class PlanetFactory
    {
        public static Planet GetPlanetFromSize(int id, string sizeStr) {
            Planet.SizeTypes size = Planet.PlanetStringToSizeType(sizeStr);
            switch (size)
            {
                case Planet.SizeTypes.DWARF:
                    return new DwarfPlanet(id);
                case Planet.SizeTypes.MEDIUM:
                    return new MediumPlanet(id);
                case Planet.SizeTypes.GIANT:
                    return new GiantPlanet(id);
                default:
                    throw new ArgumentException("invalid planet size");
            }
        }
    }
}
