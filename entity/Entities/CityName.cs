using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entity.Entities
{
    [Table("CityName")]
    [Index(nameof(LanguageId), nameof(CityId), IsUnique = true)]
    public class CityName
    {
        [Key]
        public int CityNameId { get; set; }

        [Required]
        public int? LanguageId { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language? Language { get; set; }

        [Required]
        public int? CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public City? City { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
