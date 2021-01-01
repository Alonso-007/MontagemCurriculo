using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;
using MontagemCurriculo.ViewModels;
using Rotativa.AspNetCore;

namespace MontagemCurriculo.Controllers
{
    public class CurriculosController : Controller
    {
        private readonly Contexto _context;

        public CurriculosController(Contexto context)
        {
            _context = context;
        }

        // GET: Curriculos
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Curriculos.Include(c => c.Usuario);
            return View(await contexto.ToListAsync());
        }

        // GET: Curriculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.Curriculos
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.CurriculoId == id);
            if (curriculo == null)
            {
                return NotFound();
            }

            return View(curriculo);
        }

        // GET: Curriculos/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CurriculoId,Nome,UsuarioId")] Curriculo curriculo)
        {
            curriculo.UsuarioId = int.Parse(HttpContext.Session.GetInt32("Usuario").ToString());

            if (ModelState.IsValid)
            {
                _context.Add(curriculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(curriculo);
        }

        // GET: Curriculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.Curriculos.FindAsync(id);
            if (curriculo == null)
            {
                return NotFound();
            }

            return View(curriculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CurriculoId,Nome,UsuarioId")] Curriculo curriculo)
        {
            curriculo.UsuarioId = int.Parse(HttpContext.Session.GetInt32("Usuario").ToString());

            if (id != curriculo.CurriculoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curriculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurriculoExists(curriculo.CurriculoId))
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

            return View(curriculo);
        }

        // POST: Curriculos/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var curriculo = await _context.Curriculos.FindAsync(id);
            _context.Curriculos.Remove(curriculo);
            await _context.SaveChangesAsync();
            return Json(curriculo.Nome + " excluído com sucesso ");
        }

        private bool CurriculoExists(int id)
        {
            return _context.Curriculos.Any(e => e.CurriculoId == id);
        }

        public IActionResult VisualizarComoPDF()
        {
            var id = HttpContext.Session.GetInt32("Usuario");

            CurriculoViewModel curriculo = new CurriculoViewModel();

            curriculo.Objetivos = _context.Objetivos.Where(o => o.Curriculo.UsuarioId == id).ToList();
            curriculo.FormacoesAcademicas = _context.FormacaoAcademicas.Include(fa => fa.TipoCurso)
                                                .Where(fa => fa.Curriculo.UsuarioId == id).ToList();
            curriculo.ExperienciasProfissionais = _context.ExperienciaProfissionais.Where(ep => ep.Curriculo.UsuarioId == id).ToList();
            curriculo.Idiomas = _context.Idiomas.Where(i => i.Curriculo.UsuarioId == id).ToList();

            return new ViewAsPdf("PDF", curriculo) { FileName = "Curriculo.pdf" };
        }
    }
}