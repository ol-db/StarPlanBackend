using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StarPlan.Models.Perks
{
    public class Perk
    {
        private int id;

        public Perk(int id) {
            Edit(id);
        }

        private void Edit(int id) {
            this.id = id;
        }

        #region setters
        public void SetId(int id) {
            this.id = id;
        }
        #endregion
    }
}
