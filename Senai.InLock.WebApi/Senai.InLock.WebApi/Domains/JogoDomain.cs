using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Domains
{
    public class JogoDomain
    {
        public int JogoId { get; set; }
        [Required(ErrorMessage = "Insira um nome para o jogo")]
        public string NomeJogo { get; set; }
        [Required(ErrorMessage = "Insira uma descrição para o jogo")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Insira a data de lançamento do jogo")]
        public DateTime DataLancamento { get; set; }
        [Required(ErrorMessage = "Insira um valor para o jogo")]
        public decimal Valor { get; set; }
        public EstudioDomain estudio { get; set; }
        [Required(ErrorMessage = "Insira um estúdio para o jogo")]
        public int EstudioId { get; set; }
    }
}
