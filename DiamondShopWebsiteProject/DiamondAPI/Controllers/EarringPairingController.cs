using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/EarringPairing")]
    [ApiController]
    public class EarringPairingController : ControllerBase
    {
        private readonly IEarringPairingRepository _earringPairingRepo;

        public EarringPairingController(IEarringPairingRepository earringPairingRepo)
        {
            _earringPairingRepo = earringPairingRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetEarringPairings()
        {
            var earringPairings = await _earringPairingRepo.GetAllAsync();
            var earringPairingsDTO = earringPairings.Select(earringPairing => earringPairing.ToEarringPairingDTO()).ToList();
            return Ok(earringPairings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEarringPairing(Guid id)
        {
            var earringPairing = await _earringPairingRepo.GetByIDAsync(id);

            if (earringPairing == null)
                return NotFound();

            return Ok(earringPairing.ToEarringPairingDTO());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEarringPairing(Guid id)
        {
            var earringPairing = await _earringPairingRepo.DeleteAsync(id);

            if (earringPairing == null)
                return NotFound();

            return Ok(earringPairing.ToEarringPairingDTO());
        }
    }
}
