using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProductCategory.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }

        // Foreign key
        //Navigation Property
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}