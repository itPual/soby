using soby.Domain.Abstract;
using soby.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace soby.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int Id)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.Id == Id);
            return View(product);
        }

        //// Перегруженная версия Edit() для сохранения изменений
        //[HttpPost]
        //public ActionResult Edit(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        repository.SaveProduct(product);
        //        TempData["message"] = string.Format("Changes in the product \"{0}\" were saved", product.Name);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        // Что-то не так со значениями данных
        //        return View(product);
        //    }
        //}

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                repository.SaveProduct(product);
                TempData["message"] = string.Format("Changes in the product \"{0}\" were saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            Product deletedProduct = repository.DeleteProduct(Id);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("Product \"{0}\" was deleted",
                    deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}