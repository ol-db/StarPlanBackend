using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Exceptions.SolarSystemExceptions
{
    [Serializable]
    public class SolarSystemNotFound : Exception
    {
        public SolarSystemNotFound(int id)
            : base(String.Format("No Solar System Found With The Id: {0}", id))
        {

        }

    }
}
