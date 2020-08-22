using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlanDBAccess.Exceptions
{
    [Serializable]
    public class FeildNotFound : Exception
    {
        public FeildNotFound() : base
            (
                "feild not found"
            )
        {
            //do stuff
        }
    }
}
