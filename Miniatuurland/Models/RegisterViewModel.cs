using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Miniatuurland.Models
{
    public class RegisterViewModel
    {
        //[Required]
        //[Display(Name = "Name")]
        //public int huppeldfepup


        //    <label>@Html.DisplayNAmefor<label>
        //    <input>@Html.EditorforModel(p => p.name)
        [Required(ErrorMessage = "\"Name\" is a required field")]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "\"address\" is a required field")]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required(ErrorMessage = "\"City\" is a required field")]
        [Display(Name = "City")]
        public string city { get; set; }
        
        [Display(Name = "State")]
        public string state { get; set; }

        [Required(ErrorMessage = "\"Postal Code\" is a required field")]
        [Display(Name = "Postal Code")]
        public string postalCode { get; set; }

        [Required(ErrorMessage = "\"Username\" is a required field")]
        [CustomerExists(ErrorMessage = "There's already a user by that name in our database.")]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required(ErrorMessage = "\"Password\" is a required field")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "\"Repeat Password\" is a required field")]
        [Display(Name = "Repeat Password")]
        [DataType(DataType.Password)]
        [Compare("password",ErrorMessage = "{0} and {1} contain different values")]
        public string repeatPassword { get; set; }

        [Required(ErrorMessage = "\"E-mail\" is a required field")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }        
    }
}