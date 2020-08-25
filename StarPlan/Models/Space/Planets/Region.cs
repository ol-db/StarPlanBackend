using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using Newtonsoft.Json;
using StarPlan.DataAccess;
using StarPlanDBAccess.Exceptions;
using StarPlanDBAccess.ORM;
using StarPlanDBAccess.Procedures;

namespace StarPlan.Models.Space.Planets
{
    public class Region
    {
        #region DB Feilds
        public enum FeildType
        {
            ID,
            NAME
        }

        public static string[] GetFeildNames(FeildType feild)
        {
            if (feild == FeildType.ID)
            {
                return new string[] { "id", "regionId" };
            }
            else if (feild == FeildType.NAME)
            {
                return new string[] { "name", "regionName" };
            }
            else
            {
                throw new FeildNotFound();
            }
        }

        #endregion

        private int id;
        private string name;

        public Region(int id, string name) {
            Init(id, name);
        }

        public Region(int id)
        {
            Init(id);
        }

        #region init

        private void Init(int id,string name) {
            SetId(id);
            SetName(name);
        }

        /// <todo>
        ///     init method for
        ///     get from DB
        ///     
        ///     error check Id
        ///     being equal to null
        /// </todo>
        private void Init(string name)
        {
            Init(GetId(), name);
        }

        private void Init(int id)
        {
            Init(id, "");
        }

        #endregion

        #region DB methods

        #region get

        public void GetFromDB(IDataReader reader)
        {
            string name = SpaceAccess.GetRegionFeild_FromReader(
                Region.FeildType.NAME, reader);

            Init(name);
        }

        #endregion

        #region edit

        public void EditInDB(string name, ISqlStoredProc proc)
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

                proc.SetProcName("EditRegion");

                SpaceAccess.SetRegionParams(
                    new List<Tuple<object, FeildType>>
                    {
                        new Tuple<object, FeildType>(GetId(),FeildType.ID),
                        new Tuple<object, FeildType>(GetName(),FeildType.NAME)
                    },
                    proc.GetParams()
                    );
                proc.ExcecSql();
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

        #region representation

        public string ToJson()
        {
            return JsonConvert.SerializeObject
            (
                ToObj()
            );
        }

        public object ToObj()
        {
            return
                new
                {
                    id = GetId(),
                    name = GetName()
                };
        }

        #endregion

        #region setters
        private void SetId(int id) {
            this.id = id;
        }
        private void SetName(string name)
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
