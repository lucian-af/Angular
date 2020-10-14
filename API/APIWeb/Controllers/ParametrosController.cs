using System.Collections.Generic;
using System.Threading.Tasks;
using APIWeb.Models;
using APIWeb.Repository;
using APIWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametrosController : ControllerBase
    {
        private readonly ParametroRepository _repository;

        public ParametrosController(ParametroRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Parametros
        [HttpGet]
        public async Task<IEnumerable<ParametroViewModel>> GetParametros()
        {
            return await _repository.ObterTodosParametros();
        }

        // GET: api/Parametros/1
        [HttpGet("{IdParametro}")]
        public async Task<Parametro> GetParametros(int IdParametro)
        {
            return await _repository.ObterParametro(IdParametro);
        }

        // PUT: api/Parametros
        [HttpPut]
        public async Task<bool> PutParametro(Parametro parametro)
        {
            return await _repository.AtualizarParametro(parametro);
        }

        // POST: api/Parametros
        [HttpPost]
        public async Task<bool> PostParametro(Parametro parametro)
        {
            return await _repository.InserirParametro(parametro);
        }

        // DELETE: api/Parametros/5
        [HttpDelete("{IdParametro}")]
        public async Task<bool> DeleteParametro(int IdParametro)
        {
            return await _repository.DeletarParametro(IdParametro);
        }
    }
}
