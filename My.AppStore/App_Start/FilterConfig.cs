using System.Web;
using System.Web.Mvc;

namespace My.AppStore
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CartItemCalculatorAttribute());
        }
    }
}
