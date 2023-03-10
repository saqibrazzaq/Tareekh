using AutoMapper;
using dto.dtos;
using dto.Paging;
using entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using service.Services.Interfaces;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryNamesController : ControllerBase
    {
        private readonly ICountryNameService _countryNameService;
        private readonly IMapper _mapper;
        public CountryNamesController(ICountryNameService countryNameService, 
            IMapper mapper)
        {
            _countryNameService = countryNameService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Default()
        {
            return Search(new CountryNameReqSearch());
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] CountryNameReqSearch dto)
        {
            var result = _countryNameService.Search(dto);
            var dtos = _mapper.Map<IEnumerable<CountryNameRes>>(result.PagedList);
            var res = new ApiOkPagedResponse<IEnumerable<CountryNameRes>, MetaData>(dtos,
                result.MetaData);
            return Ok(res);
        }

        [HttpGet("{countryNameId}")]
        public IActionResult Get(int countryNameId)
        {
            var result = _countryNameService.Get(countryNameId);
            var res = _mapper.Map<CountryNameRes>(result);
            return Ok(res);
        }

        [HttpGet("count/{countryId}")]
        public IActionResult Count(int countryId)
        {
            var result = _countryNameService.Count(countryId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(CountryNameReqEdit dto)
        {
            var entity = _mapper.Map<CountryName>(dto);
            var result = _countryNameService.Create(entity);
            var res = _mapper.Map<CountryNameRes>(result);
            return Ok(res);
        }

        [HttpPut("{countryNameId}")]
        public IActionResult Update(int countryNameId, CountryNameReqEdit dto)
        {
            var entity = _mapper.Map<CountryName>(dto);
            var result = _countryNameService.Update(countryNameId, entity);
            var res = _mapper.Map<CountryNameRes>(result);
            return Ok(res);
        }

        [HttpDelete("{countryNameId}")]
        public IActionResult Delete(int countryId)
        {
            _countryNameService.Delete(countryId);
            return NoContent();
        }
    }
}
