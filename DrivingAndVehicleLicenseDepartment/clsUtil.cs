using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingAndVehicleLicenseDepartment
{
    internal class clsUtil
    {
        private static bool _CreateDirectory(string directoryName)
        {
            try
            {
                Directory.CreateDirectory(directoryName);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static string _GetGUID()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        private static string _ChangeImageFileNameToGUID(string sourceFile)
        {
            FileInfo fileInfo = new FileInfo(sourceFile);
            string fileExtension = fileInfo.Extension;
            return _GetGUID() + fileExtension;
        }
        public static bool CopyImageProfileIntoProjectImages(ref string sourceFile)
        {
            string projectImagesDirectory = @"C:\DVLD-Profile-Images\";
            if (!_CreateDirectory(projectImagesDirectory))
                return false;

            string distinationFile = projectImagesDirectory + _ChangeImageFileNameToGUID(sourceFile);
            try
            {
                File.Copy(sourceFile, distinationFile, true);
                sourceFile = distinationFile;
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
