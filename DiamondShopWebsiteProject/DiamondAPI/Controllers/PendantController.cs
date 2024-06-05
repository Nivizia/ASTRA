using DiamondAPI.Data;
using DiamondAPI.DTOs.Pendant;
using DiamondAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/Pendant")]
    [ApiController]
    public class PendantController : ControllerBase
    {
        private readonly DiamondprojectContext _context;
        public PendantController(DiamondprojectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var pendants = _context.Pendants.ToList().Select(p => p.ToPendantDTO());
            return Ok(pendants);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var pendant = _context.Pendants.FirstOrDefault(p => p.PendantId == id);
            if (pendant == null)
            {
                return NotFound();
            }
            return Ok(pendant.ToPendantDTO());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePendantRequestDTO createPendantRequestDTO)
        {
            var pendant = createPendantRequestDTO.toPendantFromCreateDTO();
            _context.Pendants.Add(pendant);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = pendant.PendantId }, pendant);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] UpdatePendantRequestDTO updatePendantRequestDTO)
        {
            var pendant = _context.Pendants.FirstOrDefault(p => p.PendantId == id);
            if (pendant == null)
            {
                return NotFound();
            }
            pendant.Name = updatePendantRequestDTO.Name;
            pendant.Price = updatePendantRequestDTO.Price;
            pendant.StockQuantity = updatePendantRequestDTO.StockQuantity;
            pendant.ImageUrl = updatePendantRequestDTO.ImageUrl;
            pendant.MetalType = updatePendantRequestDTO.MetalType;
            _context.SaveChanges();
            return Ok(pendant.ToPendantDTO());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var pendant = _context.Pendants.FirstOrDefault(p => p.PendantId == id);
            if (pendant == null)
            {
                return NotFound();
            }
            _context.Pendants.Remove(pendant);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
