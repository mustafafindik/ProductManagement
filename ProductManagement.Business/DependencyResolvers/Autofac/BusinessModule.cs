using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using ProductManagement.Business.Abstract;
using ProductManagement.Business.Concrete;
using ProductManagement.DataAccess.Abstract;
using ProductManagement.DataAccess.Concrete.EntityFrameworkCore;

namespace ProductManagement.Business.DependencyResolvers.Autofac
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductService>().As<IProductService>().SingleInstance();
            builder.RegisterType<ProductRepository>().As<IProductRepository>().SingleInstance();
        }
    }
}
