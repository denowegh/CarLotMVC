using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using System.Data.Entity;
using AutoLotDAL.EF;

namespace CarLotMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MyDataInitializer());
            /*В результате база данных будет удаляться и воссоздаваться каждый раз, когда приложение запускается. Такая 
             * функциональная возможность великолепно подходит для тестирования, но при развертывании производственного 
             * приложения вы должны дважды, а то и трижды проверить, удалили ли данную строку кода! Она также будет влиять 
             * на время запуска приложения, поэтому если вы заметили, что запуск требует довольно много времени, тогда 
             * закомментируйте добавленную выше строку.*/


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
