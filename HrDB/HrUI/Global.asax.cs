using Autofac;
using Autofac.Integration.Mvc;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
namespace HrUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            //�ѵ�ǰ�����е� Controller ��ע��
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            Assembly[] assemblies = new Assembly[] { Assembly.Load("HrDAO"), Assembly.Load("HrBLL") };
            builder.RegisterAssemblyTypes(assemblies)
            .Where(type => !type.IsAbstract)
            .AsImplementedInterfaces().PropertiesAutowired();
            var container = builder.Build();
            //ע��ϵͳ����� DependencyResolver�������� MVC ��ܴ��� Controller �ȶ����ʱ���ǹ� Autofac Ҫ����
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));//!!!
        }
    }
}
