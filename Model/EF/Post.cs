namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            PostImages = new HashSet<PostImage>();
        }

        public int Id { get; set; }

        public int CataId { get; set; }

        
        [StringLength(100, ErrorMessage = "Tiêu Đề không quá 100 ký tự")]
        [Required]
        public string Title { get; set; }

        [StringLength(255)]
        public string SiteTitle { get; set; }

        [Required]
        public string Content { get; set; }

        [StringLength(128)]
        public string Image { get; set; }

        [StringLength(158)]
        public string MetaKeyword { get; set; }

        [StringLength(158)]
        public string MetaDescription { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? Status { get; set; }

        public virtual Catalog Catalog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostImage> PostImages { get; set; }

        public virtual User User { get; set; }
    }
}
