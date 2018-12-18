using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspCore_AutenticaCookie.Models
{
    public class UsuarioViewModel
    {
        private string _login;
        private string _email;

        [Required]
        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match($@"{value}");
                if (match.Success)
                {
                    Email = value;
                    _login = string.Empty;
                }
                else
                {
                    Email = string.Empty;
                    _login = value;
                }

            }
        }
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Required]
        [Display(Name = "Nome do usuário :")]
        public string Nome { get; set; }
        [Required]
        [Display(Name = "Email do usuário :")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public UsuarioViewModel()
        {
            Email = string.Empty;
            Nome = string.Empty;
        }
    }
}
