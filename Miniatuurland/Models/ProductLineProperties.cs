using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Miniatuurland.Models
{
    public class ProductLineProperties
    {
        [Display(Name = "Productline")]
        public string productLine { get; set; }        
    }
}