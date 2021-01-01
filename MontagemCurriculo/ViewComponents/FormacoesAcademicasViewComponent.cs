using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemCurriculo.ViewComponents
{
    public class FormacoesAcademicasViewComponent : ViewComponent
    {
        private readonly Contexto _contexto;
        public FormacoesAcademicasViewComponent(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var dados = _contexto.FormacaoAcademicas.Include(f => f.TipoCurso).Where(f => f.CurriculoId == id);

            return View(await dados.ToListAsync());
        }
    }
}