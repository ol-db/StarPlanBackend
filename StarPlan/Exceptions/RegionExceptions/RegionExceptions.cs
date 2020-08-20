using StarPlan.Models.Space.Planets;
using System;
using System.Collections.Generic;
using System.Drawing;
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

    [Serializable]
    public class RegionOutOfRange : Exception
    {
        public RegionOutOfRange(Point bounds)
            : base(String.Format(
                "region out of range, there must be {0}-{1} regions",
                bounds.X, bounds.Y))
        {

        }
    }
}
