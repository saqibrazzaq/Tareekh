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
    [Table("CountryName")]
    [Index(nameof(LanguageId), nameof(CountryId), IsUnique = true)]
    public class CountryName
    {
        [Key]
        public int CountryNameId { get; set; }

        [Required]
        public int? LanguageId { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language? Language { get; set; }

        [Required]
        public int? CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country? Country { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
