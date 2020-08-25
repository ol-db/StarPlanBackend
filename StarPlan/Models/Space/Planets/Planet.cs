using StarPlan.Exceptions.PlanetExceptions;
using StarPlan.Exceptions.RegionExceptions;
using StarPlan.Models.Perks;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Data;
using StarPlanDBAccess.Exceptions;
using StarPlanDBAccess.ORM;
using StarPlan.DataAccess;
using StarPlanDBAccess.Procedures;
using Newtonsoft.Json;

namespace StarPlan.Models.Space.Planets
{
    /// <todo> 
    /// all validation for perks and regions
    /// on loadfromDB method
    /// </todo>
    public abstract class Planet
    {
        #region DB Feilds
        public enum FeildType
        {
            ID,
            NAME,
            SIZE
        }

        public static string[] GetFeildNames(FeildType feild)
        {
            if (feild == FeildType.ID)
            {
                return new string[] { "id", "planetId" };
            }
            else if (feild == FeildType.NAME)
            {
                return new string[] { "name", "planetName" };
            }
            else if (feild == FeildType.SIZE)
            {
                return new string[] { "size", "planetSize" };
            }
            else
            {
                throw new FeildNotFound();
            }
        }

        #endregion

        #region id
        protected int id;
        private void VerifyId(int id) {
            if (this.id==id) {
                //id matches
            }
            else
            {
                throw new ArgumentException("id doesn't match");
            }
        }
        #endregion

        protected string name;

        #region regions
        protected RegionList regions;
        #endregion

        #region perks
        protected PerkList perks;
        #endregion

        #region planet size
        public enum SizeTypes{
            DWARF,
            MEDIUM,
            GIANT
        }
        public static string PlanetSizeTypeToString(SizeTypes size) {
            switch (size) {
                case SizeTypes.DWARF:
                case SizeTypes.MEDIUM:
                case SizeTypes.GIANT:
                    return size.ToString().ToLower();
                default:
                    throw new ArgumentException("not a valid planet type");

            }
        }
        public static SizeTypes PlanetStringToSizeType(string size)
        {
            string dwarf = SizeTypes.DWARF.ToString().ToLower();
            string medium = SizeTypes.MEDIUM.ToString().ToLower();
            string giant = SizeTypes.GIANT.ToString().ToLower();
            if (size.Equals(dwarf))
            {
                return SizeTypes.DWARF;
            }
            else if (size.Equals(medium))
            {
                return SizeTypes.MEDIUM;
            }
            else if (size.Equals(giant))
            {
                return SizeTypes.GIANT;
            }
            else
            {
                throw new ArgumentException("not a valid planet type");
            }
        }

        public static string PlanetToSizeString(Planet planet)
        {
            return PlanetSizeTypeToString(GetPlanetSize(planet));
        }

        public static Point GetRegionsRange(Planet planet) {

            SizeTypes size = GetPlanetSize(planet);
            switch (size)
            {
                case SizeTypes.DWARF:
                    return new Point(2, 3);
                case SizeTypes.MEDIUM:
                    return new Point(4, 6);
                case SizeTypes.GIANT:
                    return new Point(7, 15);
                default:
                    throw new NotImplementedException("the planet is not implemented yet");
            }
        }
        public static int GetPlanetPerkNumber(Planet planet)
        {
            SizeTypes size = GetPlanetSize(planet);
            switch (size)
            {
                case SizeTypes.DWARF:
                    return 1;
                case SizeTypes.MEDIUM:
                    return 3;
                case SizeTypes.GIANT:
                    return 6;
                default:
                    throw new NotImplementedException("the planet is not implemented yet");
            }
        }
        public static SizeTypes GetPlanetSize(Planet planet) {
            try
            {
                DwarfPlanet dwarfPlanet = (DwarfPlanet)planet;
                return SizeTypes.DWARF;
            }
            catch (InvalidCastException ice)
            {
                try
                {
                    MediumPlanet mediumPlanet = (MediumPlanet)planet;
                    return SizeTypes.MEDIUM;
                }
                catch (InvalidCastException ice_)
                {
                    try
                    {
                        GiantPlanet giantPlanet = (GiantPlanet)planet;
                        return SizeTypes.GIANT;
                    }
                    catch (InvalidCastException ice__)
                    {
                        throw new NotImplementedException("the planet is not implemented yet");
                    }
                }
            }
        }
        #endregion

        public Planet(int id,string name) {
            Init(id, name);
        }

        public Planet(int id)
        {
            Init(id);
        }

        /// <summary>
        /// verifies that a given planet has the right number of regions
        /// </summary>

        #region init
        private void Init(int id, string name)
        {
            this.perks = new PerkList(id);
            this.regions = new RegionList(id,GetRegionsRange(this));
            SetId(id);
            SetName(name);
        }

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

        public void GetFromDB(IDataReader reader) 
        {
            string name = SpaceAccess.GetPlanetFeild_FromReader(
                Planet.FeildType.NAME, reader);

            Init(name);
        }

        #region edit
        /// <todo>
        /// update size
        /// </todo>
        public void EditInDB(string name,ISqlStoredProc proc)
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
                proc.SetProcName("EditPlanet");

                SpaceAccess.SetPlanetParams
                (
                    new List<Tuple<object, Planet.FeildType>>() {
                        new Tuple<object, Planet.FeildType>(GetId(), Planet.FeildType.ID),
                        new Tuple<object, Planet.FeildType>(GetName(),Planet.FeildType.NAME),
                        new Tuple<object, Planet.FeildType>(PlanetSizeTypeToString(GetPlanetSize(this)), Planet.FeildType.SIZE)
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
                throw new InvalidOperationException("planet was not altered");
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
                    name = GetName(),
                    size = Planet.PlanetToSizeString(this),
                    regions = regions.ToObj()
                };
        }

        public string ToJsonSingle()
        {
            return JsonConvert.SerializeObject
            (
                ToObjSingle()
            );
        }

        public object ToObjSingle()
        {
            return
                new
                {
                    id = GetId(),
                    name = GetName(),
                    size = Planet.PlanetToSizeString(this)
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
        public string GetName() {
            return this.name;
        }
        public int GetId() {
            return this.id;
        }
        public RegionList GetRegions()
        {
            return this.regions;
        }

        #endregion
    }
}
