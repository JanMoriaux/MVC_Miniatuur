using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Miniatuurland.Models;
using System.ComponentModel.DataAnnotations;

namespace Miniatuurland.Models
{
    [MetadataType(typeof(ProductProperties))]
    public partial class Product
    {

        public int actualStock
        {
            get
            {
                if (this.quantityInStock.HasValue)
                {
                    if (this.quantityInOrder.HasValue)
                    {
                        return this.quantityInStock.Value - this.quantityInOrder.Value;
                    }
                    else
                    {
                        return this.quantityInStock.Value - 0;
                    }
                }
                else
                {
                    if (this.quantityInOrder.HasValue)
                    {
                        return - this.quantityInOrder.Value;
                    }
                    else
                    {
                        return 0;
                    }
                }
                
            }
        }

    }
}