using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication4.Models;
using Microsoft.EntityFrameworkCore;

public class InspeccionController : Controller
{
    private readonly AppDbContext _context;

    public InspeccionController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var inspecciones = await _context.Inspecciones.ToListAsync();
        return View(inspecciones);
    }

    public IActionResult Crear()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(PU model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // ✅ Asignar valores automáticos
                model.Fecha = DateTime.Today;               // Fecha actual sin hora
                model.Hora = DateTime.Now.TimeOfDay;        // Hora actual

                // Guardar en la base de datos
                _context.Inspecciones.Add(model);
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
        return View(model); // ✅ Aquí sí se regresa la vista con el modelo para mostrar errores
    }

}