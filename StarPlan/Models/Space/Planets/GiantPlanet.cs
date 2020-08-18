using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Models.Space.Planets
{
    public class GiantPlanet : Planet
    {
        public GiantPlanet(int id) : base(id)
        {
            //inherits from base class constructor
        }

        public GiantPlanet(int id,string name) : base(id,name)
        {
            //inherits from base class constructor
        }
    }
}
