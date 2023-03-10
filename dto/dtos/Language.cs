using dto.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dto.dtos
{
    public class LanguageRes
    {
        public int LanguageId { get; set; }
        public string? LanguageCode { get; set; }
        public string? Name { get; set; }
    }
    public class LanguageReqEdit
    {
        [Required, MinLength(2)]
        public string? LanguageCode { get; set; }
        [Required, MinLength(2)]
        public string? Name { get; set; }
    }
    public class LanguageReqSearch : PagedReq
    {
    }
}
