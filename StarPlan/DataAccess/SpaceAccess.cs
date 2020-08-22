using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using StarPlan.Models;
using StarPlan.Models.Space.Planets;
using StarPlanDBAccess.ORM;

namespace StarPlan.DataAccess
{
    public class SpaceAccess
    {
        #region get feild from data reader
        public static dynamic GetRegionFeild_FromReader(Region.FeildType feild, SqlDataReader rdr)
        {
            return RecordAccess.GetFeildFromReader(Region.GetFeildNames(feild), rdr);
        }

        public static dynamic GetPlanetFeild_FromReader(Planet.FeildType feild,SqlDataReader rdr)
        {
            return RecordAccess.GetFeildFromReader(Planet.GetFeildNames(feild), rdr);
        }

        public static dynamic GetSolarSystemFeild_FromReader(SolarSystem.FeildType feild, SqlDataReader rdr)
        {
            return RecordAccess.GetFeildFromReader(SolarSystem.GetFeildNames(feild), rdr);
        }

        public static dynamic GetGalaxyFeild_FromReader(Galaxy.FeildType feild, SqlDataReader rdr)
        {
            return RecordAccess.GetFeildFromReader(Galaxy.GetFeildNames(feild), rdr);
        }

        #endregion

        #region set params

        public static SqlParameterCollection SetRegionParam(Tuple<object, Region.FeildType> valFeild, SqlParameterCollection paramList)
        {
            ProcAccess.SetParam(Region.GetFeildNames(valFeild.Item2), paramList, valFeild.Item1);
            return paramList;
        }

        public static SqlParameterCollection SetRegionParams(IEnumerable<Tuple<object, Region.FeildType>> valFeilds, SqlParameterCollection paramList)
        {
            foreach (Tuple<object, Region.FeildType> valFeild in valFeilds)
            {
                SetRegionParam(valFeild, paramList);
            }
            return paramList;
        }

        public static SqlParameterCollection SetPlanetParam(Tuple<object, Planet.FeildType> valFeild, SqlParameterCollection paramList)
        {
            ProcAccess.SetParam(Planet.GetFeildNames(valFeild.Item2), paramList, valFeild.Item1);
            return paramList;
        }
        
        public static SqlParameterCollection SetPlanetParams(IEnumerable<Tuple<object,Planet.FeildType>> valFeilds, SqlParameterCollection paramList)
        {
            foreach (Tuple<object, Planet.FeildType> valFeild in valFeilds)
            {
                SetPlanetParam(valFeild, paramList);
            }
            return paramList;
        }

        public static SqlParameterCollection SetSolarSystemParam(Tuple<object, SolarSystem.FeildType> valFeild, SqlParameterCollection paramList)
        {
            ProcAccess.SetParam(SolarSystem.GetFeildNames(valFeild.Item2), paramList, valFeild.Item1);
            return paramList;
        }

        public static SqlParameterCollection SetSolarSystemParams(IEnumerable<Tuple<object, SolarSystem.FeildType>> valFeilds, SqlParameterCollection paramList)
        {
            foreach (Tuple<object, SolarSystem.FeildType> valFeild in valFeilds)
            {
                SetSolarSystemParam(valFeild, paramList);
            }
            return paramList;
        }

        public static SqlParameterCollection SetGalaxyParam(Tuple<object, Galaxy.FeildType> valFeild, SqlParameterCollection paramList)
        {
            ProcAccess.SetParam(Galaxy.GetFeildNames(valFeild.Item2), paramList, valFeild.Item1);
            return paramList;
        }

        public static SqlParameterCollection SetGalaxyParams(IEnumerable<Tuple<object, Galaxy.FeildType>> valFeilds, SqlParameterCollection paramList)
        {
            foreach (Tuple<object, Galaxy.FeildType> valFeild in valFeilds)
            {
                SetGalaxyParam(valFeild, paramList);
            }
            return paramList;
        }


        #endregion
    }
}
