using Moq;
using Ninject.Modules;
using soby.Domain.Abstract;
using soby.Domain.Concrete;
using soby.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace soby.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            //Bind<IRepository>().To<BookRepository>();
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //    new Product { Name = "SimCity", Price = 1499 },
            //    new Product { Name = "TITANFALL", Price=2299 },
            //    new Product { Name = "Battlefield 4", Price=899.4M }
            //});
            //Bind<IProductRepository>().ToConstant(mock.Object);
            Bind<IProductRepository>().To<ProductRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                   .AppSettings["Email.WriteAsFile"] ?? "false")
            };
            Bind<IOrderProcessor>().To<EmailOrderProcessor>()
               .WithConstructorArgument("settings", emailSettings);
            //Bind<IOrderProcessor>().To<EmailOrderProcessor>();
        }
    }
}