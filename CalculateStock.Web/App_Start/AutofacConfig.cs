using Autofac;
using Autofac.Integration.Mvc;
using Bussiness.Interface;
using Bussiness.Service;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculateStock.Web
{
    public class AutofacConfig
    {
        /// <summary>
        /// 负责调用autofac框架实现业务逻辑层和数据仓储层程序集中的类型对象的创建
        /// 负责创建MVC控制器类的对象(调用控制器中的有参构造函数),接管DefaultControllerFactory的工作
        /// </summary>
        public static void Register()
        {
            //实例化一个autofac的创建容器
            var builder = new ContainerBuilder();

            #region 注入service
            builder.RegisterType<RelativeProfitService>().As<IRelativeProfitService>();
            #endregion

            #region 注入 Repository
            builder.RegisterType<RelativeProfitRepository>().As<IRelativeProfitRepository>();
            #endregion

            //注入控制控制器
            builder.RegisterControllers(typeof(MvcApplication).Assembly);


            //创建一个Autofac的容器
            var container = builder.Build();
            //将MVC的控制器对象实例 交由autofac来创建
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}