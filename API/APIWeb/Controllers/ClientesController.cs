using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWeb.Data;
using APIWeb.Models;
using APIWeb.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteRepository _repository;

        public ClientesController(ClienteRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Clientes
        [HttpGet]
        public Task<IEnumerable<Cliente>> GetClientes()
        {
            return _repository.ObterTodosClientes();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public Task<Cliente> GetCliente(int id)
        {
            return _repository.ObterCliente(id);
        }

        // PUT: api/Clientes
        [HttpPut]
        public Task<bool> PutCliente(Cliente cliente)
        {
            return _repository.AtualizarCliente(cliente);
        }

        // POST: api/Clientes
        [HttpPost]
        public Task<bool> PostCliente(Cliente cliente)
        {
            return _repository.InserirCliente(cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public Task<bool> DeleteCliente(int id)
        {
            return _repository.DeletarCliente(id);
        }
    }
}
