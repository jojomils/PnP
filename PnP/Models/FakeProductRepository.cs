using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PnP.Models
{
  public class FakeProductRepository 
  {
    public IQueryable<Product> Products => new List<Product>
    {
      new Product{Name ="Keyboard", Price = 199},
      new Product{Name = "Mouse", Price = 99},
      new Product{Name = "Monitor", Price = 799}
    }.AsQueryable<Product>();
  }
}
