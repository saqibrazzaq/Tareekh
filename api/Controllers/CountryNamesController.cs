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
            var res = _countryNameService.Search(dto);
            return Ok(res);
        }

        [HttpGet("{countryNameId}")]
        public IActionResult Get(int countryNameId)
        {
            var res = _countryNameService.Get(countryNameId);
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
            var res = _countryNameService.Create(dto);
            return Ok(res);
        }

        [HttpPut("{countryNameId}")]
        public IActionResult Update(int countryNameId, CountryNameReqEdit dto)
        {
            var res = _countryNameService.Update(countryNameId, dto);
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
