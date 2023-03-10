using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace entity.Entities
{
    [Table("Country")]
    [Index(nameof(Slug), IsUnique = true)]
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [Required]
        public string? Slug { get; set; }


        // Child tables
        public ICollection<State>? States { get; set; }
        public IEnumerable<CountryName>? CountryNames { get; set; }
    }
}