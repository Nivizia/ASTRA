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
        private readonly IRingRepository _ringRepo;
        private readonly IRingTypeRepository _ringTypeRepo;
        private readonly IRingSubtypeRepository _ringSubtypeRepo;
        private readonly IMetalTypeRepository _metalTypeRepo;
        private readonly IFrameTypeRepository _frameTypeRepo;
        public RingController(IRingRepository ringRepo, IRingTypeRepository ringTypeRepo, IRingSubtypeRepository ringSubtypeRepo, IMetalTypeRepository metalTypeRepo, IFrameTypeRepository frameTypeRepo)
        {
            _ringRepo = ringRepo;
            _ringTypeRepo = ringTypeRepo;
            _ringSubtypeRepo = ringSubtypeRepo;
            _metalTypeRepo = metalTypeRepo;
            _frameTypeRepo = frameTypeRepo;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rings = await _ringRepo.GetAllAsync();
            var ringDTOs = rings.Select(r => r.ToRingDTO());
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
            return Ok(ring.ToRingDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRingRequestDTO ringDTO)
        {
            var ringModel = ringDTO.toRingFromCreateDTO();
            ringModel.RingTypeId = _ringTypeRepo.GetRingTypeIdFromName(ringDTO.RingType).Result;
            ringModel.RingSubtypeId = _ringSubtypeRepo.GetRingSubtypeIdFromName(ringDTO.RingSubtype).Result;
            ringModel.MetalTypeId = _metalTypeRepo.GetMetalTypeIdFromName(ringDTO.MetalType).Result;
            ringModel.FrameTypeId = _frameTypeRepo.GetFrameTypeIdFromName(ringDTO.FrameType).Result;
            await _ringRepo.CreateAsync(ringModel);
            return CreatedAtAction(nameof(GetByID), new { RingId = ringModel.RingId }, ringModel.ToRingDTO());
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

            return Ok(ring.ToRingDTO());
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
