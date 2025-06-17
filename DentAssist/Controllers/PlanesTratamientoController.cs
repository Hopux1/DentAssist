using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentAssist.Data;
using DentAssist.Models;
using Rotativa.AspNetCore;

namespace DentAssist.Controllers
{
    public class PlanesTratamientoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlanesTratamientoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlanTratamientos

        public async Task<IActionResult> Index(string buscarPaciente)
        {
            var planes = _context.PlanesTratamiento
                .Include(p => p.Odontologo)
                .Include(p => p.Paciente)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscarPaciente))
            {
                planes = planes.Where(p => p.Paciente.Nombre.Contains(buscarPaciente));
            }

            ViewBag.FiltroPaciente = buscarPaciente;
            return View(await planes.ToListAsync());
        }


        // GET: PlanTratamientos/Create
        public IActionResult Create()
        {
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Nombre");
            ViewData["OdontologoId"] = new SelectList(_context.Odontologos, "Id", "Nombre");
            return View();
        }

        // POST: PlanTratamientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Observaciones,PacienteId,OdontologoId")] PlanTratamiento planTratamiento)
        {
            bool planExistente = _context.PlanesTratamiento.Any(p =>
                p.PacienteId == planTratamiento.PacienteId &&
                p.OdontologoId == planTratamiento.OdontologoId &&
                p.Estado != "Finalizado");

            if (planExistente)
            {
                ModelState.AddModelError("", "Este paciente ya tiene un plan activo con este odontólogo.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(planTratamiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Nombre", planTratamiento.PacienteId);
            ViewData["OdontologoId"] = new SelectList(_context.Odontologos, "Id", "Nombre", planTratamiento.OdontologoId);
            return View(planTratamiento);
        }


        // GET: PlanTratamientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planTratamiento = await _context.PlanesTratamiento.FindAsync(id);
            if (planTratamiento == null)
            {
                return NotFound();
            }
            ViewData["OdontologoId"] = new SelectList(_context.Odontologos, "Id", "Nombre", planTratamiento.OdontologoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Nombre", planTratamiento.PacienteId);
            return View(planTratamiento);
        }

        // POST: PlanTratamientos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Observaciones,PacienteId,OdontologoId")] PlanTratamiento planTratamiento)
        {
            if (id != planTratamiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planTratamiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanTratamientoExists(planTratamiento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OdontologoId"] = new SelectList(_context.Odontologos, "Id", "Matricula", planTratamiento.OdontologoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Nombre", planTratamiento.PacienteId);
            return View(planTratamiento);
        }

        // GET: PlanesTratamiento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var planTratamiento = await _context.PlanesTratamiento
                .Include(p => p.Paciente)
                .Include(p => p.Odontologo)
                .Include(p => p.Pasos)
                    .ThenInclude(p => p.Tratamiento)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (planTratamiento == null)
                return NotFound();

            return View(planTratamiento);
        }


        // GET: PlanTratamientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planTratamiento = await _context.PlanesTratamiento
                .Include(p => p.Odontologo)
                .Include(p => p.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planTratamiento == null)
            {
                return NotFound();
            }

            return View(planTratamiento);
        }

        // POST: PlanTratamientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planTratamiento = await _context.PlanesTratamiento.FindAsync(id);
            if (planTratamiento != null)
            {
                _context.PlanesTratamiento.Remove(planTratamiento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanTratamientoExists(int id)
        {
            return _context.PlanesTratamiento.Any(e => e.Id == id);
        }

        public IActionResult ExportarPDF(int id)
        {
            var plan = _context.PlanesTratamiento
                .Include(p => p.Paciente)
                .Include(p => p.Odontologo)
                .Include(p => p.Pasos)
                    .ThenInclude(p => p.Tratamiento)
                .FirstOrDefault(p => p.Id == id);

            if (plan == null) return NotFound();

            return new ViewAsPdf("DetailsPDF", plan)
            {
                FileName = $"PlanTratamiento_{id}.pdf"
            };
        }

    }
}
