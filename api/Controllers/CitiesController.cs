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
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        public CitiesController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Default()
        {
            return Search(new CityReqSearch());
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] CityReqSearch dto)
        {
            var result = _cityService.Search(dto);
            var dtos = _mapper.Map<IEnumerable<CityRes>>(result.PagedList);
            var res = new ApiOkPagedResponse<IEnumerable<CityRes>, MetaData>(dtos,
                result.MetaData);
            return Ok(res);
        }

        [HttpGet("{cityId}")]
        public IActionResult Get(int cityId)
        {
            var result = _cityService.Get(cityId);
            var res = _mapper.Map<CityRes>(result);
            return Ok(res);
        }

        [HttpGet("count")]
        public IActionResult Count()
        {
            var result = _cityService.Count();
            return Ok(result);
        }

        [HttpGet("countByStateId/{stateId}")]
        public IActionResult CountByStateId(int stateId)
        {
            var result = _cityService.Count(stateId);
            return Ok(result);
        }

        [HttpGet("slug/{slug}")]
        public IActionResult GetBySlug(string slug)
        {
            var result = _cityService.GetBySlug(slug);
            var res = _mapper.Map<CityRes>(result);
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Create(CityReqEdit dto)
        {
            var entity = _mapper.Map<City>(dto);
            var result = _cityService.Create(entity);
            var res = _mapper.Map<CityRes>(result);
            return Ok(res);
        }

        [HttpPut("{cityId}")]
        public IActionResult Update(int cityId, CityReqEdit dto)
        {
            var entity = _mapper.Map<City>(dto);
            var result = _cityService.Update(cityId, entity);
            var res = _mapper.Map<CityRes>(result);
            return Ok(res);
        }

        [HttpDelete("{cityId}")]
        public IActionResult Delete(int cityId)
        {
            _cityService.Delete(cityId);
            return NoContent();
        }
    }
}
