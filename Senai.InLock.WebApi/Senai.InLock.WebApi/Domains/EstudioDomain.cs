using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Domains
{
    public class EstudioDomain
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Insira um nome para o estúdio")]
        public string  Nome { get; set; }

        public JogoDomain Jogo { get; set; }
    }
}
