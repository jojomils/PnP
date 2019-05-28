using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PnP.Models.ViewModels
{
  public class LoginModel
  {
    [Required]
    public string Name { get; set; }

    [Required]
    [UIHint("Password")]
    public string Password { get; set; }

    public String ReturnUrl { get; set; } = "/";
  }
}
