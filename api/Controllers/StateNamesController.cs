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
    public class StateNamesController : ControllerBase
    {
        private readonly IStateNameService _stateNameService;
        private readonly IMapper _mapper;
        public StateNamesController(IStateNameService stateNameService, 
            IMapper mapper)
        {
            _stateNameService = stateNameService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Default()
        {
            return Search(new StateNameReqSearch());
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] StateNameReqSearch dto)
        {
            var result = _stateNameService.Search(dto);
            var dtos = _mapper.Map<IEnumerable<StateNameRes>>(result.PagedList);
            var res = new ApiOkPagedResponse<IEnumerable<StateNameRes>, MetaData>(dtos,
                result.MetaData);
            return Ok(res);
        }

        [HttpGet("{stateNameId}")]
        public IActionResult Get(int stateNameId)
        {
            var result = _stateNameService.Get(stateNameId);
            var res = _mapper.Map<StateNameRes>(result);
            return Ok(res);
        }

        [HttpGet("count/{stateId}")]
        public IActionResult Count(int stateId)
        {
            var result = _stateNameService.Count(stateId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(StateNameReqSearch dto)
        {
            var entity = _mapper.Map<StateName>(dto);
            var result = _stateNameService.Create(entity);
            var res = _mapper.Map<StateNameRes>(result);
            return Ok(res);
        }

        [HttpPut("{stateNameId}")]
        public IActionResult Update(int stateNameId, StateNameReqEdit dto)
        {
            var entity = _mapper.Map<StateName>(dto);
            var result = _stateNameService.Update(stateNameId, entity);
            var res = _mapper.Map<StateNameRes>(result);
            return Ok(res);
        }

        [HttpDelete("{stateNameId}")]
        public IActionResult Delete(int stateNameId)
        {
            _stateNameService.Delete(stateNameId);
            return NoContent();
        }
    }
}
