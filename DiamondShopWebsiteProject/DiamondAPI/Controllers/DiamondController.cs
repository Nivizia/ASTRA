using DiamondAPI.Data;
using DiamondAPI.DTOs.Diamond;
using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using DiamondAPI.Services;
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
        private readonly DiamondCalculatorService _diaCalService;

        public DiamondController(DiamondprojectContext context, IDiamondRepository diamondRepo, DiamondCalculatorService diaCalService)
        {
            _diamondRepo = diamondRepo;
            _context = context;
            _diaCalService = diaCalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var diamonds = await _diamondRepo.GetAllAsync();
            var diamondsDTO = diamonds.Select(d => d.ToDiamondDTO());
            return Ok(diamondsDTO);
        }

        // GET api/diamond/price?carat=1.5&cut=Excellent&color=D&clarity=VVS1
        [HttpGet("GetPrice")]
        public ActionResult<decimal> GetDiamondPrice(double carat, string cut, string color, string clarity)
        {
            try
            {
                // Assuming CalculateDiamondPrice is a method that calculates the diamond's price
                // based on carat, cut, color, and clarity.
                decimal price = _diaCalService.CalculateDiamondPrice(carat, cut, color, clarity);

                return Ok(price);
            }
            catch (Exception ex)
            {
                // Log the exception details
                // Return an appropriate error response
                return StatusCode(500, "An error occurred while calculating the diamond price. " + ex.Message);
            }
        }

        [HttpGet]
        [Route("Filter")]
        public async Task<IActionResult> Filter([FromQuery] FilterDiamondRequestDTO filterDiamondDTO)
        {
            var modelFilter = filterDiamondDTO.ToModelFilterFromModelFliterDiamondRequestDTO();
            var diamonds = await _diamondRepo.FilterAsync(modelFilter.ShapeName, modelFilter.LowerPrice, modelFilter.UpperPrice, modelFilter.LowerCaratWeight, modelFilter.UpperCaratWeight, modelFilter.LowerColor, modelFilter.UpperColor, modelFilter.LowerClarity, modelFilter.UpperClarity, modelFilter.LowerCut, modelFilter.UpperCut);
            var diamondsDTO = diamonds.Select(d => d.ToDiamondDTO());
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
            return Ok(diamond.ToDiamondDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDiamondRequestDTO diamondDTO)
        {
            var diamondModel = diamondDTO.ToDiamondFromCreateDTO();
            diamondModel.Price = _diaCalService.CalculateDiamondPrice(diamondDTO.CaratWeight, diamondDTO.Cut, diamondDTO.Color, diamondDTO.Clarity);
            await _diamondRepo.CreateAsync(diamondModel);
            return CreatedAtAction(nameof(GetByID), new { D_ProductID = diamondModel.DProductId }, diamondModel.ToDiamondDTO());
        }

        [HttpPut]
        [Route("{D_ProductID}")]
        public async Task<IActionResult> Update([FromRoute] Guid D_ProductID, [FromBody] UpdateDiamondRequestDTO diamondDTO)
        {
            var modelUpdateDiamond = diamondDTO.ToModelUpdateFromUpdateRequestDTO();
            var diamond = await _diamondRepo.UpdateAsync(D_ProductID, modelUpdateDiamond);
            if (diamond == null)
            {
                return NotFound();
            }

            return Ok(diamond.ToDiamondDTO());
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
