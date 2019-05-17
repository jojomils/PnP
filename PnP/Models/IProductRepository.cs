using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PnP.Models
{
  public interface IProductRepository
  {
    IQueryable<Product> Products { get; }

    //To remove comments after removing the FakeProductRepository
    void SaveProduct(Product product);

    Product DeleteProduct(int productId);
  }
}
