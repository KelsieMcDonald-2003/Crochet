using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Crochet.Models
{
    public class PatternModel
    {

        [Key]
        public int PatternId { get; set; }
        public string PatternTitle { get; set; }
        public AppUserModel User { get; set; }
        public string LevelId { get; set; }
        [ValidateNever]
        public LevelModel Level { get; set; } = null!;
        public string HookId { get; set; }
        [ValidateNever]
        public HookModel Hook { get; set; } = null!;
        public string PatternDescription { get; set; }
        public string Pattern { get; set; }
        public string StitchesUsed { get; set; }
        public DateTime Date { get; set; }
        public string YarnColor { get; set; }
        public string YarnBrand { get; set; }
        public string YarnWeight { get; set; }
        public int EstimateofHours { get; set; }
        public string PatternSize { get; set; }

        [ValidateNever]
        public ICollection<CommentModel> Comments { get; set; }
    }
}