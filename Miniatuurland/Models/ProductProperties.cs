using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Miniatuurland.Models
{
    public  class ProductProperties
    {
        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString ="{0:€ #,##0.00}")]
        public decimal buyPrice { get; set; }
        [Display(Name = "Product Name")]
        public string product { get; set; }               
        [Display(Name = "Scale")]
        public string scale { get; set; }
        [Display(Name ="Description")]
        public string description { get; set; }
        [Display(Name = "In Stock")]
        public Nullable<short> quantityInStock { get; set; }
        [Display(Name = "In Order")]
        public Nullable<short> quantityInOrder { get; set; }  

                      
    }
}