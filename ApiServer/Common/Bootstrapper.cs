using System.Configuration;
using System.Web;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace ApiServer.Common
{
    public class Bootstrapper
    {
        private static IUnityContainer unityContainer;
        /// <summary>
        /// 初始化依赖对象
        /// </summary>
        public static void Initialise(HttpConfiguration config)
        {
            var container = BuildUnityContainerFormConfig();

            ////mvc注入
            //DependencyResolver.SetResolver(new UnityDependencyReslover(container));

            //webapi 注入

            //GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            config.DependencyResolver = new UnityResolver(container);
            unityContainer = container;
        }
        /// <summary>
        /// 获取容器内指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            try
            {
                return unityContainer.Resolve<T>();
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// 注册依赖对象
        /// </summary>
        /// <returns></returns>
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            //container.RegisterType<IUserManager, Server.UserManager>();
            return container;
        }
        
        /// <summary>
        /// 根据配置绑定容器对象
        /// </summary>
        /// <returns></returns>
        private static IUnityContainer BuildUnityContainerFormConfig()
        {
            var container = new UnityContainer();
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = HttpContext.Current.Server.MapPath("~/Config/Unity.config") };
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var unitySection = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);
            container.LoadConfiguration(unitySection, "MyContainer");
            return container;

            // var unitySection = ConfigurationManager.GetSection(UnityConfigurationSection.SectionName) as UnityConfigurationSection; //配置在web.config中
            // unitySection.Configure(container, "MyContainer");  
        }

    }
}