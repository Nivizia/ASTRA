using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [ApiController]
    [Route("DiamondAPI/Models/RingPairing")]
    public class RingPairingController : ControllerBase
    {
        private readonly IRingPairingRepository _ringPairingRepository;

        public RingPairingController(IRingPairingRepository ringPairingRepository)
        {
            _ringPairingRepository = ringPairingRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetRingPairings()
        {
            var ringPairings = await _ringPairingRepository.GetAll();
            var ringPairingsDTO = ringPairings.Select(r => r.ToRingPairingDTO());
            return Ok(ringPairingsDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetRingPairing(Guid id)
        {
            var ringPairing = await _ringPairingRepository.GetById(id);

            if (ringPairing == null)
                return NotFound();

            return Ok(ringPairing.ToRingPairingDTO());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteRingPairing(Guid id)
        {
            var ringPairing = await _ringPairingRepository.Delete(id);

            if (ringPairing == null)
                return NotFound();

            return Ok(ringPairing.ToRingPairingDTO());
        }
    }
}
