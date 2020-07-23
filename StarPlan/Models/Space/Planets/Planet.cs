using StarPlan.Exceptions.PlanetExceptions;
using StarPlan.Exceptions.RegionExceptions;
using StarPlan.Models.Perks;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;

namespace StarPlan.Models.Space.Planets
{
    /// <todo> 
    /// all validation for perks and regions
    /// on loadfromDB method
    /// </todo>
    public abstract class Planet
    {
        protected int id;
        protected string name;

        #region regions
        protected RegionList regions;
        private void VerifyRegionsAmmount()
        {
            int regionCount = regions.GetNumberOfRegions();

            //get regions range
            Point minMaxRegions = Planet.GetRegionsRange(this);
            int minRegions = minMaxRegions.X;
            int maxRegions = minMaxRegions.Y;

            if ((regionCount >= minRegions) && (regionCount <= maxRegions))
            {
                //regions are verified
            }
            else
            {
                throw new RegionOutOfRangeException(maxRegions, minRegions, this);
            }

        }
        #endregion

        #region perks
        protected PerkList perks;
        private void VerifyPerksAmmount()
        {
            int perkCount = perks.GetPerkCount();

            //get number of perks
            int perkNum = GetPlanetPerkNumber(this);

            if (perkCount==perkNum)
            {
                //regions are verified
            }
            else
            {
                throw new PerkCountOutOfRangeException(perkNum, this);
            }
        }
        #endregion

        #region planet size
        public enum PlanetSizeTypes{
            DWARF,
            MEDIUM,
            GIANT
        }
        public static string PlanetSizeTypeToString(PlanetSizeTypes size) {
            switch (size) {
                case PlanetSizeTypes.DWARF:
                case PlanetSizeTypes.MEDIUM:
                case PlanetSizeTypes.GIANT:
                    return size.ToString().ToLower();
                default:
                    throw new ArgumentException("not a valid planet type");

            }
        }
        public static PlanetSizeTypes PlanetStringToSizeType(string size)
        {
            string dwarf = PlanetSizeTypes.DWARF.ToString().ToLower();
            string medium = PlanetSizeTypes.MEDIUM.ToString().ToLower();
            string giant = PlanetSizeTypes.GIANT.ToString().ToLower();
            if (size.Equals(dwarf))
            {
                return PlanetSizeTypes.DWARF;
            }
            else if (size.Equals(medium))
            {
                return PlanetSizeTypes.MEDIUM;
            }
            else if (size.Equals(giant))
            {
                return PlanetSizeTypes.GIANT;
            }
            else
            {
                throw new ArgumentException("not a valid planet type");
            }
        }
        public static Point GetRegionsRange(Planet planet) {
            try
            {
                DwarfPlanet dwarfPlanet = (DwarfPlanet)planet;
                return new Point(2, 3);
            }
            catch (InvalidCastException ice) {
                try
                {
                    MediumPlanet mediumPlanet = (MediumPlanet)planet;
                    return new Point(4, 6);
                }
                catch (InvalidCastException ice_)
                {
                    try
                    {
                        GiantPlanet giantPlanet = (GiantPlanet)planet;
                        return new Point(7, 15);
                    }
                    catch (InvalidCastException ice__)
                    {
                        throw new NotImplementedException("the planet is not implemented yet");
                    }
                }
            }
        }
        public static int GetPlanetPerkNumber(Planet planet)
        {
            try
            {
                DwarfPlanet dwarfPlanet = (DwarfPlanet)planet;
                return 1;
            }
            catch (InvalidCastException ice)
            {
                try
                {
                    MediumPlanet mediumPlanet = (MediumPlanet)planet;
                    return 3;
                }
                catch (InvalidCastException ice_)
                {
                    try
                    {
                        GiantPlanet giantPlanet = (GiantPlanet)planet;
                        return 6;
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
            this.regions = new RegionList(id);
            SetId(id);
            SetName(name);
        }
        private void Init(int id)
        {
            Init(id, "");
        }
        #endregion

        #region edit
        public void Edit(string name) {
            SetName(name);
        }
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
        public string GetName() {
            return this.name;
        }
        public int GetId() {
            return this.id;
        }
        #endregion
    }
}
