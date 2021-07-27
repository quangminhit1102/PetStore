namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ProductAttributes = new HashSet<ProductAttribute>();
            ProductImages = new HashSet<ProductImage>();
        }
        
        public int Id { get; set; }

        [Required]
        [StringLength(50,ErrorMessage = "Tên sản phẩm không quá 50 kí tự!")]
        public string Name { get; set; }

        public int CateId { get; set; }

        public int? BrandId { get; set; }
       
        public string Description { get; set; }
        [Range(1000,20000000,ErrorMessage = "Giá của sản phẩm phải từ 1000 - 20.000.000VNĐ")]
        public int? Price { get; set; }

        [StringLength(128)]
        public string SiteTile { get; set; }

        [StringLength(158)]
        public string MetaKeywords { get; set; }

        [StringLength(158)]
        public string MetaDescription { get; set; }

        public int? ViewCount { get; set; }

        [StringLength(128)]
        public string Video { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? Status { get; set; }

        public double? Evaluate { get; set; }

        public int? TotalEvaluate { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
