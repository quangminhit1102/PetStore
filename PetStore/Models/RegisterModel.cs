using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetStore.Models
{
    public class RegisterModel
    {
        public string username { set; get; }
        public string name { set; get; }
        public string email { set; get; }
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 kí tự!")]
        public string password { set; get; }
        [Compare("password", ErrorMessage = "Mật khẩu không trùng khớp!")]
        public string confirmpassword { set; get; }
    }
}