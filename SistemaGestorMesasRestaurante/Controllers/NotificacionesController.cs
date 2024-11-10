using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGestorMesasRestaurante.Data;
using SistemaGestorMesasRestaurante.Models;

namespace SistemaGestorMesasRestaurante.Controllers
{
    public class NotificacionesController : Controller
    {
        private readonly SistemaGestionContext _context;

        public NotificacionesController(SistemaGestionContext context)
        {
            _context = context;
        }

        // GET: Notificaciones
        public async Task<IActionResult> Index()
        {
            var sistemaGestionContext = _context.Notificacions.Include(n => n.Cliente).Include(n => n.Reserva);
            return View(await sistemaGestionContext.ToListAsync());
        }

        // GET: Notificaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificacion = await _context.Notificacions
                .Include(n => n.Cliente)
                .Include(n => n.Reserva)
                .FirstOrDefaultAsync(m => m.NotificacionId == id);
            if (notificacion == null)
            {
                return NotFound();
            }

            return View(notificacion);
        }

        // GET: Notificaciones/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId");
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "ReservaId", "ReservaId");
            return View();
        }

        // POST: Notificaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotificacionId,ClienteId,ReservaId,Tipo,FechaEnvio,Estado")] Notificacion notificacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", notificacion.ClienteId);
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "ReservaId", "ReservaId", notificacion.ReservaId);
            return View(notificacion);
        }

        // GET: Notificaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificacion = await _context.Notificacions.FindAsync(id);
            if (notificacion == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", notificacion.ClienteId);
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "ReservaId", "ReservaId", notificacion.ReservaId);
            return View(notificacion);
        }

        // POST: Notificaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NotificacionId,ClienteId,ReservaId,Tipo,FechaEnvio,Estado")] Notificacion notificacion)
        {
            if (id != notificacion.NotificacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notificacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificacionExists(notificacion.NotificacionId))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", notificacion.ClienteId);
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "ReservaId", "ReservaId", notificacion.ReservaId);
            return View(notificacion);
        }

        // GET: Notificaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificacion = await _context.Notificacions
                .Include(n => n.Cliente)
                .Include(n => n.Reserva)
                .FirstOrDefaultAsync(m => m.NotificacionId == id);
            if (notificacion == null)
            {
                return NotFound();
            }

            return View(notificacion);
        }

        // POST: Notificaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notificacion = await _context.Notificacions.FindAsync(id);
            if (notificacion != null)
            {
                _context.Notificacions.Remove(notificacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificacionExists(int id)
        {
            return _context.Notificacions.Any(e => e.NotificacionId == id);
        }
    }
}
