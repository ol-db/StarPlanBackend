using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlanDBAccess.Exceptions
{
    [Serializable]
    public class ParamNotFound : Exception
    {
        public ParamNotFound() : base
            (
                "param not found"
            )
        {
            //do stuff
        }
    }
}
