using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var registros = await _context.USERS.ToListAsync();
            return View(registros);
        }
        [HttpPost]
        public async Task<IActionResult> Create(USERS model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            _context.USERS.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}