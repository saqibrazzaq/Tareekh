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
            var res = _stateService.Search(dto);
            return Ok(res);
        }

        [HttpGet("{stateId}")]
        public IActionResult Get(int stateId)
        {
            var res = _stateService.Get(stateId);
            return Ok(res);
        }

        [HttpGet("slug/{slug}")]
        public IActionResult GetBySlug(string slug)
        {
            var res = _stateService.GetBySlug(slug);
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
            var res = _stateService.Create(dto);
            return Ok(res);
        }

        [HttpPut("{stateId}")]
        public IActionResult Update(int stateId, StateReqEdit dto)
        {
            var res = _stateService.Update(stateId, dto);
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
