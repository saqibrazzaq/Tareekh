using dto.Paging;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace dto.dtos
{
    public class CityRes
    {
        public int CityId { get; set; }
        public string? Slug { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Foreign keys
        public int? StateId { get; set; }
        public StateRes? State { get; set; }

        public int? TimezoneId { get; set; }
        public TimezoneRes? Timezone { get; set; }

        // Child tables
        public IEnumerable<CityNameRes>? CityNames { get; set; }
    }
    public class CityReqEdit
    {
        [Required]
        public string? Slug { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Foreign keys
        public int? StateId { get; set; }
        
        public int? TimezoneId { get; set; }
    }
    public class CityReqSearch : PagedReq
    {
        public int? StateId { get; set; }
    }
}
