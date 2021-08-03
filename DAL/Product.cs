using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name="Product Id")]
        public int ProductId { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Required]
        public string Description { get; set; }

        [Display(Name="Unit Price")]
        public decimal? UnitPrice { get; set; }

        //foreign key
        [ForeignKey("Category")] //Category is propertyName
        [Display(Name=("Category"))]
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; } //propertyName
    }

    public class Category
    {
        public int CategoryId { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string Name { get; set; }
    }


    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
    }
}
