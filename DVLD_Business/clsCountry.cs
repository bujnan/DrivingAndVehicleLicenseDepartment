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

        // Static Method
        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }
    }
}
