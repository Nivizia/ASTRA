using DiamondAPI.Data;
using DiamondAPI.DTOs.Ring;
using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/Ring")]
    [ApiController]
    public class RingController : ControllerBase
    {
        private readonly DiamondprojectContext _context;
        private readonly IRingRepository _ringRepo;
        public RingController(DiamondprojectContext context, IRingRepository ringRepo)
        {
            _context = context;
            _ringRepo = ringRepo;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rings = await _ringRepo.GetAllAsync();
            var ringDTOs = rings.Select(r => r.toRingDTO());
            return Ok(ringDTOs);
        }

        [HttpGet("{RingId}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid RingId)
        {
            var ring = await _ringRepo.GetByIDAsync(RingId);
            if (ring == null)
            {
                return NotFound();
            }
            return Ok(ring.toRingDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRingRequestDTO ringDTO)
        {
            var ringModel = ringDTO.toRingFromCreateDTO();
            await _ringRepo.CreateAsync(ringModel);
            return CreatedAtAction(nameof(GetByID), new { RingId = ringModel.RingId }, ringModel.toRingDTO());
        }

        [HttpPut]
        [Route("{RingId}")]
        public async Task<IActionResult> Update([FromRoute] Guid RingId, [FromBody] UpdateRingRequestDTO ringDTO)
        {
            var ring = await _ringRepo.UpdateAsync(RingId, ringDTO);
            if (ring == null)
            {
                return NotFound();
            }

            return Ok(ring.toRingDTO());
        }

        [HttpDelete]
        [Route("{RingId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid RingId)
        {
            var ring = await _ringRepo.DeleteAsync(RingId);
            if (ring == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
