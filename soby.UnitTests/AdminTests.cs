using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using soby.Controllers;
using soby.Domain.Abstract;
using soby.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace soby.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Games()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { Id = 1, Name = "Product1"},
                new Product { Id = 2, Name = "Product2"},
                new Product { Id = 3, Name = "Product3"},
                new Product { Id = 4, Name = "Product4"},
                new Product { Id = 5, Name = "Product5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            List<Product> result = ((IEnumerable<Product>)controller.Index().
                ViewData.Model).ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Product1", result[0].Name);
            Assert.AreEqual("Product2", result[1].Name);
            Assert.AreEqual("Product3", result[2].Name);
        }

        [TestMethod]
        public void Can_Edit_Game()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { Id = 1, Name = "Product1"},
                new Product { Id = 2, Name = "Product2"},
                new Product { Id = 3, Name = "Product3"},
                new Product { Id = 4, Name = "Product4"},
                new Product { Id = 5, Name = "Product5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            Product product1 = controller.Edit(1).ViewData.Model as Product;
            Product product2 = controller.Edit(2).ViewData.Model as Product;
            Product product3 = controller.Edit(3).ViewData.Model as Product;

            // Assert
            Assert.AreEqual(1, product1.Id);
            Assert.AreEqual(2, product2.Id);
            Assert.AreEqual(3, product3.Id);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Game()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { Id = 1, Name = "Product1"},
                new Product { Id = 2, Name = "Product2"},
                new Product { Id = 3, Name = "Product3"},
                new Product { Id = 4, Name = "Product4"},
                new Product { Id = 5, Name = "Product5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            Product result = controller.Edit(6).ViewData.Model as Product;

            // Assert
        }

        // ...

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта Game
            Product product = new Product { Name = "Test" };

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(product);

            // Утверждение - проверка того, что к хранилищу производится обращение
            mock.Verify(m => m.SaveProduct(product));

            // Утверждение - проверка типа результата метода
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта Game
            Product product = new Product { Name = "Test" };

            // Организация - добавление ошибки в состояние модели
            controller.ModelState.AddModelError("error", "error");

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(product);

            // Утверждение - проверка того, что обращение к хранилищу НЕ производится 
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        // ...

        [TestMethod]
        public void Can_Delete_Valid_Games()
        {
            // Организация - создание объекта Game
            Product product = new Product { Id = 2, Name = "Product2" };

            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { Id = 1, Name = "Product1"},
                new Product { Id = 2, Name = "Product2"},
                new Product { Id = 3, Name = "Product3"},
                new Product { Id = 4, Name = "Product4"},
                new Product { Id = 5, Name = "Product5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие - удаление игры
            controller.Delete(product.Id);

            // Утверждение - проверка того, что метод удаления в хранилище
            // вызывается для корректного объекта Game
            mock.Verify(m => m.DeleteProduct(product.Id));
        }
    }
}
