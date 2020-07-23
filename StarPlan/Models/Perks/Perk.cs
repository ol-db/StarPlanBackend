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
            Init(id);
        }

        #region init
        private void Init(int id) {
            SetId(id);
        }
        #endregion

        #region setters
        private void SetId(int id) {
            this.id = id;
        }
        #endregion
    }
}
