using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontagemCurriculo.Controllers
{
    public class InformacoesLoginController : Controller
    {
        private readonly Contexto _contexto;

        public InformacoesLoginController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("Usuario");

            var dados = _contexto.InformacoesLogin.Include(u => u.Usuario)
                        .Where(i => i.UsuarioId == usuarioId);

            return View(await dados.ToListAsync());
        }

        public IActionResult DownloadDados()
        {
            var usuarioId = HttpContext.Session.GetInt32("Usuario");

            var dados = _contexto.InformacoesLogin.Include(u => u.Usuario)
                        .Where(i => i.UsuarioId == usuarioId);

            StringBuilder arquivo = new StringBuilder();
            arquivo.AppendLine("EnderecoIP;Data;Horario");

            foreach (var item in dados)
            {
                arquivo.AppendLine($@"{item.EnderecoIp};{item.Data};{item.Horario}");
            }

            return File(Encoding.ASCII.GetBytes(arquivo.ToString()), "text/csv", "dados.csv");
        }
    }
}