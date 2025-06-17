using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentAssist.Data;
using DentAssist.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DentAssist.Controllers
{
    public class PasosTratamientoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PasosTratamientoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PasosTratamiento/Create
        public IActionResult Create(int? PlanTratamientoId)
        {
            ViewData["TratamientoId"] = new SelectList(_context.Tratamientos, "Id", "Nombre");
            ViewData["Estado"] = new SelectList(new[] { "Pendiente", "Realizado", "Cancelado" });

            return View(new PasoTratamiento
            {
                PlanTratamientoId = PlanTratamientoId ?? 0
            });
        }

        // POST: PasosTratamiento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PasoTratamiento pasoTratamiento)
        {
            var plan = await _context.PlanesTratamiento.FindAsync(pasoTratamiento.PlanTratamientoId);

            if (plan == null)
                return NotFound("El plan de tratamiento no existe.");

            if (plan.Estado == "Finalizado")
                ModelState.AddModelError("", "No se pueden agregar pasos a un plan de tratamiento finalizado.");

            if (pasoTratamiento.FechaEstimada < DateTime.Now)
                ModelState.AddModelError("FechaEstimada", "La fecha estimada no puede estar en el pasado.");

            if (!ModelState.IsValid)
            {
                CargarViewData(pasoTratamiento);
                return View(pasoTratamiento);
            }

            _context.Add(pasoTratamiento);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "PlanesTratamiento", new { id = pasoTratamiento.PlanTratamientoId });
        }

        // GET: PasosTratamiento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var pasoTratamiento = await _context.PasosTratamiento.FindAsync(id);
            if (pasoTratamiento == null) return NotFound();

            CargarViewData(pasoTratamiento);
            return View(pasoTratamiento);
        }

        // POST: PasosTratamiento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TratamientoId,FechaEstimada,Estado,Observaciones")] PasoTratamiento pasoTratamiento)
        {
            if (id != pasoTratamiento.Id)
                return NotFound();

            var originalPaso = await _context.PasosTratamiento.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (originalPaso == null)
                return NotFound();

            pasoTratamiento.PlanTratamientoId = originalPaso.PlanTratamientoId;

            if (pasoTratamiento.FechaEstimada < DateTime.Now)
                ModelState.AddModelError("FechaEstimada", "La fecha estimada no puede estar en el pasado.");

            if (!ModelState.IsValid)
            {
                CargarViewData(pasoTratamiento);
                return View(pasoTratamiento);
            }

            try
            {
                _context.Update(pasoTratamiento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasoTratamientoExists(id)) return NotFound();
                throw;
            }

            return RedirectToAction("Details", "PlanesTratamiento", new { id = pasoTratamiento.PlanTratamientoId });
        }

        // GET: PasosTratamiento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var pasoTratamiento = await _context.PasosTratamiento
                .Include(p => p.Tratamiento)
                .Include(p => p.PlanTratamiento)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pasoTratamiento == null) return NotFound();

            return View(pasoTratamiento);
        }

        // POST: PasosTratamiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pasoTratamiento = await _context.PasosTratamiento.FindAsync(id);
            if (pasoTratamiento != null)
            {
                _context.PasosTratamiento.Remove(pasoTratamiento);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "PlanesTratamiento", new { id = pasoTratamiento?.PlanTratamientoId ?? 1 });
        }

        private bool PasoTratamientoExists(int id)
        {
            return _context.PasosTratamiento.Any(e => e.Id == id);
        }

        private void CargarViewData(PasoTratamiento paso)
        {
            ViewData["TratamientoId"] = new SelectList(_context.Tratamientos, "Id", "Nombre", paso.TratamientoId);
            ViewData["Estado"] = new SelectList(new[] { "Pendiente", "Realizado", "Cancelado" }, paso.Estado);
            ViewBag.PlanTratamientoId = paso.PlanTratamientoId;
        }
    }
}
