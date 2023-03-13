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
    public class CityNamesController : ControllerBase
    {
        private readonly ICityNameService _cityNameService;
        private readonly IMapper _mapper;
        public CityNamesController(ICityNameService cityNameService, 
            IMapper mapper)
        {
            _cityNameService = cityNameService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Default()
        {
            return Search(new CityNameReqSearch());
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] CityNameReqSearch dto)
        {
            var res = _cityNameService.Search(dto);
            return Ok(res);
        }

        [HttpGet("{cityNameId}")]
        public IActionResult Get(int cityNameId)
        {
            var res = _cityNameService.Get(cityNameId);
            return Ok(res);
        }

        [HttpGet("count/{cityId}")]
        public IActionResult Count(int cityId)
        {
            var result = _cityNameService.Count(cityId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(CityNameReqEdit dto)
        {
            var res = _cityNameService.Create(dto);
            return Ok(res);
        }

        [HttpPut("{cityNameId}")]
        public IActionResult Update(int cityNameId, CityNameReqEdit dto)
        {
            var res = _cityNameService.Update(cityNameId, dto);
            return Ok(res);
        }

        [HttpDelete("{cityNameId}")]
        public IActionResult Delete(int cityNameId)
        {
            _cityNameService.Delete(cityNameId);
            return NoContent();
        }
    }
}
