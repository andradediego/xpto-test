using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Xpto.Repository;

namespace XptoWeb
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IClienteRepository, ClienteRespository>();
            container.RegisterType<IProdutoRepository, ProdutoRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}