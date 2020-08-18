using StarPlan.Exceptions.PlanetExceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace StarPlan.Models.Space.Planets
{
    public class DwarfPlanet : Planet
    {
        public DwarfPlanet(int id) : base(id)
        {
            //inherits from base class constructor
        }

        public DwarfPlanet(int id, string name) : base(id,name)
        {
            //inherits from base class constructor
        }

    }
}
