using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentAssist.Data;
using DentAssist.Models;

namespace DentAssist.Controllers
{
    public class PacientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PacientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pacientes.ToListAsync());
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var paciente = await _context.Pacientes.FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null) return NotFound();

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            ViewData["Previsiones"] = new SelectList(new[] {
                "FONASA",
                "ISAPRE Banmédica",
                "ISAPRE Colmena",
                "ISAPRE Cruz Blanca",
                "ISAPRE Consalud",
                "ISAPRE Vida Tres",
                "ISAPRE Nueva Masvida",
                "Particular",
                "Otro"
            });

            return View();
        }

        // POST: Pacientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Rut,Telefono,Email,Direccion,Prevision")] Paciente paciente)
        {
            if (_context.Pacientes.Any(p => p.Rut == paciente.Rut))
            {
                ModelState.AddModelError("Rut", "Ya existe un paciente con este RUT.");
            }
            if (ModelState.IsValid)
            {
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Previsiones"] = new SelectList(new[] {
                "FONASA",
                "ISAPRE Banmédica",
                "ISAPRE Colmena",
                "ISAPRE Cruz Blanca",
                "ISAPRE Consalud",
                "ISAPRE Vida Tres",
                "ISAPRE Nueva Masvida",
                "Particular",
                "Otro"
            }, paciente.Prevision);

            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null) return NotFound();

            ViewData["Previsiones"] = new SelectList(new[] {
                "FONASA",
                "ISAPRE Banmédica",
                "ISAPRE Colmena",
                "ISAPRE Cruz Blanca",
                "ISAPRE Consalud",
                "ISAPRE Vida Tres",
                "ISAPRE Nueva Masvida",
                "Particular",
                "Otro"
            }, paciente.Prevision);

            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Telefono,Email,Direccion,Prevision")] Paciente paciente)
        {
            if (id != paciente.Id)
                return NotFound();

            var pacienteExistente = await _context.Pacientes.FindAsync(id);
            if (pacienteExistente == null)
                return NotFound();

            pacienteExistente.Nombre = paciente.Nombre;
            pacienteExistente.Telefono = paciente.Telefono;
            pacienteExistente.Email = paciente.Email;
            pacienteExistente.Direccion = paciente.Direccion;
            pacienteExistente.Prevision = paciente.Prevision;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacienteExistente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["Previsiones"] = new SelectList(new[] {
        "FONASA", "ISAPRE Banmédica", "ISAPRE Colmena", "ISAPRE Cruz Blanca",
        "ISAPRE Consalud", "ISAPRE Vida Tres", "ISAPRE Nueva Masvida", "Particular", "Otro"
    }, paciente.Prevision);

            return View(pacienteExistente);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var paciente = await _context.Pacientes.FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null) return NotFound();

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }
    }
}
