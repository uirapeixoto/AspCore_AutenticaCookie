using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCore_AutenticaCookie.Models
{
    public interface IAutenticacao
    {
        string GetConnectionString();
        string RegistrarUsuario(UsuarioViewModel usuario);
        string ValidarLogin(UsuarioViewModel usuario);
    }
}
