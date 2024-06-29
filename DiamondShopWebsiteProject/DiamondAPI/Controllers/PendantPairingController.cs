using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [ApiController]
    [Route("DiamondAPI/Models/PendantPairing")]
    public class PendantPairingController : ControllerBase
    {
        private readonly IPendantPairingRepository _pendantPairingRepo;

        public PendantPairingController(IPendantPairingRepository pendantPairingRepo)
        {
            _pendantPairingRepo = pendantPairingRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetPendantPairings()
        {
            var pendantPairings = await _pendantPairingRepo.GetPendantPairingsAsync();
            var pendantPairingsDTO = pendantPairings.Select(pendantPairing => pendantPairing.ToPendantPairingDTO()).ToList();
            return Ok(pendantPairings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPendantPairing(Guid id)
        {
            var pendantPairing = await _pendantPairingRepo.GetPendantPairingAsync(id);

            if (pendantPairing == null)
                return NotFound();

            return Ok(pendantPairing.ToPendantPairingDTO());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePendantPairing(Guid id)
        {
            var pendantPairing = await _pendantPairingRepo.DeletePendantPairingAsync(id);

            if (pendantPairing == null)
                return NotFound();

            return Ok(pendantPairing.ToPendantPairingDTO());
        }
    }
}
