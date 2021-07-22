namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductAttribute
    {
        public int? ProductId { get; set; }

        public int? AttributeValueId { get; set; }

        public int? Price { get; set; }

        public int Id { get; set; }

        public virtual AttributeValue AttributeValue { get; set; }

        public virtual Product Product { get; set; }

        public List<Model.EF.AttributeValue> listAttributeValue = new List<AttributeValue>();
    }
}
