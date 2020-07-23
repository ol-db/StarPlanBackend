using StarPlan.Models.Space.Planets;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Exceptions.RegionExceptions
{
    [Serializable]
    public class RegionAlreadyExistsException : Exception
    {
        public RegionAlreadyExistsException(Region region)
            : base(String.Format(
                "region 'name: {0} id: {1}' already exists",
                region.GetName(), region.GetId()
                ))
        {

        }

    }
}
