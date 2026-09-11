using System;
using DVLD_DataAccess;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTestType
    {
        private int _testId = -1;
        private string _title = "";
        private string _description = "";
        private double _fees = 0;

        // Setters and Getters
        public int Id
        {
            get { return _testId; }
        }

        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }

        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }

        public double Fees
        {
            set { _fees = value; }
            get { return _fees; }
        }

        // Constructors
        private clsTestType(int id, string title, string description, double fees)
        {
            _testId = id;
            _title = title;
            _description = description;
            _fees = fees;
        }

        // Non Static Methods

        private bool _Update()
        {
            return clsTestTypeData.Update(_testId, _title, _description, _fees);
        }
        public bool Save()
        {
            return _Update();
        }

        // Static Methods
        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTesttypes();
        }

        public static clsTestType Find(int testTypeId)
        {
            string title = "";
            string description = "";
            double fees = 0;

            if (clsTestTypeData.Find(testTypeId, ref title, ref description, ref fees))
                return new clsTestType(testTypeId, title, description, fees);
            else
                return null;
        }
    }
}
