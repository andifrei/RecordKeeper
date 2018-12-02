using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RecordKeeper.Models
{
    public class RecordItem
    {
        public int ID { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string Artist { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Album { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string StoreLocation { get; set; }
        public string Type { get; set; }
        [Range(0, 10000)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [Display(Name = "Last Date")]
        [DataType(DataType.Date)]
        public DateTime AsOf { get; set; }
        public string Store { get; set; }
        public int UserID { get; set; }

    }
}