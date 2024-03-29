using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Crochet.Models
{
    public class RecommendationModel
    {
        [Key]
        public int RecommendId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Author { get; set; }

        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime PubDate { get; set; }

        [Range(1, 5)]
        public int? Rating { get; set; }
    }
}