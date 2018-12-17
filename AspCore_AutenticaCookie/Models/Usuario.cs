using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspCore_AutenticaCookie.Models
{
    public class Usuario
    {
        [Required]
        public string Login { get; set; }
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
    }
}
