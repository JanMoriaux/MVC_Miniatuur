using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Miniatuurland.Models;
using Miniatuurland.Services;

namespace Miniatuurland.Controllers
{
    public class CartController : Controller
    {
        private ProductService service = new ProductService();
        // GET: Cart
        public ActionResult Overview()
        {
            List<CartItem> cart;
            if (Session["cart"] != null)
            {
                cart = Session["cart"] as List<CartItem>;
            }
            else
            {
                cart = new List<CartItem>();
            }
            return View(cart);
        }

        public ActionResult Add(int productId)
        {
            List<CartItem> cart;
            if (Session["cart"] != null)
            {
                cart = Session["cart"] as List<CartItem>;
            }
            else
            {
                cart = new List<CartItem>();
                Session["cart"] = cart;
            }
            Product product = service.GetProductById(productId);
            var cartitem = cart.Find(m => m.itemId == productId);
            //var cartitem = (from item in cart where item.itemId == product.productID select item).FirstOrDefault();
            if (cartitem != null)
            {
                if (cartitem.Product.actualStock > cartitem.quantity && (cartitem.quantity > 0))
                {
                    cartitem.quantity += 1;
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, cartitem.itemName + " : ordered quantity must be between 0 and " + cartitem.Product.actualStock + ".");
                    ViewBag.errorcount = ModelState.Values.Count();
                    return View("Overview", cart);
                }
            }
            else
            {
                cartitem = new CartItem();
                cartitem.itemId = product.productID;
                cartitem.itemName = product.product;
                cartitem.quantity = 1;
                cartitem.Product = product;
                cart.Add(cartitem);
            }
            return RedirectToAction("Overview", "Cart");
        }
        public ActionResult Delete(int productId)
        {
            List<CartItem> cart;
            if (Session["cart"] != null)
            {
                cart = Session["cart"] as List<CartItem>;
            }
            else
            {
                cart = new List<CartItem>();
                Session["cart"] = cart;
            }
            Product product = service.GetProductById(productId);
            var cartitem = cart.Find(m => m.itemId == productId);
            if (cartitem != null)
            {
                cart.Remove(cartitem);
            }
            return RedirectToAction("Overview", "Cart");
        }
        public ActionResult Empty()
        {
            Session["cart"] = null;
            return RedirectToAction("Overview", "Cart");
        }
        public ActionResult Update(int productId, int quantity)
        {
            if (Session["cart"] != null)
            {
                List<CartItem> cart = Session["cart"] as List<CartItem>;
                var cartItem = cart.Find(m => m.itemId == productId);
                if (cartItem.Product.actualStock >= quantity && quantity > 0)
                {
                    cartItem.quantity = quantity;
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, cartItem.itemName + " : ordered quantity must be between 0 and " + cartItem.Product.actualStock + ".");
                    ViewBag.errorcount = ModelState.Values.Count();
                    return View("Overview", cart);
                }

            }
            return RedirectToAction("Overview", "Cart");
        }

        public ActionResult CheckoutConfirm()
        {
            if (Session["cart"] == null)
            {
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        public ActionResult Checkout()
        {
            //pagina onbereikbaar als niet ingelogd of winkelkar bestaat niet/ is leeg
            if (Session["cart"] == null || Session["customer"] == null)
            {
                return RedirectToAction("Index", "Product");
            }
            List<CartItem> cart = Session["cart"] as List<CartItem>;
            if (cart.Count == 0)
            {
                return RedirectToAction("Index", "Product");
            }
            
            //anders nieuw order/orderdetails aanmaken (zie ook ProductService)
            else
            {
                var cartitems = Session["cart"] as List<CartItem>;
                
                //session leegmaken
                Session["cart"] = null;

                var customer = Session["customer"] as Customer;                               
                var order = service.CreateNewOrder(customer, cartitems);
                var summary = new OrderSummaryViewModel(customer, order);
                               
                                
                return View(summary);
            }
            
        }
    }
}