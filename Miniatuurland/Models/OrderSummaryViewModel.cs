using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Miniatuurland.Models
{
    public class OrderSummaryViewModel
    {
        //props van customer 
        [Display(Name = "Name")]
        public string name
        {
            get
            {
                return this.customer.name;
            }
        }
        [Display(Name = "Adress")]
        public string address
        {
            get
            {
                return this.customer.address;
            }
        }
        [Display(Name = "City")]
        public string city
        {
            get
            {
                return this.customer.city;
            }
        }
        [Display(Name = "State")]
        public string state
        {
            get
            {
                return this.customer.state;
            }
        }
        [Display(Name = "Postal Code")]
        public string postalCode
        {
            get
            {
                return this.customer.postalCode;
            }
        }
        
        [Display(Name = "E-mail")]
        public string email
        {
            get
            {
                return this.customer.email;
            }
        }

        //props van order
        [Display(Name = "Order ID")]
        public int orderID
        {
            get
            {
                return this.order.orderID;
            }
        }
        [Display(Name = "Order Date")]
        public Nullable<System.DateTime> orderDate
        {
            get
            {
                return this.order.orderDate;
            }
        }
        [Display(Name = "Required Date")]
        public Nullable<System.DateTime> requiredDate
        {
            get
            {
                return this.order.requiredDate;
            }
        }
        [Display(Name = "Status")]
        public string status
        {
            get
            {
                return this.order.status;
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails
        {
            get
            {
                return this.order.OrderDetails;
            }
        }

        //virtual props
        public virtual Customer customer { get; set; }
        public virtual Order order { get; set; }

        //ctor
        public OrderSummaryViewModel(Customer customer, Order order)
        {
            this.customer = customer;
            this.order = order;
        }
    }
}
