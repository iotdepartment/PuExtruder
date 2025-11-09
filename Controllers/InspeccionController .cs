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