﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AspCore_AutenticaCookie.Models
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind] UsuarioViewModel usuario)
        {

            ModelState.Remove("Nome");
            ModelState.Remove("Email");
            ModelState.Remove("Email");

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
                        return RedirectToAction("UsuarioHome", "Usuario");
                    else
                    {
                        TempData["Falhou"] = "O login falhou. Informe as credenciais corretas " + User.Identity.Name;
                        return RedirectToAction("LoginUsuario", "Login");
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
                return View();
            }
        }

        public IActionResult RegistrarUsuario()
        {
            return View();
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
                }
                else
                {
                    TempData["Falhou"] = "O Registro do usuário falhou";
                }
            }
            return View();
        }

    }
}