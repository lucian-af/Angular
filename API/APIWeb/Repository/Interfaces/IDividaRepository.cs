using System.Collections.Generic;
using System.Threading.Tasks;
using APIWeb.ViewModel;
using APIWeb.Models;

namespace APIWeb.Repository.Interfaces
{
    interface IDividaRepository
    {
        Task<IEnumerable<DividaClienteViewModel>> ObterDividasPorCliente(int idCliente);
        Task<bool> InserirDivida(Divida divida);
        Task<bool> AtualizarDivida(Divida divida);
        Task<bool> DeletarDivida(int idDivida);
        Task<IEnumerable<ComissaoViewModel>> ObterComissoes();

    }
}
