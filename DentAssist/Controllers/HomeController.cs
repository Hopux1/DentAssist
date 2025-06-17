using System.Diagnostics;
using DentAssist.Data;
using DentAssist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentAssist.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new DashboardViewModel
            {
                TotalPacientes = _context.Pacientes.Count(),
                TotalOdontologos = _context.Odontologos.Count(),
                TotalTurnos = _context.Turnos.Count(),
                TotalTratamientos = _context.Tratamientos.Count(),

                TratamientosMasUsados = _context.PasosTratamiento
                    .GroupBy(p => p.Tratamiento.Nombre)
                    .Select(g => new { Nombre = g.Key, Cantidad = g.Count() })
                    .OrderByDescending(t => t.Cantidad)
                    .Take(5)
                    .ToDictionary(t => t.Nombre, t => t.Cantidad),

                UltimosPlanes = _context.PlanesTratamiento
                    .Include(p => p.Paciente)
                    .Include(p => p.Odontologo)
                    .OrderByDescending(p => p.Id)
                    .Take(5)
                    .ToList()
            };

            return View(viewModel);
        }
    }
}