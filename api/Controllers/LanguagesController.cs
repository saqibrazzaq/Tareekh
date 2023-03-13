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
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;
        public LanguagesController(ILanguageService languageService, 
            IMapper mapper)
        {
            _languageService = languageService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Default()
        {
            return Search(new LanguageReqSearch());
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] LanguageReqSearch dto)
        {
            var res = _languageService.Search(dto);
            return Ok(res);
        }

        [HttpGet("{languageId}")]
        public IActionResult Get(int languageId)
        {
            var res = _languageService.Get(languageId);
            return Ok(res);
        }

        [HttpGet("count")]
        public IActionResult Count()
        {
            var result = _languageService.Count();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(LanguageReqEdit dto)
        {
            var res = _languageService.Create(dto);
            return Ok(res);
        }

        [HttpPut("{languageId}")]
        public IActionResult Update(int languageId, LanguageReqEdit dto)
        {
            var res = _languageService.Update(languageId, dto);
            return Ok(res);
        }

        [HttpDelete("{languageId}")]
        public IActionResult Delete(int languageId)
        {
            _languageService.Delete(languageId);
            return NoContent();
        }
    }
}
