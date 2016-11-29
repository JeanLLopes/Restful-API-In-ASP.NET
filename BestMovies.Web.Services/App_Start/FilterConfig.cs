using System.Web;
using System.Web.Mvc;

namespace BestMovies.Web.Services
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
