using System.ComponentModel.DataAnnotations;

namespace Crochet.Models
{
    public class HookModel
    {
        [Key]
        public string HookId { get; set; } = string.Empty;
        public string HookName { get; set; } = string.Empty;
        public string HookSize { get; set; } = string.Empty;
    }
}