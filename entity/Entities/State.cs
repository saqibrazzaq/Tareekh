using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace entity.Entities
{
    [Table("State")]
    [Index(nameof(Slug), IsUnique = true)]
    public class State
    {
        [Key]
        public int StateId { get; set; }
        [Required]
        public string? Slug { get; set; }

        // Foreign keys
        public int? CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country? Country { get; set; }

        // Child tables
        public ICollection<City>? Cities { get; set; }
        public IEnumerable<StateName>? StateNames { get; set; }
    }
}
