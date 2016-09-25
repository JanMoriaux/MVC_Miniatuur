using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Miniatuurland.Models
{
    public class LoginViewModel
    {
        //[Required]
        //[Display(Name = "Name")]
        //public int huppeldfepup


        //    <label>@Html.DisplayNAmefor<label>
        //    <input>@Html.EditorforModel(p => p.name)

        [Required]
        [Display(Name = "Login")]
        public string username
        {
            get
            {
                return this.username;
            }
                }
        [Required]
        [Display(Name ="Password")]
        public string password
        {
            get
            {
                return this.password;
            }
                }
    }
}