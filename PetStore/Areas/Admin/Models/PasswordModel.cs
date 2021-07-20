using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetStore.Areas.Admin.Models
{
    public class PasswordModel
    {
        public string password { set; get; }
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 kí tự!")]
        public string newpassword { set; get; }
        [Compare("newpassword", ErrorMessage = "Mật khẩu không trùng khớp!")]
        public string confirmpassword { set; get; }
    }
}