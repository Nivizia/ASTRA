using DiamondAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace DiamondAPI.Controllers
{
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
            var pendants = _context.Pendants.ToList().Select(p => p.toPendantDTO());
            return Ok(pendants);
        }
    }
}
