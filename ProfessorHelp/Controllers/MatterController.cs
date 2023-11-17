using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfessorHelp.Dto;
using ProfessorHelp.Interfaces;
using ProfessorHelp.Models.Entity;

namespace ProfessorHelp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MatterController : ControllerBase
    {
        private readonly IMatterRepository _matterRepository;
        private readonly IMapper _mapper;

        public MatterController(IMatterRepository matterRepository, IMapper mapper)
        {
            this._matterRepository = matterRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Matter>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAll([FromQuery] int professorId)
        {
            var matters = _mapper.Map<List<MatterDto>>(await _matterRepository.GetAll(professorId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (matters.Count() <= 0)
            {
                return NotFound();
            }

            return Ok(matters);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Matter>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetMatters([FromQuery] int professorId, int id) 
        {
            var matters = _mapper.Map<List<MatterDto>>(await _matterRepository.GetMatters(id, professorId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (matters.Count() <= 0)
            {
                return NotFound();
            }

            return Ok(matters);
        }

        [HttpGet("{title}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Matter>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetMatters([FromQuery] int professorId ,string title) 
        {
            var matters = _mapper.Map<List<MatterDto>>(await _matterRepository.GetMatters(title, professorId));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(matters.Count() <= 0)
            {
                return NotFound();
            }
            
            return Ok(matters);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatter([FromQuery] int professor_id ,[FromBody] MatterDto matter)
        {
            bool newMatter = await _matterRepository.CreateMatter(matter, professor_id);

            return Ok(newMatter);
        }
    }
}
