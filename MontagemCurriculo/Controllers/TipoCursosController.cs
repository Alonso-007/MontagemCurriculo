using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;

namespace MontagemCurriculo.Controllers
{
    public class TipoCursosController : Controller
    {
        private readonly Contexto _context;

        public TipoCursosController(Contexto context)
        {
            _context = context;
        }

        // GET: TipoCursoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoCursos.ToListAsync());
        }

        // GET: TipoCursoes/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoCursoId,Tipo")] TipoCurso tipoCurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoCurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoCurso);
        }

        // GET: TipoCursoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCurso = await _context.TipoCursos.FindAsync(id);
            if (tipoCurso == null)
            {
                return NotFound();
            }
            return View(tipoCurso);
        }

        // POST: TipoCursoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoCursoId,Tipo")] TipoCurso tipoCurso)
        {
            if (id != tipoCurso.TipoCursoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoCurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoCursoExists(tipoCurso.TipoCursoId))
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
            return View(tipoCurso);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var tipoCurso = await _context.TipoCursos.FindAsync(id);
            _context.TipoCursos.Remove(tipoCurso);
            await _context.SaveChangesAsync();
            return Json(tipoCurso + " excluído com sucesso ");
        }

        private bool TipoCursoExists(int id)
        {
            return _context.TipoCursos.Any(e => e.TipoCursoId == id);
        }
    }
}
