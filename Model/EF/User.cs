namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Orders = new HashSet<Order>();
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(32)]
        public string Password { get; set; }

        public int Role { get; set; }

        [Required]
        [StringLength(50,ErrorMessage ="Họ tên không được quá 50 kí tự!")]
        public string FullName { get; set; }

        public bool? Gender { get; set; }

        [StringLength(100)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail không hợp lệ")]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date, ErrorMessage = "Vui lòng nhập Ngày hợp lệ!")]
        public DateTime? Birthday { get; set; }

        [StringLength(100, ErrorMessage = "Địa chỉ không được quá 100 kí tự!")]
        public string Address { get; set; }

        [StringLength(11,ErrorMessage = "Số điện thoại không được quá 11 kí tự!")]
        public string Phone { get; set; }

        public bool? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Posts { get; set; }

        public virtual Role Role1 { get; set; }
    }
}
