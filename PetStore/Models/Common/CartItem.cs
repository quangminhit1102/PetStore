using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetStore.Models.Common
{
    public class CartItem
    {
        public Product Product { set; get; }
        public int Quantity { set; get; }
        public int? Price { set; get; }
        public String AttributeName { set; get; }
        public decimal Total { set; get; }
        //public int total
    //    {
    //        get { return Quantity * Product.Price; }
    //    }
    //}

    //public CartItem(Product product, int quantity, int total)
    //{

    }
}