using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using StarPlanDBAccess.Exceptions;

namespace StarPlanDBAccess.ORM
{
    public class ProcAccess
    {
        public static string GetParam(string[] paramNames, SqlParameterCollection paramList)
        {

            for (int i = 0; i < paramList.Count; i++)
            {
                foreach (string name in paramNames)
                {
                    //not case sensitive
                    if ((paramList[i].ParameterName.ToLower()).Equals("@"+name.ToLower()))
                    {
                        return paramList[i].ParameterName;
                    }
                }
            }
            throw new ParamNotFound();
        }

        public static SqlParameterCollection SetParam(
            string[] paramNames,
            SqlParameterCollection paramList,
            object paramValue)
        {
            paramList[GetParam(paramNames, paramList)].Value = paramValue;
            return paramList;
        }
    }
}
