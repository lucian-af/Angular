using System.Collections.Generic;
using System.Threading.Tasks;
using APIWeb.ViewModel;
using APIWeb.Models;
using APIWeb.Repository;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DividasController : ControllerBase
    {
        private readonly DividaRepository _repository;

        public DividasController(DividaRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Dividas/1
        [HttpGet("{idCliente}")]
        public Task<IEnumerable<DividaClienteViewModel>> GetDividas(int idCliente)
        {
            return _repository.ObterDividasPorCliente(idCliente);
        }

        // GET: api/Dividas/comissao
        [HttpGet("comissao")]
        public Task<IEnumerable<ComissaoViewModel>> GetComissoes()
        {
            return _repository.ObterComissoes();
        }

        // PUT: api/Dividas
        [HttpPut]
        public Task<bool> PutDivida(Divida divida)
        {
            return _repository.AtualizarDivida(divida);
        }

        // POST: api/Dividas
        [HttpPost]
        public Task<bool> PostDivida(Divida divida)
        {
            return _repository.InserirDivida(divida);
        }

        // DELETE: api/Dividas/5
        [HttpDelete("{id}")]
        public Task<bool> DeleteDivida(int id)
        {
            return _repository.DeletarDivida(id);
        }
    }
}
