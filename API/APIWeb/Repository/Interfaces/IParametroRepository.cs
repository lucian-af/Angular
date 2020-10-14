using System.Collections.Generic;
using System.Threading.Tasks;
using APIWeb.Models;
using APIWeb.ViewModel;

namespace APIWeb.Repository.Interfaces
{
    interface IParametroRepository
    {
        Task<IEnumerable<ParametroViewModel>> ObterTodosParametros();
        Task<Parametro> ObterParametro(int IdParametro);
        Task<bool> InserirParametro(Parametro parametro);
        Task<bool> AtualizarParametro(Parametro parametro);
        Task<bool> DeletarParametro(int IdParametro);
    }
}