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
        [HttpGet("v1/token")]
        public IActionResult Token()
        {

            return StatusCode(200);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> GetToken([FromHeader] string key)
        {
            // Recupera o usuário
            var user = UserRepository.Get();

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Retorna os dados
            return new
            {
                token = token
            };
        }

    }
}
