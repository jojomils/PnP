using Microsoft.AspNetCore.Mvc;
using PnP.Models;
using System.Linq;

namespace PnP.Controllers {

  public class OrderController : Controller
  {
    private IOrderRepository repository;
    private Cart cart;

    public OrderController(IOrderRepository repoService, Cart cartService)
    {
      repository = repoService;
      cart = cartService;
    }

    //public ViewResult List() =>
    //    View(repository.Orders.Where(o => !o.Shipped));
    //[HttpPost]
    //public IActionResult MarkShipped(int orderID)
    //{
    //  Order order = repository.Orders
    //      .FirstOrDefault(o => o.OrderID == orderID);
    //  if (order != null)
    //  {
    //    order.Shipped = true;
    //    repository.SaveOrder(order);
    //  }
    //  return RedirectToAction(nameof(List));
    //}

    public ViewResult Checkout() => View(new Order());

    [HttpPost]
    public IActionResult Checkout(Order order)
    {
      if (cart.Lines.Count() == 0)
      {
        //ModelState.AddModelError("", "Sorry, your cart is empty!");
        return RedirectToAction(nameof(Error));
      }
      if (ModelState.IsValid)
      {
        order.Lines = cart.Lines.ToArray();
        repository.SaveOrder(order);
        return RedirectToAction(nameof(Completed));
      }
      else
      {
        return View(order);
      }
    }

    public RedirectToPageResult Error()
    {
      return RedirectToPage("/Error/Error404");
    }

    public ViewResult Completed()
    {
      cart.Clear();
      return View();
    }
  }
}
