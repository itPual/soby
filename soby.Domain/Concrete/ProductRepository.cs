using soby.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using soby.Domain.Entities;

namespace soby.Domain.Concrete
{
    public class ProductRepository : IProductRepository
    {
        //public IEnumerable<Product> Products => throw new NotImplementedException();

        ProductContext context = new ProductContext();

        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        //public void SaveProduct(Product product)
        //{
        //    if (product.Id == 0)
        //        context.Products.Add(product);
        //    else
        //    {
        //        Product dbEntry = context.Products.Find(product.Id);
        //        if (dbEntry != null)
        //        {
        //            dbEntry.Name = product.Name;
        //            dbEntry.Description = product.Description;
        //            dbEntry.Price = product.Price;
        //            dbEntry.Category = product.Category;
        //        }
        //    }
        //    context.SaveChanges();
        //}

        public void SaveProduct(Product product)
        {
            if (product.Id == 0)
                context.Products.Add(product);
            else
            {
                Product dbEntry = context.Products.Find(product.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int Id)
        {
            Product dbEntry = context.Products.Find(Id);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
