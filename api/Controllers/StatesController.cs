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
    public class StatesController : ControllerBase
    {
        private readonly IStateService _stateService;
        private readonly IMapper _mapper;
        public StatesController(IStateService stateService, 
            IMapper mapper)
        {
            _stateService = stateService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Default()
        {
            return Search(new StateReqSearch());
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] StateReqSearch dto)
        {
            var result = _stateService.Search(dto);
            var dtos = _mapper.Map<IEnumerable<StateRes>>(result.PagedList);
            var res = new ApiOkPagedResponse<IEnumerable<StateRes>, MetaData>(dtos,
                result.MetaData);
            return Ok(res);
        }

        [HttpGet("{stateId}")]
        public IActionResult Get(int stateId)
        {
            var result = _stateService.Get(stateId);
            var res = _mapper.Map<StateRes>(result);
            return Ok(res);
        }

        [HttpGet("slug/{slug}")]
        public IActionResult GetBySlug(string slug)
        {
            var result = _stateService.GetBySlug(slug);
            var res = _mapper.Map<StateRes>(result);
            return Ok(res);
        }

        [HttpGet("count")]
        public IActionResult Count()
        {
            var result = _stateService.Count();
            return Ok(result);
        }

        [HttpGet("count/{countryId}")]
        public IActionResult CountByCountryId(int countryId)
        {
            var result = _stateService.Count(countryId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(StateReqEdit dto)
        {
            var entity = _mapper.Map<State>(dto);
            var result = _stateService.Create(entity);
            var res = _mapper.Map<StateRes>(result);
            return Ok(res);
        }

        [HttpPut("{stateId}")]
        public IActionResult Update(int stateId, StateReqEdit dto)
        {
            var entity = _mapper.Map<State>(dto);
            var result = _stateService.Update(stateId, entity);
            var res = _mapper.Map<StateRes>(result);
            return Ok(res);
        }

        [HttpDelete("{stateId}")]
        public IActionResult Delete(int stateId)
        {
            _stateService.Delete(stateId);
            return NoContent();
        }
    }
}
