using DiamondAPI.Data;
using DiamondAPI.DTOs.Ring;
using DiamondAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/Ring")]
    [ApiController]
    public class RingController : ControllerBase
    {
        private readonly DiamondprojectContext _context;
        public RingController(DiamondprojectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rings = _context.Rings.ToList().Select(r => r.toRingDTO());
            return Ok(rings);
        }

        [HttpGet("{RingId}")]
        public IActionResult GetByID([FromRoute] Guid RingId)
        {
            var ring = _context.Rings.Find(RingId);
            if (ring == null)
            {
                return NotFound();
            }
            return Ok(ring.toRingDTO());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateRingRequestDTO ringDTO)
        {
            var ringModel = ringDTO.toRingFromCreateDTO();
            _context.Rings.Add(ringModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetByID), new { RingId = ringModel.RingId }, ringModel.toRingDTO());
        }

        [HttpPut]
        [Route("{RingId}")]
        public IActionResult Update([FromRoute] Guid RingId, [FromBody] UpdateRingRequestDTO ringDTO)
        {
            var ring = _context.Rings.FirstOrDefault(x => x.RingId == RingId);
            if (ring == null)
            {
                return NotFound();
            }
            ring.Name = ringDTO.Name;
            ring.Price = ringDTO.Price;
            ring.StockQuantity = ringDTO.StockQuantity;
            ring.ImageUrl = ringDTO.ImageUrl;
            ring.MetalType = ringDTO.MetalType;
            ring.RingSize = ringDTO.RingSize;
            _context.SaveChanges();
            return Ok(ring.toRingDTO());
        }

        [HttpDelete]
        [Route("{RingId}")]
        public IActionResult Delete([FromRoute] Guid RingId)
        {
            var ring = _context.Rings.Find(RingId);
            if (ring == null)
            {
                return NotFound();
            }
            _context.Rings.Remove(ring);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
