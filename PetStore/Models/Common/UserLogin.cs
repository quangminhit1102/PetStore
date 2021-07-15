using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetStore.Models.Common
{
    [Serializable]
    public class UserLogin
    {
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string Name { set; get; }
        public int Role { set; get; }
    }
}