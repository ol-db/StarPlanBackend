using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using StarPlan.DataAccess;
using StarPlanDBAccess.Exceptions;
using StarPlanDBAccess.ORM;
using StarPlanDBAccess.Procedures;

namespace StarPlan.Models
{
    public class Galaxy
    {
        #region DB Feilds
        public enum FeildType
        {
            ID,
            NAME,
            DESC
        }

        public static string[] GetFeildNames(FeildType feild)
        {
            if (feild == FeildType.ID)
            {
                return new string[] { "id", "galaxyId" };
            }
            else if (feild == FeildType.NAME)
            {
                return new string[] { "name", "galaxyName" };
            }
            else if (feild == FeildType.DESC)
            {
                return new string[] { "desc", "galaxyDesc", "description", "galaxyDescription" };
            }
            else
            {
                throw new FeildNotFound();
            }
        }

        #endregion

        private int id;
        private string name;
        private string desc;
        private SolarSystemList solarSystems;

        public Galaxy(int id,string name,string desc)
        {
            Init(id, name, desc);
        }

        public Galaxy(int id)
        {
            
            Init(id);
        }

        #region DB methods

        #region edit methods

        public void EditInDB(string name, string desc,ISqlStoredProc proc) {

            string lastDesc = GetDesc();
            string lastName = GetName();

            SetName(name);
            SetDesc(desc);

            try
            {

                proc.SetProcName("EditGalaxy");

                SqlCommand cmd = proc.GetCmd();

                ///catch any SQL error
                ///so changes can be undone

                SpaceAccess.SetGalaxyParams
                (
                    new List<Tuple<object, FeildType>>
                    {
                        new Tuple<object, FeildType>(GetId(),FeildType.ID),
                        new Tuple<object, FeildType>(GetName(),FeildType.NAME),
                        new Tuple<object, FeildType>(GetDesc(),FeildType.DESC)
                    },
                    cmd.Parameters
                );

                proc.ExcecSql();

            }
            catch (Exception se)
            {
                SetName(lastName);
                SetDesc(lastDesc);

                ///<todo>
                ///add custom exception
                ///</todo>
                throw new InvalidOperationException("planet was not altered");
            }
        }

        #endregion

        #region get methods

        /// <summary>
        ///     reads attributes for
        ///     galaxy from DB
        ///     into class object
        /// </summary>
        /// <param name="reader"></param>
        public void GetFromDB(IDataReader reader)
        {
            string name = SpaceAccess.GetGalaxyFeild_FromReader(
                Galaxy.FeildType.NAME, reader);
            string desc = SpaceAccess.GetGalaxyFeild_FromReader(
                Galaxy.FeildType.DESC, reader);

            Init(name, desc);
        }

        #endregion

        #endregion

        #region init methods

        private void Init(int id, string name, string desc)
        {
            this.solarSystems = new SolarSystemList(id);
            SetId(id);
            SetName(name);
            SetDesc(desc);
        }

        //for when the id has already been initialised
        private void Init(string name,string desc)
        {
            Init(GetId(), name, desc);
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
                solarSystems.ToString() +
                "END_OF_GALAXY\n",
                id, name, desc
                );
        }

        #endregion

        #region setters

        private void SetId(int id) {
            this.id = id;
        }

        private void SetDesc(string desc)
        {
            if (desc.Length > 200) {
                throw new ArgumentException("desc is too long");
            }
            else { this.desc = desc; }
            
        }

        private void SetName(string name)
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

        public SolarSystemList GetSolarSystems()
        {
            return this.solarSystems;
        }

        #endregion
    }
}
