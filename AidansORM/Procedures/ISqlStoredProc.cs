using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace StarPlanDBAccess.Procedures
{
    public interface ISqlStoredProc
    {
        SqlCommand GetCmd();
        IDataReader ExcecRdr();
        void ExcecSql();
        void SetProcName(string procName);
    }
}
