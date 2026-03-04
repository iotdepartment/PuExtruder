using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication4.Models;

public class InspeccionController : Controller
{
    private readonly AppDbContext _context;

    public InspeccionController(AppDbContext context)
    {
        _context = context; 
    }

    public async Task<IActionResult> Index()
    {
        var REGISTROS = await _context.PUMASTER.ToListAsync();
        return View(REGISTROS);
    }

    public IActionResult Crear()
    {
        ViewBag.ExtruderList = new List<SelectListItem>
    {
        new SelectListItem { Value = "Extruder 1", Text = "Extruder 1" },
        new SelectListItem { Value = "Extruder 2", Text = "Extruder 2" },
        new SelectListItem { Value = "Extruder 3", Text = "Extruder 3" },
        new SelectListItem { Value = "Extruder 4", Text = "Extruder 4" },
        new SelectListItem { Value = "Extruder 5", Text = "Extruder 5" },
        new SelectListItem { Value = "Extruder 6", Text = "Extruder 6" }
    };

        ViewBag.ShiftList = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1" },
        new SelectListItem { Value = "2", Text = "2" },
        new SelectListItem { Value = "3", Text = "3" },
    };

        ViewBag.LogoList = new List<SelectListItem>
    {
        new SelectListItem { Value = "SI", Text = "SI" },
        new SelectListItem { Value = "NO", Text = "NO" },
    };

        // Consulta dinámica de familias desde la tabla TOLERANCES
        var familias = _context.TOLERANCES
                               .Where(t => t.FAMILIA != null)   // Evita nulos
                               .Select(t => t.FAMILIA!)
                               .Distinct()
                               .OrderBy(f => f)                 // Opcional: orden alfabético
                               .ToList();

        ViewBag.FamiliaList = familias
            .Select(f => new SelectListItem { Value = f, Text = f })
            .ToList();

        ViewBag.MandrilList = new List<SelectListItem>
    {
        new SelectListItem { Value = "V5-MX", Text = "V5-MX" },
    };

    ViewBag.EmpleadoList = _context.USERS
    .Select(e => new SelectListItem
    {
        Value = e.ID.ToString(),
        Text = $"{e.ID} - {e.NOMBRE}"
    })
    .ToList();
        return View();


    }

    [HttpGet]
    public JsonResult GetFamiliasByExtruder(string extruder)
    {
        var familias = _context.TOLERANCES
                               .Where(t => t.EXTRUDER == extruder && t.FAMILIA != null)
                               .Select(t => t.FAMILIA!)
                               .Distinct()
                               .OrderBy(f => f)
                               .ToList();

        return Json(familias);
    }

    [HttpGet]
    public JsonResult GetMandriles(string extruder, string familia)
    {
        var mandriles = _context.TOLERANCES
                                .Where(t => t.EXTRUDER == extruder
                                         && t.FAMILIA == familia
                                         && t.MANDRIL != null)
                                .Select(t => t.MANDRIL!)
                                .Distinct()
                                .OrderBy(m => m)
                                .ToList();

        return Json(mandriles);
    }

    [HttpGet]
    public JsonResult GetParametros(string extruder, string familia, string mandril)
    {
        var parametros = _context.TOLERANCES
                                 .Where(t => t.EXTRUDER == extruder
                                          && t.FAMILIA == familia
                                          && t.MANDRIL == mandril)
                                 .Select(t => new {
                                     t.ID_,
                                     t.ID_TOL,
                                     t.LONGITUD_CORTE,
                                     t.LONGITUD_CORTE_TOL,
                                     t.PARED,
                                     t.PARED_TOL,
                                     t.PITCH,
                                     t.PITCH_TOL,
                                     t.INNER_YARN,
                                     t.INNER_YARN_TOL,
                                     t.OUTER_YARN,
                                     t.OUTER_YARN_TOL,
                                     t.LONGITUD_LEYENDA,
                                     t.LONGITUD_LEYENDA_TOL,
                                     t.GROSOR_LEYENDA,
                                     t.GROSOR_LEYENDA_TOL
                                 })
                                 .FirstOrDefault();

        return Json(parametros);
    }


    [HttpGet]
    public JsonResult ObtenerNombreEmpleado(int id)
    {
        var empleado = _context.USERS.FirstOrDefault(e => e.ID == id);
        if (empleado != null)
        {
            return Json(new { nombre = empleado.NOMBRE });
        }
        return Json(new { nombre = "" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(PUMASTER model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                model.FECHA = DateTime.Today;
                model.HORA = DateTime.Now.TimeOfDay;

                _context.PUMASTER.Add(model);
                await _context.SaveChangesAsync();

                TempData["Mensaje"] = "✅ Registro guardado correctamente.";
                TempData["TipoMensaje"] = "success";
                return RedirectToAction("Crear");
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "❌ Error al guardar el registro: " + ex.Message;
                TempData["TipoMensaje"] = "danger";
                return RedirectToAction("Crear");
            }
        }

        TempData["Mensaje"] = "⚠️ Verifica los campos del formulario.";
        TempData["TipoMensaje"] = "warning";
        return View(model);
    }
}