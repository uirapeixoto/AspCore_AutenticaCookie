using AspCore_AutenticaCookie.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspCore_AutenticaCookie.Controllers
{
    public class AcessoController : Controller
    {

        private readonly IAutenticacao _autentica;


        public AcessoController(IAutenticacao autentica)
        {
            _autentica = autentica;
        }

        public IActionResult Login()
        {
            TempData["Registro"] = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind] UsuarioViewModel usuario)
        {

            ModelState.Remove("Nome");
            ModelState.Remove("Email");
            ModelState.Remove("Login");

            if (ModelState.IsValid)
            {
                string LoginStatus = _autentica.ValidarLogin(usuario);
                if (LoginStatus == "Sucesso")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Login)
                    };
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    if (User.Identity.IsAuthenticated)
                        return RedirectToAction("Home", "Cliente");
                    else
                    {
                        TempData["Falhou"] = "O login falhou. Informe as credenciais corretas " + User.Identity.Name;
                        return View();
                    }
                }
                else
                {
                    TempData["Falhou"] = "O login falhou. Informe as credenciais corretas";
                    return View();
                }
            }
            else
            {
                TempData["Registro"] = false;
                return View();
            }
        }

        [HttpPost]
        public IActionResult RegistrarUsuario([Bind] UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                string RegistroStatus = _autentica.RegistrarUsuario(usuario);
                if (RegistroStatus == "Sucesso")
                {
                    ModelState.Clear();
                    TempData["Sucesso"] = "Registro realizado com sucesso!";
                    TempData["Registro"] = false;
                }
                else
                {
                    TempData["Falhou"] = "O Registro do usuário falhou";
                }
                TempData["Registro"] = true;
            }
            return View();
        }
    }
}