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
            var result = _countryService.Search(dto);
            var dtos = _mapper.Map<IEnumerable<CountryRes>>(result.PagedList);
            var res = new ApiOkPagedResponse<IEnumerable<CountryRes>, MetaData>(dtos,
                result.MetaData);
            return Ok(res);
        }

        [HttpGet("{countryId}")]
        public IActionResult Get(int countryId)
        {
            var result = _countryService.Get(countryId);
            var res = _mapper.Map<CountryRes>(result);
            return Ok(res);
        }

        [HttpGet("slug/{slug}")]
        public IActionResult GetBySlug(string slug)
        {
            var result = _countryService.GetBySlug(slug);
            var res = _mapper.Map<CountryRes>(result);
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
            var entity = _mapper.Map<Country>(dto);
            var result = _countryService.Create(entity);
            var res = _mapper.Map<CountryRes>(result);
            return Ok(res);
        }

        [HttpPut("{countryId}")]
        public IActionResult Update(int countryId, CountryReqEdit dto)
        {
            var entity = _mapper.Map<Country>(dto);
            var result = _countryService.Update(countryId, entity);
            var res = _mapper.Map<CountryRes>(result);
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
