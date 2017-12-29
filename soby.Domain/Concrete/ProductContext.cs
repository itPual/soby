using soby.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soby.Domain.Concrete
{
    class ProductContext : DbContext
    {
        //public UserContext()
        //    :base("DbConnection")
        //{ }
        public DbSet<Product> Products { get; set; }
    }
}
