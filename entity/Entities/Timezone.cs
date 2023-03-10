using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entity.Entities
{
    [Table("Timezone")]
    public class Timezone
    {
        [Key]
        public int TimezoneId { get; set; }

        [Required]
        public string? TzName { get; set; }
        [Required]
        public string? CityName { get; set; }
        [Required]
        public int? GmtOffset { get; set; }
        [Required]
        public string? GmtOffsetName { get; set; }
        [Required]
        public string? Abbreviation { get; set; }
    }
}
