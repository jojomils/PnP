using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PnP.Models.ViewModels
{
  public class RegisterModel
  {
    [Required]
    public string Name { get; set; }

    [Required]
    [UIHint("Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [UIHint("Confirm Password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [Compare("Email", ErrorMessage = "The Email and confirmation Email do not match.")]
    public string ConfirmEmail { get; set; }


    

  }
}
