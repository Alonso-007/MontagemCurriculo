using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemCurriculo.ViewComponents
{
    public class ExperienciasProfissionaisViewComponent : ViewComponent
    {
        private readonly Contexto _contexto;

        public ExperienciasProfissionaisViewComponent(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var dados = _contexto.ExperienciaProfissionais.Where(ep => ep.CurriculoId == id);

            return View(await dados.ToListAsync());
        }
    }
}