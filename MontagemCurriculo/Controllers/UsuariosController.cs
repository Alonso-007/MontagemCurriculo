using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;
using MontagemCurriculo.ViewModels;

namespace MontagemCurriculo.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly Contexto _context;

        public UsuariosController(Contexto context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro([Bind("UsarioId,Email,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                

                InformacaoLogin informacao = new InformacaoLogin()
                {
                    UsuarioId = usuario.UsarioId,
                    EnderecoIp = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Data = DateTime.Now.ToShortDateString(),
                    Horario = DateTime.Now.ToShortTimeString()
                };

                _context.Add(informacao);
                await _context.SaveChangesAsync();

                HttpContext.Session.SetInt32("Usuario", usuario.UsarioId);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, usuario.Email)
                };
                var userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                return RedirectToAction(nameof(Index), "Curriculos");
            }
            return View(usuario);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.Session.Clear();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (_context.Usuarios.Any(u => u.Email == login.Email && u.Senha == login.Senha))
            {
                int id = _context.Usuarios.Where(u => u.Email == login.Email && u.Senha == login.Senha).
                                                    Select(u => u.UsarioId).Single();

                InformacaoLogin informacao = new InformacaoLogin()
                {
                    UsuarioId = id,
                    EnderecoIp = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Data = DateTime.Now.ToShortDateString(),
                    Horario = DateTime.Now.ToShortTimeString()
                };

                _context.Add(informacao);
                await _context.SaveChangesAsync();

                HttpContext.Session.SetInt32("Usuario", id);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, login.Email)
                };
                var userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                return RedirectToAction(nameof(Index), "Curriculos");
            }

            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Usuarios");
        }

        //usado para validacao remota
        public JsonResult UsuarioExiste(string email)
        {
            if (!_context.Usuarios.Any(e => e.Email == email))
            {
                return Json(true);
            }
            return Json("Email existente");
        }
    }
}