using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;
using soby.Domain.Entities;
using soby.Domain.Abstract;
using soby.Controllers;

namespace soby.UnitTests
{
        [TestClass]
        public class ImageTests
        {
            [TestMethod]
            public void Can_Retrieve_Image_Data()
            {
                // Организация - создание объекта Game с данными изображения
                Product product = new Product
                {
                    Id = 2,
                    Name = "Product2",
                    ImageData = new byte[] { },
                    ImageMimeType = "image/png"
                };

                // Организация - создание имитированного хранилища
                Mock<IProductRepository> mock = new Mock<IProductRepository>();
                mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product {Id = 1, Name = "Product1"},
                product,
                new Product {Id = 3, Name = "Product3"}
            }.AsQueryable());

            // Организация - создание контроллера
            ProductController controller = new ProductController(mock.Object);

                // Действие - вызов метода действия GetImage()
                ActionResult result = controller.GetImage(2);

                // Утверждение
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(FileResult));
                Assert.AreEqual(product.ImageMimeType, ((FileResult)result).ContentType);
            }

            [TestMethod]
            public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
            {
                // Организация - создание имитированного хранилища
                Mock<IProductRepository> mock = new Mock<IProductRepository>();
                mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product {Id = 1, Name = "Product1"},
                new Product {Id = 2, Name = "Product2"}
            }.AsQueryable());

            // Организация - создание контроллера
            ProductController controller = new ProductController(mock.Object);

                // Действие - вызов метода действия GetImage()
                ActionResult result = controller.GetImage(10);

                // Утверждение
                Assert.IsNull(result);
            }
        }
}

