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

    public async Task<IActionResult> Crear(int? id)
    {
        CargarListas(); // siempre carga las listas

        if (id != null)
        {
            var registro = await _context.PUMASTER.FindAsync(id);
            if (registro != null)
            {
                return View(registro);
            }
        }

        return View(new PUMASTER());
    }

    private void CargarListas()
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

        var familias = _context.TOLERANCES
                               .Where(t => t.FAMILIA != null)
                               .Select(t => t.FAMILIA!)
                               .Distinct()
                               .OrderBy(f => f)
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
    public IActionResult GetEmpleadoNombre(int id)
    {
        var empleado = _context.USERS.FirstOrDefault(e => e.ID == id);
        if (empleado != null)
        {
            return Json(new { nombre = empleado.NOMBRE });
        }
        return Json(new { nombre = "" });
    }

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Crear(PUMASTER model)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            model.FECHA = DateTime.Today;
    //            model.HORA = DateTime.Now.TimeOfDay;

    //            _context.PUMASTER.Add(model);
    //            await _context.SaveChangesAsync();

    //            TempData["Mensaje"] = "✅ Registro guardado correctamente.";
    //            TempData["TipoMensaje"] = "success";
    //            return RedirectToAction("Crear");
    //        }
    //        catch (Exception ex)
    //        {
    //            TempData["Mensaje"] = "❌ Error al guardar el registro: " + ex.Message;
    //            TempData["TipoMensaje"] = "danger";
    //            return RedirectToAction("Crear");
    //        }
    //    }

    //    TempData["Mensaje"] = "⚠️ Verifica los campos del formulario.";
    //    TempData["TipoMensaje"] = "warning";
    //    return View(model);
    //}

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Generar(PUMASTER model)
    {
        if (!string.IsNullOrEmpty(model.NRO_EMPLEADO) &&
            !string.IsNullOrEmpty(model.NOMBRE) &&
            !string.IsNullOrEmpty(model.EXTRUDER) &&
            !string.IsNullOrEmpty(model.MANDRIL) &&
            !string.IsNullOrEmpty(model.FAMILIA))
        {
            try
            {
                model.FECHA = DateTime.Today;
                model.HORA = DateTime.Now.TimeOfDay;

                // Asignar turno automáticamente según la hora actual
                var horaActual = DateTime.Now.TimeOfDay;

                // Turno 1: 07:00 - 15:30
                if (horaActual >= new TimeSpan(7, 0, 0) && horaActual <= new TimeSpan(15, 30, 0))
                {
                    model.TURNO = "1";
                }
                // Turno 2: 15:31 - 23:59
                else if (horaActual > new TimeSpan(15, 30, 0) && horaActual <= new TimeSpan(23, 59, 59))
                {
                    model.TURNO = "2";
                }
                // Turno 3: 00:00 - 06:59
                else
                {
                    model.TURNO = "3";
                }

                model.STATUS = "PARCIAL";

                _context.PUMASTER.Add(model);
                await _context.SaveChangesAsync();

                TempData["Mensaje"] = "✅ Registro generado correctamente como PARCIAL.";
                TempData["TipoMensaje"] = "success";

                return RedirectToAction("Crear", new { id = model.ID });
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "❌ Error al generar el registro: " + ex.Message;
                TempData["TipoMensaje"] = "danger";
                return RedirectToAction("Crear");
            }
        }

        TempData["Mensaje"] = "⚠️ Debes completar todos los campos generales en el modal.";
        TempData["TipoMensaje"] = "warning";
        CargarListas();
        return View("Crear", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Guardar(PUMASTER model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var registro = await _context.PUMASTER.FindAsync(model.ID);
                if (registro != null)
                {
                    // Actualizar campos de primera pieza
                    registro.ID_A = model.ID_A;
                    registro.LONGITUD_A = model.LONGITUD_A;
                    registro.PARED12_A = model.PARED12_A;
                    registro.PARED3_A = model.PARED3_A;
                    registro.PARED6_A = model.PARED6_A;
                    registro.PARED9_A = model.PARED9_A;
                    registro.PITCH_A = model.PITCH_A;
                    registro.PARED_INTERNA_A = model.PARED_INTERNA_A;
                    registro.PARED_EXTERNA_A = model.PARED_EXTERNA_A;
                    registro.LONGITUD_LEYENDA_A = model.LONGITUD_LEYENDA_A;
                    registro.GROSOR_LEYENDA_A = model.GROSOR_LEYENDA_A;
                    registro.PESO_A = model.PESO_A;
                    registro.LOGO_A = model.LOGO_A;

                    // Actualizar campos de última pieza
                    registro.ID_B = model.ID_B;
                    registro.LONGITUD_B = model.LONGITUD_B;
                    registro.PARED12_B = model.PARED12_B;
                    registro.PARED3_B = model.PARED3_B;
                    registro.PARED6_B = model.PARED6_B;
                    registro.PARED9_B = model.PARED9_B;
                    registro.PITCH_B = model.PITCH_B;
                    registro.PARED_INTERNA_B = model.PARED_INTERNA_B;
                    registro.PARED_EXTERNA_B = model.PARED_EXTERNA_B;
                    registro.LONGITUD_LEYENDA_B = model.LONGITUD_LEYENDA_B;
                    registro.GROSOR_LEYENDA_B = model.GROSOR_LEYENDA_B;
                    registro.PESO_B = model.PESO_B;
                    registro.LOGO_B = model.LOGO_B;

                    // Comentarios y estatus
                    registro.COMENTARIOS = model.COMENTARIOS;
                    registro.STATUS = "TERMINADO";

                    _context.Update(registro);
                    await _context.SaveChangesAsync();

                    TempData["Mensaje"] = "✅ Registro actualizado correctamente como TERMINADO.";
                    TempData["TipoMensaje"] = "success";
                }
                else
                {
                    TempData["Mensaje"] = "⚠️ No se encontró el registro a actualizar.";
                    TempData["TipoMensaje"] = "warning";
                }

                return RedirectToAction("Crear");
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "❌ Error al guardar el registro: " + ex.Message;
                TempData["TipoMensaje"] = "danger";
                return RedirectToAction("Crear");
            }
        }

        TempData["Mensaje"] = "⚠️ Verifica los campos del formulario antes de guardar.";
        TempData["TipoMensaje"] = "warning";
        return View("Crear", model);
    }
}