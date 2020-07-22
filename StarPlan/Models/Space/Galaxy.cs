﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text;

namespace StarPlan.Models
{
    public class Galaxy
    {
        private int id;
        private string name;
        private string desc;
        private SolarSystemList SolarSystems;

        public Galaxy(int id,string name,string desc)
        {
            Init(id, name, desc);
        }

        public Galaxy(int id)
        {
            
            Init(id);
        }

        #region edit methods

        public void Edit(string name, string desc) {
            
            SetName(name);
            SetDesc(desc);
        }

        #endregion

        #region init methods

        private void Init(int id, string name, string desc)
        {
            this.SolarSystems = new SolarSystemList(id);
            SetId(id);
            Edit(name, desc);
        }

        private void Init(int id)
        {
            Init(id, "", "");
        }

        #endregion

        #region representation

        override
        public string ToString()
        {
            return string.Format(
                "START_OF_GALAXY\n" +
                "id:{0}\n" +
                "name:{1}\n" +
                "desc:{2}\n" +
                SolarSystems.ToString() +
                "END_OF_GALAXY\n",
                id, name, desc
                );
        }

        #endregion

        #region setters

        public void SetId(int id) {
            this.id = id;
        }

        public void SetDesc(string desc)
        {
            if (desc.Length > 200) {
                throw new ArgumentException("desc is too long");
            }
            else { this.desc = desc; }
            
        }

        public void SetName(string name)
        {
            if (name.Length > 20)
            {
                throw new ArgumentException("name is too long");
            }
            else { this.name = name; }

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

        public string GetDesc()
        {
            return this.desc;
        }

        #endregion
    }
}
