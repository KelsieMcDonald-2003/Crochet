using System.ComponentModel.DataAnnotations;

namespace Crochet.Models
{
    public class CommentModel
    {
        [Key]
        public int CommentId { get; set; }
        public AppUserModel User { get; set; }

        [StringLength(500, MinimumLength = 10)]
        [Required]
        public string Comment { get; set; }
        public int PatternId { get; set; }
        public PatternModel Pattern { get; set; }
    }
}