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
    public class CountryNameRes
    {
        public int CountryNameId { get; set; }

        public int? LanguageId { get; set; }
        public LanguageRes? Language { get; set; }

        public int? CountryId { get; set; }
        public CountryRes? Country { get; set; }

        public string? Name { get; set; }
    }
    public class CountryNameReqEdit
    {
        [Required]
        public int? LanguageId { get; set; }
        
        [Required]
        public int? CountryId { get; set; }
        
        [Required]
        public string? Name { get; set; }
    }

    public class CountryNameReqSearch : PagedReq
    {
        [Required]
        public int? CountryId { get; set; }
    }

}
