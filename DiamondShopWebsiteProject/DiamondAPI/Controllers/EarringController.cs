using DiamondAPI.Data;
using DiamondAPI.DTOs.Earring;
using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/Earring")]
    [ApiController]
    public class EarringController : ControllerBase
    {
        private readonly DiamondprojectContext _context;
        private readonly IEarringRepository _earringRepo;
        public EarringController(DiamondprojectContext context, IEarringRepository earringRepo)
        {
            _context = context;
            _earringRepo = earringRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var earrings = await _earringRepo.GetAllAsync();
            var earringDTOs = earrings.Select(e => e.toEarringDTO());
            return Ok(earringDTOs);
        }

        [HttpGet("{EarringId}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid EarringId)
        {
            var earring = await _earringRepo.GetByIDAsync(EarringId);
            if (earring == null)
            {
                return NotFound();
            }
            return Ok(earring.toEarringDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEarringRequestDTO earringDTO)
        {
            var earringModel = earringDTO.toEarringFromCreateDTO();
            await _earringRepo.CreateAsync(earringModel);
            return CreatedAtAction(nameof(GetByID), new { EarringId = earringModel.EarringId }, earringModel.toEarringDTO());
        }

        [HttpPut]
        [Route("{EarringId}")]
        public async Task<IActionResult> Update([FromRoute] Guid EarringId, [FromBody] UpdateEarringRequestDTO earringDTO)
        {
            var earring = await _earringRepo.UpdateAsync(EarringId, earringDTO);
            if (earring == null)
            {
                return NotFound();
            }

            return Ok(earring.toEarringDTO());
        }

        [HttpDelete]
        [Route("{EarringId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid EarringId)
        {
            var earring = await _earringRepo.DeleteAsync(EarringId);
            if (earring == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
