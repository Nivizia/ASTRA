using DiamondAPI.Data;
using DiamondAPI.DTOs.Pendant;
using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/Pendant")]
    [ApiController]
    public class PendantController : ControllerBase
    {
        private readonly DiamondprojectContext _context;
        private readonly IPendantRepository _pendantRepo;
        public PendantController(DiamondprojectContext context, IPendantRepository pendantRepo)
        {
            _context = context;
            _pendantRepo = pendantRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pendants = await _pendantRepo.GetAllAsync();
            var pendantDTOs = pendants.Select(p => p.ToPendantDTO());
            return Ok(pendantDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var pendant = await _pendantRepo.GetByIDAsync(id);
            if (pendant == null)
            {
                return NotFound();
            }
            return Ok(pendant.ToPendantDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePendantRequestDTO createPendantRequestDTO)
        {
            var pendant = createPendantRequestDTO.toPendantFromCreateDTO();
            await _pendantRepo.CreateAsync(pendant);
            return CreatedAtAction(nameof(GetById), new { id = pendant.PendantId }, pendant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePendantRequestDTO updatePendantRequestDTO)
        {
            var pendant = await _pendantRepo.UpdateAsync(id, updatePendantRequestDTO);
            if (pendant == null)
            {
                return NotFound();
            }

            return Ok(pendant.ToPendantDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var pendant = await _pendantRepo.DeleteAsync(id);
            if (pendant == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
