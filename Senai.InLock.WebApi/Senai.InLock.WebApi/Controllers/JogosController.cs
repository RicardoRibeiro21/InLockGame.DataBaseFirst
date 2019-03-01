using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using Senai.InLock.WebApi.Repositories;

namespace Senai.InLock.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private IJogoRepository JogoRepository { get; set; }

        public JogosController()
        {
            JogoRepository = new JogoRepository();
        }
        //[Authorize(Roles = "ADMINISTRADOR, CLIENTE")]
        [HttpGet]
           public IActionResult Get()
        {
            try
            {                               
                return Ok(JogoRepository.ListarJogos());
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public  IActionResult Post(JogoDomain jogo)
        {
            try
            {
                JogoRepository.Cadastrar(jogo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}