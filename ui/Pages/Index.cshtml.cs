using entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using service.Services.Interfaces;

namespace ui.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICityService _cityService;

        public string? LangReq { get; set; }
        public string? CityReq { get; set; }
        public string? H1 { get; set; }
        public string? H2 { get; set; }
        public string? CityLabel { get; set; }
        public string? localTimeLabel { get; set; }
        public string? dateTodayLabel { get; set; }
        public DateTime dateToday { get; set; } = DateTime.UtcNow;
        public string? hijriDateTodayLabel { get; set; }
        public City? city { get; set; }
        public string? CityName { get; set; }
        public string? StateName { get; set; }
        public string? CountryName { get; set; }
        public IndexModel(ILogger<IndexModel> logger, 
            ICityService cityService)
        {
            _logger = logger;
            _cityService = cityService;
        }

        public void OnGet(string lang, string city)
        {
            
        }

        private void LoadCity()
        {
            //city = _cityService.GetBySlug(CityReq);
        }

        private void UpdateCulture()
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(LangReq);
            ViewData["Lang"] = LangReq;
            //ViewData["Dir"] = Constants.GetLangDirection(LangReq);
        }

        private void InitializeHeadings()
        {
            H1 = string.Format(Resources.Strings.h1, CityName);
            H2 = string.Format(Resources.Strings.h2, CityName);
            CityLabel = Resources.Strings.cityLabel;
            localTimeLabel = Resources.Strings.localTimeLabel;
            dateTodayLabel = Resources.Strings.dateTodayLabel;
            hijriDateTodayLabel = Resources.Strings.hijriDateTodayLabel;
        }
    }
}