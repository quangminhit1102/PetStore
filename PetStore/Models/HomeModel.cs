using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetStore.Models
{
    public class HomeModel
    {
        public List<Product> listProduct{set;get;}
        public List<Category> listCategory {set;get;}
        public List<Post> listPost { set;get; }
    }

}