using dto.Paging;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace dto.dtos
{
    public class StateRes
    {
        public int StateId { get; set; }
        public string? Slug { get; set; }

        // Foreign keys
        public int? CountryId { get; set; }
        public CountryRes? Country { get; set; }

        // Child tables
        public ICollection<CityRes>? Cities { get; set; }
        public IEnumerable<StateNameRes>? StateNames { get; set; }
    }
    public class StateReqEdit
    {
        [Required]
        public string? Slug { get; set; }

        // Foreign keys
        public int? CountryId { get; set; }
    }
    public class StateReqSearch : PagedReq
    {
        public int? CountryId { get; set; }
    }
}
