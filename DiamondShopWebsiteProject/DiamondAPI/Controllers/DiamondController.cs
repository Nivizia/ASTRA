using DiamondAPI.Data;
using DiamondAPI.DTOs.Diamond;
using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/Diamond")]
    [ApiController]
    public class DiamondController : ControllerBase
    {
        private readonly DiamondprojectContext _context;
        private readonly IDiamondRepository _diamondRepo;

        public DiamondController(DiamondprojectContext context, IDiamondRepository diamondRepo)
        {
            _diamondRepo = diamondRepo;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var diamonds = await _diamondRepo.GetAllAsync();
            var diamondsDTO = diamonds.Select(d => d.toDiamondDTO());
            return Ok(diamondsDTO);
        }

        [HttpGet]
        [Route("Filter")]
        public async Task<IActionResult> Filter([FromQuery] FilterDiamondRequestDTO filterDiamondDTO)
        {
            var diamonds = await _diamondRepo.FilterAsync(filterDiamondDTO.DType, filterDiamondDTO.LowerPrice, filterDiamondDTO.UpperPrice, filterDiamondDTO.LowerCaratWeight, filterDiamondDTO.UpperCaratWeight, filterDiamondDTO.LowerColor, filterDiamondDTO.UpperColor, filterDiamondDTO.LowerClarity, filterDiamondDTO.UpperClarity, filterDiamondDTO.LowerCut, filterDiamondDTO.UpperCut);
            var diamondsDTO = diamonds.Select(d => d.toDiamondDTO());
            return Ok(diamondsDTO);
        }

        [HttpGet("{D_ProductID}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid D_ProductID)
        {
            var diamond = await _diamondRepo.GetByIDAsync(D_ProductID);
            if (diamond == null)
            {
                return NotFound();
            }
            return Ok(diamond.toDiamondDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDiamondRequestDTO diamondDTO)
        {
            var diamondModel = diamondDTO.toDiamondFromCreateDTO();
            await _diamondRepo.CreateAsync(diamondModel);
            return CreatedAtAction(nameof(GetByID), new { D_ProductID = diamondModel.DProductId }, diamondModel.toDiamondDTO());
        }

        [HttpPut]
        [Route("{D_ProductID}")]
        public async Task<IActionResult> Update([FromRoute] Guid D_ProductID, [FromBody] UpdateDiamondRequestDTO diamondDTO)
        {
            var diamond = await _diamondRepo.UpdateAsync(D_ProductID, diamondDTO);
            if (diamond == null)
            {
                return NotFound();
            }

            return Ok(diamond.toDiamondDTO());
        }

        [HttpDelete]
        [Route("{D_ProductID}")]
        public async Task<IActionResult> Delete([FromRoute] Guid D_ProductID)
        {
            var diamond = await _diamondRepo.DeleteAsync(D_ProductID);
            if (diamond == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
