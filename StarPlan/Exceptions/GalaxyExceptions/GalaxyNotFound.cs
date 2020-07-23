using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Exceptions.GalaxyExceptions
{
    [Serializable]
    public class GalaxyNotFound : Exception
    {
        public GalaxyNotFound(int id)
            : base(String.Format("No Galaxy Found With The Id: {0}", id))
        {

        }

    }
}
