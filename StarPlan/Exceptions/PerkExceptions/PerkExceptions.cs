using StarPlan.Models.Perks;
using StarPlan.Models.Space.Planets;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Exceptions.PerkExceptions
{
    [Serializable]
    public class PerkAlreadyExistsException : Exception
    {
        public PerkAlreadyExistsException(Perk perk)
            : base(String.Format(
                "perk 'id: {0}' already exists",
                perk.GetId()
                ))
        {

        }


    }
}

