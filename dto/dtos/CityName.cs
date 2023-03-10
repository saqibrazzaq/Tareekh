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
    public class CityNameRes
    {
        public int CityNameId { get; set; }

        public int? LanguageId { get; set; }
        public LanguageRes? Language { get; set; }

        public int? CityId { get; set; }
        public CityRes? City { get; set; }

        public string? Name { get; set; }
    }

    public class CityNameReqEdit
    {
        [Required]
        public int? LanguageId { get; set; }
        
        [Required]
        public int? CityId { get; set; }
        
        [Required]
        public string? Name { get; set; }
    }

    public class CityNameReqSearch : PagedReq
    {
        [Required]
        public int? CityId { get; set; }
    }
}
