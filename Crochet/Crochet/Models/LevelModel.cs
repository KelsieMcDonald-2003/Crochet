using System.ComponentModel.DataAnnotations;

namespace Crochet.Models
{
    public class LevelModel
    {
        [Key]
        public string LevelId { get; set; } = string.Empty;
        public string LevelName { get; set; } = string.Empty;
    }
}