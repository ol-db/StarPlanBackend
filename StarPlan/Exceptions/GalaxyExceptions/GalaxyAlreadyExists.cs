using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Exceptions.GalaxyExceptions
{
    [Serializable]
    public class GalaxyAlreadyExists : Exception
    {
        public GalaxyAlreadyExists(int id)
            : base(String.Format("Galaxy Already Exists With The Id: {0}", id))
        {

        }
    }
}
