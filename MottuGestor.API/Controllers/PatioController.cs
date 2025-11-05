using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MottuGestor.Application.DTOs;
using MottuGestor.Application.Interfaces;

namespace GestMottu.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PatioController : ControllerBase
{
        private readonly IPatioService _service;
        public PatioController(IPatioService service) => _service = service;
        
        /// <summary>
        /// Lista todos os pátios
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResultDto<PatioDto>), 200)]
        public async Task<ActionResult<PagedResultDto<PatioDto>>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var all = await _service.ListAsync() ?? new List<PatioDto>();
            var total = all.Count;
            var paged = all.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            foreach (var patio in paged)
            {
                patio.Links.Add(new NavigationDto { Rel = "self", Href = Url.Action(nameof(GetById), new { id = patio.Id }) ?? string.Empty, Method = "GET" });
                patio.Links.Add(new NavigationDto { Rel = "update", Href = Url.Action(nameof(Put), new { id = patio.Id }) ?? string.Empty, Method = "PUT" });
                patio.Links.Add(new NavigationDto { Rel = "delete", Href = Url.Action(nameof(Delete), new { id = patio.Id }) ?? string.Empty, Method = "DELETE" });
            }

            return Ok(new PagedResultDto<PatioDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Items = paged
            });
        }

        /// <summary>
        /// Busca um pátio por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PatioDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PatioDto>> GetById(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result is null) return NotFound();

            result.Links.Add(new NavigationDto { Rel = "self", Href = Url.Action(nameof(GetById), new { id = result.Id }) ?? string.Empty, Method = "GET" });
            result.Links.Add(new NavigationDto { Rel = "update", Href = Url.Action(nameof(Put), new { id = result.Id }) ?? string.Empty, Method = "PUT" });
            result.Links.Add(new NavigationDto { Rel = "delete", Href = Url.Action(nameof(Delete), new { id = result.Id }) ?? string.Empty, Method = "DELETE" });

            return Ok(result);
        }

        /// <summary>
        /// Cadastra um pátio
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(PatioDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PatioDto>> Post([FromBody] PatioDto dto)
        {
            if (!ModelState.IsValid || dto is null)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);

            created.Links.Add(new NavigationDto { Rel = "self", Href = Url.Action(nameof(GetById), new { id = created.Id }) ?? string.Empty, Method = "GET" });
            created.Links.Add(new NavigationDto { Rel = "update", Href = Url.Action(nameof(Put), new { id = created.Id }) ?? string.Empty, Method = "PUT" });
            created.Links.Add(new NavigationDto { Rel = "delete", Href = Url.Action(nameof(Delete), new { id = created.Id }) ?? string.Empty, Method = "DELETE" });

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Atualiza um pátio
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put(string id, [FromBody] PatioDto dto)
        {
            if (!ModelState.IsValid || dto is null)
                return BadRequest(ModelState);

            var ok = await _service.UpdateAsync(id, dto);
            return ok ? NoContent() : NotFound();
        }
        
        /// <summary>
        /// Apaga um pátio
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
}