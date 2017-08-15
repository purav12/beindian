using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebgapeClass
{
    public class AppLogic
    {
        #region Declaration
        static public AppConfig AppConfigTable;
        #endregion

        public static String AppConfigs(String paramName)
        {
            try
            {
                if (AppConfigTable[paramName] != null)
                {
                    return AppConfigTable[paramName].ConfigValue;
                }
                else
                {
                    return "";
                }
            }
            catch (NullReferenceException ex)
            {

                return "";
            }
        }

        public static void ApplicationStart()
        {
            AppConfigTable = new AppConfig();
        }
             
        public static bool AppConfigBool(String paramName)
        {
            String tmp = AppConfigs(paramName).ToUpperInvariant();
            if (tmp == "TRUE" || tmp == "YES" || tmp == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
