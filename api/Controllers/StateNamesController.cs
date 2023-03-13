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
            var res = _stateNameService.Search(dto);
            return Ok(res);
        }

        [HttpGet("{stateNameId}")]
        public IActionResult Get(int stateNameId)
        {
            var res = _stateNameService.Get(stateNameId);
            return Ok(res);
        }

        [HttpGet("count/{stateId}")]
        public IActionResult Count(int stateId)
        {
            var result = _stateNameService.Count(stateId);
            return Ok(result);
        }

        [HttpGet("anyByLanguage/{languageId}")]
        public IActionResult AnyByLanguage(int languageId)
        {
            var result = _stateNameService.AnyByLanguage(languageId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(StateNameReqEdit dto)
        {
            var res = _stateNameService.Create(dto);
            return Ok(res);
        }

        [HttpPut("{stateNameId}")]
        public IActionResult Update(int stateNameId, StateNameReqEdit dto)
        {
            var res = _stateNameService.Update(stateNameId, dto);
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
