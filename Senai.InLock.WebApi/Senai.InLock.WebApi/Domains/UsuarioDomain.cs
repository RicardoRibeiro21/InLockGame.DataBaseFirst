using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Domains
{
    public class UsuarioDomain
    {
        public int UsuarioId { get; set;}
        [Required(ErrorMessage="Insira um email")]
        [DataType(DataType.EmailAddress)]
        public string  Email { get; set; }
        [Required(ErrorMessage = "Insira uma senha")]
        public string  Senha { get; set; }
        //[Required(ErrorMessage = "Insira um tipo de usuário")]
        public string TipoUsuario { get; set; }
    }
}
