using System.ComponentModel.DataAnnotations;

namespace RecordStore.Core.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
    }
}
