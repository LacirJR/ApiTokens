using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidationApi.Models;
using ValidationApi.Repositories;
using ValidationApi.Services;

namespace ValidationApi.Controllers
{

    [ApiController]
    public class ValidationController : ControllerBase
    {
        private readonly Ef.CONN_STRContext _connStrContext;
        public ValidationController()
        {
            _connStrContext = new Ef.CONN_STRContext();
        }

        [HttpGet]
        [Route("v1/token")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> GetToken([FromHeader] string client_id, [FromHeader] string client_secret)
        {
            try
            {

                var user = UserRepository.Get(client_id, client_secret);
                var token = TokenService.GenerateToken(user);

                return new
                {
                    token = token
                };
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet]
        [Route("v1/GetString")]
        [Authorize]
        public IActionResult StringConnection([FromQuery] string keyValue)
        {

            try
            {
                var valueConnection = _connStrContext.StrConexaos.Where(x => x.Chave == keyValue).Select(x => x.Valor).First();

                if (string.IsNullOrEmpty(valueConnection))
                    throw new InvalidOperationException("Nao existe string de conexao para esta chave.");

                return StatusCode(200, new {StringConnection = valueConnection });

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

        }
        [HttpPost]
        [Route("v1/RegistrationString")]
        [Authorize]
        public IActionResult RegistrationString([FromQuery] string valueConnection)
        {

            try
            {
                var key =  Guid.NewGuid().ToString();

                var jaExiste = string.IsNullOrEmpty(_connStrContext.StrConexaos.Where(x => x.Valor == valueConnection).Select(x => x.Valor).FirstOrDefault()) ? false : true;

                if (jaExiste)
                    throw new InvalidOperationException("Ja existe essa string de conexao armazenada.");
                _connStrContext.StrConexaos.Add(new Ef.StrConexao
                {
                    Chave = key,
                    Valor = valueConnection,
                });
                _connStrContext.SaveChanges();


                if (string.IsNullOrEmpty(valueConnection))
                    throw new InvalidOperationException("Nao existe string de conexao para esta chave.");

                return StatusCode(200, new { ConnectionKey = key });

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

        }

        [HttpGet]
        [Route("v1/AllStrings")]
        [Authorize(Roles = "MASTER")]
        public IActionResult AllStrings()
        {

            try
            {
                var key = Guid.NewGuid().ToString();

                var listaStrings = _connStrContext.StrConexaos.Select(x => x).ToList();

                return StatusCode(200, new { ListaStrings = listaStrings });

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

        }

    }
}
