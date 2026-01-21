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

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var entity = _context.TOLERANCES.FirstOrDefault(x => x.ID == id);

            if (entity == null)
                return NotFound();

            _context.TOLERANCES.Remove(entity);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var entity = _context.TOLERANCES.FirstOrDefault(x => x.ID == id);

            if (entity == null)
                return NotFound();

            return Json(entity);
        }

        [HttpPost]
        public IActionResult Update(TOLERANCES model)
        {
            var entity = _context.TOLERANCES.FirstOrDefault(x => x.ID == model.ID);

            if (entity == null)
                return NotFound();

            entity.EXTRUDER = model.EXTRUDER;
            entity.MANDRIL = model.MANDRIL;
            entity.FAMILIA = model.FAMILIA;

            entity.ID_ = model.ID_;
            entity.ID_TOL = model.ID_TOL;

            entity.LONGITUD_CORTE = model.LONGITUD_CORTE;
            entity.LONGITUD_CORTE_TOL = model.LONGITUD_CORTE_TOL;

            entity.PARED = model.PARED;
            entity.PARED_TOL = model.PARED_TOL;

            entity.PITCH = model.PITCH;
            entity.PITCH_TOL = model.PITCH_TOL;

            entity.INNER_YARN = model.INNER_YARN;
            entity.INNER_YARN_TOL = model.INNER_YARN_TOL;

            entity.OUTER_YARN = model.OUTER_YARN;
            entity.OUTER_YARN_TOL = model.OUTER_YARN_TOL;

            entity.LONGITUD_LEYENDA = model.LONGITUD_LEYENDA;
            entity.LONGITUD_LEYENDA_TOL = model.LONGITUD_LEYENDA_TOL;

            entity.GROSOR_LEYENDA = model.GROSOR_LEYENDA;
            entity.GROSOR_LEYENDA_TOL = model.GROSOR_LEYENDA_TOL;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}