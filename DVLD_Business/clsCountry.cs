using DVLD_DataAccess;
using System;
using System.Data;

namespace DVLD_Business
{
    public class clsCountry
    {
        private int _countryId;
        private string _countryName;

        // Setters and Getters
        public int CountryId
        {
            get { return _countryId; }
        }

        public string CountryName
        {
            get { return _countryName; }
        }

        // Constructors

        public clsCountry()
        {
            _countryId = -1;
            _countryName = "";
        }
        private clsCountry(int countryId, string countryName)
        {
            _countryId = countryId;
            _countryName = countryName;
        }

        // Static Method
        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }

        public static clsCountry Find(int countryId)
        {
            string countryName = "";
            if (clsCountryData.GetCountryById(countryId, ref countryName))
                return new clsCountry(countryId, countryName);
            else
                return null;
        }

        public static clsCountry Find(string countryName)
        {
            int id = -1;
            if (clsCountryData.GetCountryByName(ref id, countryName))
                return new clsCountry(id, countryName);
            else
                return null;
        }
    }
}
