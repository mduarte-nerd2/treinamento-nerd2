using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TreinamentoNerd2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        // GET: api/<AlunoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Professor01", "Professor01" };
        }
    }
}
