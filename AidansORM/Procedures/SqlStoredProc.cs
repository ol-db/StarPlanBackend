﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace StarPlanDBAccess.Procedures
{
    public class SqlStoredProc : ISqlStoredProc
    {
        private SqlCommand cmd;

        public SqlStoredProc()
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
        }

        public SqlStoredProc(SqlConnection conn)
        {
            cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
        }

        public SqlParameterCollection GetParams()
        {
            return this.cmd.Parameters;
        }

        public IDataReader ExcecRdr()
        {
            return this.cmd.ExecuteReader();
        }

        public void ExcecSql()
        {
            this.cmd.ExecuteNonQuery();
        }

        public void SetProcName(string procName)
        {
            cmd.CommandText = procName;
            SqlCommandBuilder.DeriveParameters(cmd);
        }

    }
}
