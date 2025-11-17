using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Diagnostics;
using System.IO;
using System.Linq;
using WebApplication4.Models;


namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _context;

        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            _env = env;
        }


        public async Task<IActionResult> Index()
        {
            var REGISTROS = await _context.PUMASTER.ToListAsync();
            return View(REGISTROS);
        }

        public IActionResult GenerarExcel(DateTime fechaInicial, DateTime fechaFinal, string familia, string mandril)
        {
            var registros = _context.PUMASTER
                .Where(r => r.FECHA >= fechaInicial && r.FECHA <= fechaFinal)
                .Where(r => r.FAMILIA == familia && r.MANDRIL == mandril)
                .ToList();

            var rutaPlantilla = Path.Combine(_env.WebRootPath, "Plantillas", "1000F-PRD-119 FORMATO PMU FAMILIA 16X24.xlsx");
            var rutaTemporal = Path.GetTempFileName();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(new FileInfo(rutaPlantilla));
            var hoja = package.Workbook.Worksheets[0];

            // Datos generales
            var r0 = registros.FirstOrDefault();
            if (r0 != null)
            {
                hoja.Cells["A3"].Value = r0.MANDRIL;
                hoja.Cells["B3"].Value = r0.NOMBRE;
                hoja.Cells["B4"].Value = r0.EXTRUDER;
                hoja.Cells["B5"].Value = r0.FECHA.ToString("dd/MM/yyyy");
            }

            // Datos por registro
            int fila = 7;
            foreach (var r in registros)
            {
                hoja.Cells[$"A{fila}"].Value = r.FECHA;
                hoja.Cells[$"B{fila}"].Value = r.EXTRUDER;
                hoja.Cells[$"C{fila}"].Value = r.LONGITUD_A;
                hoja.Cells[$"D{fila}"].Value = r.LOGO_A;
                hoja.Cells[$"E{fila}"].Value = r.PARED3_A;
                hoja.Cells[$"F{fila}"].Value = r.PARED6_A;
                hoja.Cells[$"G{fila}"].Value = r.PARED9_A;
                hoja.Cells[$"H{fila}"].Value = r.PARED12_A;
                hoja.Cells[$"I{fila}"].Value = r.ID_A;
                hoja.Cells[$"J{fila}"].Value = r.PITCH_A;
                hoja.Cells[$"M{fila}"].Value = r.LONGITUD_LEYENDA_A;
                hoja.Cells[$"N{fila}"].Value = r.GROSOR_LEYENDA_A;

                hoja.Cells[$"O{fila}"].Value = r.LONGITUD_B;
                hoja.Cells[$"P{fila}"].Value = r.LOGO_B;
                hoja.Cells[$"Q{fila}"].Value = r.PARED3_B;
                hoja.Cells[$"R{fila}"].Value = r.PARED6_B;
                hoja.Cells[$"S{fila}"].Value = r.PARED9_B;
                hoja.Cells[$"T{fila}"].Value = r.PARED12_B;
                hoja.Cells[$"U{fila}"].Value = r.ID_B;
                hoja.Cells[$"V{fila}"].Value = r.PITCH_B;
                hoja.Cells[$"Y{fila}"].Value = r.LONGITUD_LEYENDA_B;
                hoja.Cells[$"Z{fila}"].Value = r.GROSOR_LEYENDA_B;

                hoja.Cells[$"AA{fila}"].Value = r.NOMBRE;
                fila++;
            }

            package.SaveAs(new FileInfo(rutaTemporal));
            var excelBytes = System.IO.File.ReadAllBytes(rutaTemporal);
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReportePMU.xlsx");
        }

        public IActionResult Imprimir()
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
            ViewBag.FamiliaList = new List<SelectListItem>
    {
        new SelectListItem { Value = "16X24", Text = "16X24" },
    };

            ViewBag.MandrilList = new List<SelectListItem>
    {
        new SelectListItem { Value = "V5-MX", Text = "V5-MX" },
    };
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
