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
    [Table("City")]
    [Index(nameof(Slug), IsUnique = true)]
    public class City
    {
        [Key]
        public int CityId { get; set; }
        [Required]
        public string? Slug { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Foreign keys
        public int? StateId { get; set; }
        [ForeignKey(nameof(StateId))]
        public State? State { get; set; }

        public int? TimezoneId { get; set; }
        [ForeignKey(nameof(TimezoneId))]
        public Timezone? Timezone { get; set; }

        // Child tables
        public IEnumerable<CityName>? CityNames { get; set; }
    }
}
