﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bulky.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Display(Name ="List Price")]
        [Range(0,1000)]
        public double ListPrice { get; set; }

        [Display(Name = "Price for 1-50")]
        [Range(0, 1000)]
        public double Price { get; set; }

        [Display(Name = "Price for 50+")]
        [Range(0, 1000)]
        public double Price50 { get; set; }
        [Display(Name = "Price for 100+")]
        [Range(0, 1000)]
        public double Price100 { get; set; }

       
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        
        [ValidateNever]
        public Category Category { get; set; }
       
        [ValidateNever]
        public string ImageUrl { get; set; }

	}
}
