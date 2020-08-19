using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
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

        #region DB methods

        #region edit

        public void EditInDB(string name, SqlConnection conn)
        {
            ///get name in case changes need to
            ///be reverted because of
            ///an SQL error
            ///
            ///set name in class
            string lastName = GetName();
            SetName(name);

            try
            {
                using (SqlCommand cmd = new SqlCommand("EditRegion", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;



                    ///catch any SQL error
                    ///so changes to class
                    ///can be reverted
                    ///to keep data consistent
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = GetId();
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = GetName();

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
            catch (Exception se)
            {
                SetName(lastName);

                ///<todo>
                ///add custom exception
                ///</todo>
                throw new InvalidOperationException("region was not altered");
            }
        }

        #endregion

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
