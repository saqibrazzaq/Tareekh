using AutoMapper;
using dto.dtos;
using dto.Paging;
using entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using service.Services.Interfaces;
using Sprache;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        public CountriesController(ICountryService countryService, 
            IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Default()
        {
            return Search(new CountryReqSearch());
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] CountryReqSearch dto)
        {
            var res = _countryService.Search(dto);
            return Ok(res);
        }

        [HttpGet("{countryId}")]
        public IActionResult Get(int countryId)
        {
            var res = _countryService.Get(countryId);
            return Ok(res);
        }

        [HttpGet("slug/{slug}")]
        public IActionResult GetBySlug(string slug)
        {
            var res = _countryService.GetBySlug(slug);
            return Ok(res);
        }

        [HttpGet("count")]
        public IActionResult Count()
        {
            var res = _countryService.Count();
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Create(CountryReqEdit dto)
        {
            var res = _countryService.Create(dto);
            return Ok(res);
        }

        [HttpPut("{countryId}")]
        public IActionResult Update(int countryId, CountryReqEdit dto)
        {
            var res = _countryService.Update(countryId, dto);
            return Ok(res);
        }

        [HttpDelete("{countryId}")]
        public IActionResult Delete(int countryId)
        {
            _countryService.Delete(countryId);
            return NoContent();
        }
    }
}
