using System;
using DVLD_DataAccess;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsApplicationsTypes
    {
        private int _id = -1;
        private string _title = "";
        private double _fees = 0;

        // Setters and Getters
        public int Id
        {
            get { return _id; }
        }

        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }

        public double Fees
        {
            set { _fees = value; }
            get { return _fees; }
        }

        // Construtors
        private clsApplicationsTypes(int id, string title, double fees)
        {
            _id = id;
            _title = title;
            _fees = fees;
        }

        // Non Static Methods
        private bool _Update()
        {
            return clsApplicationsTypesData.Update(_id, _title, _fees);
        }

        // Static Methods
        public static DataTable GetAllApplicationsTypes()
        {
            return clsApplicationsTypesData.GetAllApplicationsTypes();
        }

        public static clsApplicationsTypes Find(int applicationTypeId)
        {
            string title = "";
            double fees = 0;

            if (clsApplicationsTypesData.Find(applicationTypeId, ref title, ref fees))
                return new clsApplicationsTypes(applicationTypeId, title, fees);
            else
                return null;
        }

        public bool Save()
        {
            return _Update();
        }
    }
}
