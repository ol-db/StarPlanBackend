using System;
using System.Collections.Generic;
using System.Text;

namespace StarPlan.Models.Space.Planets
{
    public class Region
    {
        private int id;
        private string name;

        public Region(int id, string name) {
            Init(id, name);
        }

        #region init
        private void Init(int id,string name) {
            SetId(id);
            SetName(name);
        }
        #endregion

        #region setters
        private void SetId(int id) {
            this.id = id;
        }
        public void SetName(string name)
        {
            this.name = name;
        }
        #endregion

        #region getters
        public int GetId() {
            return this.id;
        }
        public string GetName()
        {
            return this.name;
        }
        #endregion
    }
}
