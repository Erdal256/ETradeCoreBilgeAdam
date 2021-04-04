using AppCore.Records.Bases;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class ProductModel :RecordBase
    {

        [Required(ErrorMessage = "{0} is required!")]
        //[StringLength(200 , ErrorMessage = "{0} must be maximum {1} characters!")]
        [MinLength(2,ErrorMessage ="{0} must be minimum {1} characters!")]
        [MaxLength(200,ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Description { get; set; }

        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }

        [DisplayName("Stock Amount")]
        [Range(0,Int32.MaxValue,ErrorMessage ="{0},must be between {1} and {2}!")]
        public int StockAmount { get; set; }

        [DisplayName("Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public CategoryModel Category { get; set; }

    }
}
