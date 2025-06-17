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
    public class TurnosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TurnosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Turnoes

        public async Task<IActionResult> Index(string buscarPaciente)
        {
            var turnos = _context.Turnos
                .Include(t => t.Odontologo)
                .Include(t => t.Paciente)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscarPaciente))
            {
                turnos = turnos.Where(t => t.Paciente.Nombre.Contains(buscarPaciente));
            }

            ViewBag.FiltroPaciente = buscarPaciente;
            return View(await turnos.ToListAsync());
        }


        // GET: Turnos/Create
        public IActionResult Create()
        {
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Nombre");
            ViewData["OdontologoId"] = new SelectList(_context.Odontologos, "Id", "Nombre");
            ViewData["Estado"] = new SelectList(new[] { "Pendiente", "Confirmado", "Realizado", "Cancelado" });

            return View();
        }

        // POST: Turnos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Turno turno)
        {
            if (turno.FechaHora < DateTime.Now)
            {
                ModelState.AddModelError("FechaHora", "La fecha del turno no puede estar en el pasado.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(turno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["OdontologoId"] = new SelectList(_context.Odontologos, "Id", "Nombre", turno.OdontologoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Nombre", turno.PacienteId);
            return View(turno);
        }


        // GET: Turnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
            ViewData["OdontologoId"] = new SelectList(_context.Odontologos, "Id", "Nombre", turno.OdontologoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Nombre", turno.PacienteId);
            ViewData["Estado"] = new SelectList(new[] { "Pendiente", "Realizado", "Cancelado" }, turno.Estado);
            return View(turno);
        }

        // POST: Turnos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Turno turno)
        {
            if (id != turno.Id)
            {
                return NotFound();
            }

            if (turno.FechaHora < DateTime.Now)
            {
                ModelState.AddModelError("FechaHora", "La fecha del turno no puede estar en el pasado.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Turnos.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["OdontologoId"] = new SelectList(_context.Odontologos, "Id", "Nombre", turno.OdontologoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Nombre", turno.PacienteId);
            return View(turno);
        }


        // GET: Turnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .Include(t => t.Odontologo)
                .Include(t => t.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // POST: Turnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turno = await _context.Turnos.FindAsync(id);
            if (turno != null)
            {
                _context.Turnos.Remove(turno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoExists(int id)
        {
            return _context.Turnos.Any(e => e.Id == id);
        }
    }
}
