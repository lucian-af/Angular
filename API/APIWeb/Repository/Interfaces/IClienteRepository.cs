using System.Collections.Generic;
using System.Threading.Tasks;
using APIWeb.Models;

namespace APIWeb.Repository.Interfaces
{
    interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObterTodosClientes();
        Task<Cliente> ObterCliente(int idCliente);
        Task<bool> InserirCliente(Cliente cliente);
        Task<bool> AtualizarCliente(Cliente cliente);
        Task<bool> DeletarCliente(int idCliente);
    }
}
