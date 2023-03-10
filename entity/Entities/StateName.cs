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
    [Table("StateName")]
    [Index(nameof(LanguageId), nameof(StateId), IsUnique = true)]
    public class StateName
    {
        [Key]
        public int StateNameId { get; set; }

        [Required]
        public int? LanguageId { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language? Language { get; set; }

        [Required]
        public int? StateId { get; set; }
        [ForeignKey(nameof(StateId))]
        public State? State { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
