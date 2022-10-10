using Aplicacao.Interfaces;
using Dominio.Paginacao;
using Entidades.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        ILogger<UsuarioController> _logger;
        private readonly IAplicacaoUsuario _IAplicacaoUsuario;

        public UsuarioController(IAplicacaoUsuario aplicacaoUsuario, ILogger<UsuarioController> logger)
        {
            _IAplicacaoUsuario = aplicacaoUsuario;
            _logger = logger;
        }


        /// <summary>
        ///  Retorna lista de usuários cadastrados na base de dados
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Paginacao<Usuario>), 200)]
        public async Task<IActionResult> GetUsuario([FromQuery] FiltroUsuario filtro)
        {
            try
            {
                Expression<Func<Usuario, bool>> expresssion = item =>
                (!string.IsNullOrEmpty(filtro.Nome) ? item.Nome.ToLower().Contains(filtro.Nome.ToLower()) : true) &&
                (!string.IsNullOrEmpty(filtro.Sobrenome) ? item.Sobrenome.ToLower().Contains(filtro.Sobrenome.ToLower()) : true) &&
                (!string.IsNullOrEmpty(filtro.Email) ? item.Email.ToLower().Contains(filtro.Email.ToLower()) : true) &&
                (filtro.Escolaridade.HasValue ? item.Escolaridade == filtro.Escolaridade.Value : true);

                return Ok(await _IAplicacaoUsuario.Paginar(filtro.Pagina, filtro.PaginaQuantidade, expresssion));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.InnerException?.Message);
                _logger.LogError(ex.StackTrace);

                return StatusCode(500, new
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException?.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }


        /// <summary>
        /// Retorna dados do usuario cadastrados na base de acordo com o id informado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Usuario), 200)]
        public async Task<IActionResult> GetUsuario(int id)
        {
            try
            {
                return Ok(await _IAplicacaoUsuario.BuscarPorId(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.InnerException?.Message);
                _logger.LogError(ex.StackTrace);

                return StatusCode(500, new
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException?.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }


        /// <summary>
        ///  Cadastra usuario na base de dados
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Usuario), 200)]
        public async Task<IActionResult> PostUsario([FromBody] Usuario usuario)
        {
            try
            {
                await _IAplicacaoUsuario.Adicionar(usuario);

                if (usuario.Notificacoes.Count > 0)
                {
                    return BadRequest(usuario.Notificacoes.Select(campo => new { mensagem = campo.Mensagem }));
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.InnerException?.Message);
                _logger.LogError(ex.StackTrace);

                return StatusCode(500, new
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException?.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }


        /// <summary>
        /// Altera os dados do usuario cadastrado na base de dados
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Usuario), 200)]
        public async Task<IActionResult> PutUsuario([FromBody] Usuario usuario)
        {
            try
            {
                await _IAplicacaoUsuario.Alterar(usuario);

                if (usuario.Notificacoes.Count > 0)
                {
                    return BadRequest(usuario.Notificacoes.Select(campo => new { mensagem = campo.Mensagem }));
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.InnerException?.Message);
                _logger.LogError(ex.StackTrace);

                return StatusCode(500, new
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException?.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }


        /// <summary>
        /// Excluir os dados cadastrados do usuário na base de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                await _IAplicacaoUsuario.Excluir(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.InnerException?.Message);
                _logger.LogError(ex.StackTrace);

                return StatusCode(500, new
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException?.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }


    }
}
