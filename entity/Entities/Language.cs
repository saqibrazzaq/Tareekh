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
    [Table("Language")]
    [Index(nameof(LanguageCode), IsUnique = true)]
    public class Language
    {
        [Key]
        public int LanguageId { get; set; }
        [Required, MinLength(2)]
        public string? LanguageCode { get; set; }
        [Required, MinLength(2)]
        public string? Name { get; set; }
    }
}
