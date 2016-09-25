using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Miniatuurland.Models;

namespace Miniatuurland.Services
{
    public class ProductService
    {
        //connectie met de database
        private miniatuurlandEntities1 db = new miniatuurlandEntities1();
        
        //Productenlijst per genre
        public List<Product> ListByProductLineId(int id)
        {            
            var query = db.Products.Where(p => p.productlineID == id).OrderBy(p => p.product).ToList();
            if (query.Count != 0)
            {
                return query;
            }
            else
            {
                return null;
            }
        }
        
        //alle productLines
        public List<ProductLine> GetProductLines()
        {
            var query = db.ProductLines.OrderBy(p => p.productLine).ToList();
            return query;
        }
        
        //één product op Id opvragen
        public Product GetProductById(int id)
        {
            var query = db.Products.Find(id);
            return query;
        }
        //één productlijn op Id opvragen
        public ProductLine GetProductLineById(int id)
        {
            var query = db.ProductLines.Find(id);
            return query;
        }
        //klant terugvinden in database op username en password
        public Customer GetCustomerByUsernameAndPassword(string username, string password)
        {
            var query = (from customer in db.Customers
                         where customer.username.ToUpper() == username.ToUpper()
                         && customer.password == password
                         select customer).FirstOrDefault();
            return query;
        }
        //check of username al in gebruik is
        public bool UsernameExists(string username)
        {
            var count = db.Customers.Where(p => p.username == username).Count();
            return (count > 0);
        }
        //nieuwe user toevoegen
        public bool RegisterUser(Customer customer)
        {
            db.Customers.Add(customer);
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        //winkelkar toevoegen aan order en order teruggeven aan controller
        public Order CreateNewOrder(Customer customer, List<CartItem> cartitems)
        {
            Order order = new Order();

            //cartitems als orderdetail toevoegen aan order 
            foreach (var cartitem in cartitems)
            {
                cartitem.Product = GetProductById(cartitem.itemId);
                var orderdetail = new OrderDetail()
                {
                    productID = cartitem.itemId,
                    quantityOrdered = (short?)cartitem.quantity,
                    priceEach = cartitem.Product.buyPrice,
                    
                };
                order.OrderDetails.Add(orderdetail);

                //beschikbare stock aanpassen
                var product = cartitem.Product;
                product.quantityInStock -= (short?)cartitem.quantity;                
            }            

            //leverdata in order setten en status updaten
            order.orderDate = DateTime.Now;
            order.requiredDate = DateTime.Now.AddDays(14);
            order.status = "In progress";

            //order toewijzen aan klant
            order.customerID = customer.customerID;
            
            try
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return order;
            }
            catch (Exception)
            {
                return null;                
            }
        }        
    }
}