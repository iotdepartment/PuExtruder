using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class TOLERANCESController : Controller
    {
        private readonly AppDbContext _context;

        public TOLERANCESController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var registros = await _context.TOLERANCES.ToListAsync();
            return View(registros);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TOLERANCES model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            _context.TOLERANCES.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public IActionResult Detalle(int id)
        {
            var item = _context.TOLERANCES.FirstOrDefault(x => x.ID == id);

            if (item == null)
                return NotFound();

            return PartialView("_DetalleTolerances", item);
        }
    }
}