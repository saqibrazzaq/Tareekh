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
    public class TimezonesController : ControllerBase
    {
        private readonly ITimezoneService _timezoneService;
        private readonly IMapper _mapper;
        public TimezonesController(ITimezoneService timezoneService, 
            IMapper mapper)
        {
            _timezoneService = timezoneService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Default()
        {
            return Search(new TimezoneReqSearch());
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] TimezoneReqSearch dto)
        {
            var result = _timezoneService.Search(dto);
            var dtos = _mapper.Map<IEnumerable<TimezoneRes>>(result.PagedList);
            var res = new ApiOkPagedResponse<IEnumerable<TimezoneRes>, MetaData>(dtos,
                result.MetaData);
            return Ok(res);
        }

        [HttpGet("{timezoneId}")]
        public IActionResult Get(int timezoneId)
        {
            var result = _timezoneService.Get(timezoneId);
            var res = _mapper.Map<TimezoneRes>(result);
            return Ok(res);
        }

        [HttpGet("count")]
        public IActionResult Count()
        {
            var result = _timezoneService.Count();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(TimezoneReqEdit dto)
        {
            var entity = _mapper.Map<Timezone>(dto);
            var result = _timezoneService.Create(entity);
            var res = _mapper.Map<TimezoneRes>(result);
            return Ok(res);
        }

        [HttpPut("{timezoneId}")]
        public IActionResult Update(int timezoneId, TimezoneReqEdit dto)
        {
            var entity = _mapper.Map<Timezone>(dto);
            var result = _timezoneService.Update(timezoneId, entity);
            var res = _mapper.Map<TimezoneRes>(result);
            return Ok(res);
        }

        [HttpDelete("{timezoneId}")]
        public IActionResult Delete(int timezoneId)
        {
            _timezoneService.Delete(timezoneId);
            return NoContent();
        }
    }
}
