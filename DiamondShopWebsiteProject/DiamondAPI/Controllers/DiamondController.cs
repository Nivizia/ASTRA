using DiamondAPI.Data;
using DiamondAPI.DTOs.Diamond;
using DiamondAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/Diamond")]
    [ApiController]
    public class DiamondController : ControllerBase
    {
        private readonly DiamondprojectContext _context;
        public DiamondController(DiamondprojectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var diamonds = _context.Diamonds.ToList().Select(s => s.toDiamondDTO());
            return Ok(diamonds);
        }

        [HttpGet("{D_ProductID}")]
        public IActionResult GetByID([FromRoute] string D_ProductID)
        {
            var diamond = _context.Diamonds.Find(D_ProductID);
            if (diamond == null)
            {
                return NotFound();
            }
            return Ok(diamond.toDiamondDTO());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDiamondRequestDTO diamondDTO)
        {
            var diamondModel = diamondDTO.toDiamondFromCreateDTO();
            _context.Diamonds.Add(diamondModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetByID), new { D_ProductID = diamondModel.DProductId }, diamondModel.toDiamondDTO());
        }

        [HttpPut]
        [Route("{D_ProductID}")]
        public IActionResult Update([FromRoute] int D_ProductID, [FromBody] UpdateDiamondRequestDTO diamondDTO)
        {
            var diamond = _context.Diamonds.FirstOrDefault(x => x.DProductId == D_ProductID);
            if (diamond == null)
            {
                return NotFound();
            }
            diamond.Name = diamondDTO.Name;
            diamond.Price = diamondDTO.Price;
            diamond.ImageUrl = diamondDTO.ImageUrl;
            diamond.DType = diamondDTO.DType;
            diamond.CaratWeight = diamondDTO.CaratWeight;
            diamond.Color = diamondDTO.Color;
            diamond.Clarity = diamondDTO.Clarity;
            diamond.Cut = diamondDTO.Cut;

            _context.SaveChanges();
            return Ok(diamond.toDiamondDTO());
        }

        [HttpDelete]
        [Route("{D_ProductID}")]
        public IActionResult Delete([FromRoute] int D_ProductID)
        {
            var diamond = _context.Diamonds.FirstOrDefault(x => x.DProductId == D_ProductID);
            if (diamond == null)
            {
                return NotFound();
            }
            _context.Diamonds.Remove(diamond);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
