using Autofac;
using Business.Managers;
using Business.Services;
using Core.Utilities.Cache;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeeManager>().As<IEmployeeService>();
            builder.RegisterType<EfEmployeeDal>().As<IEmployeeDal>();

            builder.RegisterType<RedisCache>().As<ICache>();

            builder.RegisterType<LoggerManager>().As<ILoggerService>();
        }
    }
}
