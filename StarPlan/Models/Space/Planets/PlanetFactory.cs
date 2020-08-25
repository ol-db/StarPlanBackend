using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Models.Space.Planets
{
    public class PlanetFactory
    {
        public static Planet GetPlanetFromSize(int id, string sizeStr) {
            return GetPlanetFromSize(id, "", sizeStr);
        }

        public static Planet GetPlanetFromSize(int id,string name, string sizeStr)
        {
            Planet.SizeTypes size = Planet.PlanetStringToSizeType(sizeStr);
            switch (size)
            {
                case Planet.SizeTypes.DWARF:
                    return new DwarfPlanet(id,name);
                case Planet.SizeTypes.MEDIUM:
                    return new MediumPlanet(id,name);
                case Planet.SizeTypes.GIANT:
                    return new GiantPlanet(id,name);
                default:
                    throw new ArgumentException("invalid planet size");
            }
        }
    }
}
