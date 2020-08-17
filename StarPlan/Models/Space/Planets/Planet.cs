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
        private void VerifyRegionsAmmount(RegionList regions)
        {
            int regionCount = regions.GetNumberOfRegions();

            //get regions range
            Point minMaxRegions = Planet.GetRegionsRange(this);
            int minRegions = minMaxRegions.X;
            int maxRegions = minMaxRegions.Y;

            if ((regionCount >= minRegions) && (regionCount <= maxRegions))
            {
                //verify that ids match
                try
                {
                    VerifyId(regions.GetPlanetId());
                }
                catch (ArgumentException ae)
                {
                    throw new RegionsPlanetDoesntMatchException(regions, this);
                }
            }
            else
            {
                throw new RegionOutOfRangeException(maxRegions, minRegions, this);
            }

        }
        #endregion

        #region perks
        protected PerkList perks;
        private void VerifyPerksAmmount(PerkList perks)
        {
            int perkCount = perks.GetPerkCount();

            //get number of perks
            int perkNum = GetPlanetPerkNumber(this);

            if (perkCount==perkNum)
            {
                //verify that ids match
                try
                {
                    VerifyId(perks.GetPlanetId());
                }
                catch (ArgumentException ae) {
                    throw new PerksPlanetDoesntMatchException(perks, this);
                }
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

        #region DB methods

        #region edit
        public void Edit(string name) {
            SetName(name);
        }
        #endregion

        #endregion

        #region setters
        private void SetId(int id) {
            this.id = id;
        }
        public void SetRegions(RegionList regions)
        {
            VerifyRegionsAmmount(regions);
            this.regions = regions;
            
        }
        public void SetPerks(PerkList perks)
        {
            VerifyPerksAmmount(perks);
            this.perks = perks;
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
