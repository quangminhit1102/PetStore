namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PostImage
    {
        public int Id { get; set; }

        public int? PostId { get; set; }

        [StringLength(128)]
        public string Image { get; set; }

        public virtual Post Post { get; set; }
    }
}
