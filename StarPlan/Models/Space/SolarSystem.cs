using StarPlan.Models.Space.Planets;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using StarPlanDBAccess.Exceptions;
using StarPlanDBAccess.ORM;

namespace StarPlan.Models
{
    public class SolarSystem
    {
        #region DB Feilds
        public enum FeildType
        {
            ID
        }

        public static string[] GetFeildNames(FeildType feild)
        {
            if (feild == FeildType.ID)
            {
                return new string[] { "id", "solarSystemId", "systemId" };
            }
            else
            {
                throw new FeildNotFound();
            }
        }

        #endregion

        private int id;
        private PlanetList planets;

        public SolarSystem(int id) {
            init(id);
        }

        #region init
        private void init(int id) {
            planets = new PlanetList(id);
            SetId(id);
        }
        #endregion

        #region representation
        override
        public string ToString()
        {
            return string.Format(
                "START_OF_SOLAR_SYSTEM\n"+
                "id:{0}\n"+
                "END_OF_SOLAR_SYSTEM\n"
                ,id);
        }
        #endregion

        #region DB methods

        #endregion

        #region setters
        private void SetId(int id) {
            this.id = id;
        }
        #endregion

        #region getters

        public int GetId() {
            return id;
        }

        public PlanetList GetPlanets()
        {
            return this.planets;
        }

        #endregion
    }
}
