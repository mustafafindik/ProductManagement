using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using ProductManagement.Business.Abstract;
using ProductManagement.Business.Concrete;
using ProductManagement.Core.Utilities.Security.Jwt;
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

            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<UserRepository>().As<IUserRepository>().SingleInstance();

            builder.RegisterType<AuthRepository>().As<IAuthRepository>();
            builder.RegisterType<AuthService>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
        }
    }
}
