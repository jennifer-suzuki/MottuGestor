using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MottuGestor.Application.DTOs;
using MottuGestor.Application.Interfaces;

namespace GestMottu.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MotoController : ControllerBase
{
        private readonly IMotoService _service;
        public MotoController(IMotoService service) => _service = service;

        [HttpGet]
        [ProducesResponseType(typeof(PagedResultDto<MotoDto>), 200)]
        public async Task<ActionResult<PagedResultDto<MotoDto>>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var all = await _service.ListAsync() ?? new List<MotoDto>();
            var total = all.Count;
            var paged = all.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            foreach (var moto in paged)
            {
                moto.Links.Add(new NavigationDto { Rel = "self", Href = Url.Action(nameof(GetById), new { id = moto.Id }) ?? string.Empty, Method = "GET" });
                moto.Links.Add(new NavigationDto { Rel = "update", Href = Url.Action(nameof(Put), new { id = moto.Id }) ?? string.Empty, Method = "PUT" });
                moto.Links.Add(new NavigationDto { Rel = "delete", Href = Url.Action(nameof(Delete), new { id = moto.Id }) ?? string.Empty, Method = "DELETE" });
            }

            return Ok(new PagedResultDto<MotoDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Items = paged
            });
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MotoDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<MotoDto>> GetById(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            result.Links.Add(new NavigationDto { Rel = "self", Href = Url.Action(nameof(GetById), new { id = result.Id }) ?? string.Empty, Method = "GET" });
            result.Links.Add(new NavigationDto { Rel = "update", Href = Url.Action(nameof(Put), new { id = result.Id }) ?? string.Empty, Method = "PUT" });
            result.Links.Add(new NavigationDto { Rel = "delete", Href = Url.Action(nameof(Delete), new { id = result.Id }) ?? string.Empty, Method = "DELETE" });
            return Ok(result);
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(MotoDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<MotoDto>> Post([FromBody] MotoDto dto)
        {
            if (dto == null) return BadRequest("Dados obrigatórios não enviados.");
            var created = await _service.CreateAsync(dto);
            created.Links.Add(new NavigationDto { Rel = "self", Href = Url.Action(nameof(GetById), new { id = created.Id }) ?? string.Empty, Method = "GET" });
            created.Links.Add(new NavigationDto { Rel = "update", Href = Url.Action(nameof(Put), new { id = created.Id }) ?? string.Empty, Method = "PUT" });
            created.Links.Add(new NavigationDto { Rel = "delete", Href = Url.Action(nameof(Delete), new { id = created.Id }) ?? string.Empty, Method = "DELETE" });
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put(string id, [FromBody] MotoDto dto)
        {
            if (dto == null) return BadRequest("Dados obrigatórios não enviados.");
            var ok = await _service.UpdateAsync(id, dto);
            return ok ? NoContent() : NotFound();
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
}