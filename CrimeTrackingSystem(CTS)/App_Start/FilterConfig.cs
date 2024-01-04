using System.Web;
using System.Web.Mvc;

namespace CrimeTrackingSystem_CTS_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
