using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dto.Paging;

namespace dto.dtos
{
    public class StateNameRes
    {
        public int StateNameId { get; set; }

        public int? LanguageId { get; set; }
        public LanguageRes? Language { get; set; }

        public int? StateId { get; set; }
        public StateRes? State { get; set; }

        public string? Name { get; set; }
    }
    public class StateNameReqEdit
    {
        [Required]
        public int? LanguageId { get; set; }
        
        [Required]
        public int? StateId { get; set; }
        
        [Required]
        public string? Name { get; set; }
    }

    public class StateNameReqSearch : PagedReq
    {
        [Required]
        public int? StateId { get; set; }
    }
}
