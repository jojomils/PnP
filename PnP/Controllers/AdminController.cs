using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PnP.Models;
using System.Linq;

namespace PnP.Controllers {

  [Authorize(Roles = "Admin")]
  public class AdminController : Controller
  {
    private IProductRepository repository;

    private IOrderRepository reps;

    public AdminController(IProductRepository repo, IOrderRepository rep)
    {
      repository = repo;
      reps = rep;
    }

    public ViewResult Index() => View(repository.Products);

    public ViewResult Edit(int productId) =>
        View(repository.Products
            .FirstOrDefault(p => p.ProductId == productId));

    [HttpPost]
    public IActionResult Edit(Product product)
    {
      if (ModelState.IsValid)
      {
        repository.SaveProduct(product);
        TempData["message"] = $"{product.Name} has been saved";
        return RedirectToAction("Index");
      }
      else
      {
        // there is something wrong with the data values
        return View(product);
      }
    }

    //This is to call the order controller List to view it.
    public ViewResult Unshipped() =>
       View(reps.Orders.Where(o => !o.Shipped));

    //This is to call the Shipped View to view all shipped orders
    public ViewResult Shipped() => View(reps.Orders.Where(s => s.Shipped));

    //This is to call the unshipped view to see all unshipped orders
    public ViewResult List() => View(reps.Orders);

    [HttpPost]
    public IActionResult MarkShipped(int orderID)
    {
      Order order = reps.Orders
          .FirstOrDefault(o => o.OrderID == orderID);
      if (order != null)
      {
        order.Shipped = true;
        reps.SaveOrder(order);
      }
      return RedirectToAction(nameof(List));
    }

    //Creates a new product
    public ViewResult Create() => View("Edit", new Product());

    [HttpPost]
    public IActionResult Delete(int productId)
    {
      Product deletedProduct = repository.DeleteProduct(productId);
      if (deletedProduct != null)
      {
        TempData["message"] = $"{deletedProduct.Name} was deleted";
      }
      return RedirectToAction("Index");
    }
  }
}
