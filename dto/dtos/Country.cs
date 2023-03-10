using dto.Paging;
using System.ComponentModel.DataAnnotations;

namespace dto.dtos
{
    public class CountryRes
    {
        public int CountryId { get; set; }
        public string? Slug { get; set; }

        // Child tables
        public ICollection<StateRes>? States { get; set; }
        public IEnumerable<CountryNameRes>? CountryNames { get; set; }
    }
    public class CountryReqEdit
    {
        [Required]
        public string? Slug { get; set; }
    }
    public class CountryReqSearch : PagedReq
    {

    }
}
