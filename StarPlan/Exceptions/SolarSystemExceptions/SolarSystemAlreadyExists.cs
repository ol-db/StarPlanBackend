using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Exceptions.SolarSystemExceptions
{
    [Serializable]
    class SolarSystemAlreadyExists : Exception
    {
        
            public SolarSystemAlreadyExists(int id)
                : base(String.Format("Solar System Already Exists With The Id: {0}", id))
            {

            }
    }
}
