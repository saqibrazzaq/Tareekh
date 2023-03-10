using dto.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dto.dtos
{
    public class TimezoneRes
    {
        public int TimezoneId { get; set; }

        public string? TzName { get; set; }
        public string? CityName { get; set; }
        public int? GmtOffset { get; set; }
        public string? GmtOffsetName { get; set; }
        public string? Abbreviation { get; set; }
    }
    public class TimezoneReqEdit
    {
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
    public class TimezoneReqSearch : PagedReq
    {
    }
}
