using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Miniatuurland
{
    public class CustomerExistsAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            if (!(value is string))
            {
                return false;
            }
            else
            {
                Services.ProductService db = new Services.ProductService();
                return (!db.UsernameExists((string)value));
            }

        }
    }
}