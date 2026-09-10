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
        public static DataTable GetAllApplicationsTypes()
        {
            return clsApplicationsTypesData.GetAllApplicationsTypes();
        }
    }
}
