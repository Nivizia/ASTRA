using DiamondAPI.Data;
using DiamondAPI.DTOs.Earring;
using DiamondAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/Earring")]
    [ApiController]
    public class EarringController : ControllerBase
    {
        private readonly DiamondprojectContext _context;
        public EarringController(DiamondprojectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var earrings = _context.Earrings.ToList().Select(e => e.toEarringDTO());
            return Ok(earrings);
        }

        [HttpGet("{EarringId}")]
        public IActionResult GetByID([FromRoute] Guid EarringId)
        {
            var earring = _context.Earrings.Find(EarringId);
            if (earring == null)
            {
                return NotFound();
            }
            return Ok(earring.toEarringDTO());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateEarringRequestDTO earringDTO)
        {
            var earringModel = earringDTO.toEarringFromCreateDTO();
            _context.Earrings.Add(earringModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetByID), new { EarringId = earringModel.EarringId }, earringModel.toEarringDTO());
        }

        [HttpPut]
        [Route("{EarringId}")]
        public IActionResult Update([FromRoute] Guid EarringId, [FromBody] UpdateEarringRequestDTO earringDTO)
        {
            var earring = _context.Earrings.FirstOrDefault(x => x.EarringId == EarringId);
            if (earring == null)
            {
                return NotFound();
            }
            earring.Name = earringDTO.Name;
            earring.Price = earringDTO.Price;
            earring.StockQuantity = earringDTO.StockQuantity;
            earring.ImageUrl = earringDTO.ImageUrl;
            earring.MetalType = earringDTO.MetalType;
            _context.SaveChanges();
            return Ok(earring.toEarringDTO());
        }

        [HttpDelete]
        [Route("{EarringId}")]
        public IActionResult Delete([FromRoute] Guid EarringId)
        {
            var earring = _context.Earrings.Find(EarringId);
            if (earring == null)
            {
                return NotFound();
            }
            _context.Earrings.Remove(earring);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
