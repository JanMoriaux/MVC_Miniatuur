using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Miniatuurland.Models
{
    public class CartItem
    {
        public int customerId { get; set; }

        [Display(Name = "ID")]
        public int itemId { get; set; }
        [Display(Name = "Quantity")]
        public int quantity { get; set; }

        [Display(Name = "Product Name")]
        public string itemName { get; set; }

        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:€ #,##0.00}")]
        public decimal itemPrice
        {
            get
            {
                return quantity * (decimal)this.Product.buyPrice;
            }
        }



        //navigationproperties
        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}