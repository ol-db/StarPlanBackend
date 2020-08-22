using System;
using System.Data;
using System.Collections.Generic;
using StarPlanDBAccess.Exceptions;

namespace StarPlanDBAccess.ORM
{
    public class RecordAccess
    {
        public static object GetFeildFromReader(string[] names,IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                foreach(string name in names)
                {
                    //not case sensitive
                    if ((reader.GetName(i).ToLower()).Equals(name.ToLower()))
                    {
                        return reader[reader.GetName(i)];
                    }
                }
            }
            throw new FeildNotFound();
        }

    }
}
