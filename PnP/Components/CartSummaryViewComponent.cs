using Microsoft.AspNetCore.Mvc;
using PnP.Models;

namespace PnP.Components {

  public class CartSummaryViewComponent : ViewComponent
  {
    private Cart cart;

    public CartSummaryViewComponent(Cart cartService)
    {
      cart = cartService;
    }

    public IViewComponentResult Invoke()
    {
      return View(cart);
    }
  }
}
