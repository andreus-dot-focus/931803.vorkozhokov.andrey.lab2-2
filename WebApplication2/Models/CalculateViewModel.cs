using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class CalculateViewModel
    {
        [Required(ErrorMessage = "Number1 is required")]
        public int? Number1 { get; set; }

        public String Operation { get; set; }

        [Required(ErrorMessage = "Number2 is required")]
        public int? Number2 { get; set; }

        public int? ResultNumber { get; set; }
    }
}
