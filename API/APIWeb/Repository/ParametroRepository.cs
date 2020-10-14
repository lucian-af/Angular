using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWeb.Data;
using APIWeb.Models;
using APIWeb.Repository.Interfaces;
using APIWeb.ViewModel;
using Microsoft.EntityFrameworkCore;
using static APIWeb.Enums.Enums;

namespace APIWeb.Repository
{
    public class ParametroRepository : IParametroRepository
    {
        private readonly Context _context;

        public ParametroRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ParametroViewModel>> ObterTodosParametros()
        {
            return await _context.Parametros
                .Select(param => new ParametroViewModel
                {
                    IdParametro = param.IdParametro,
                    QuantidadeParcelaMaxima = param.QuantidadeParcelaMaxima,
                    TipoJuros = param.TipoJuros,
                    PercentualJuros = param.PercentualJuros,
                    PercentualComissao = param.PercentualComissao,
                    DescricaoTipoJuros = param.TipoJuros == (int)eTipoJuros.JurosComposto ? "JUROS COMPOSTO" : "JUROS SIMPLES"
                })
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Parametro> ObterParametro(int idParametro)
        {
            return await _context.Parametros.FindAsync(idParametro);
        }
        public async Task<bool> InserirParametro(Parametro parametro)
        {
            // Por padrão só terá um registro na tabela de Parametro
            // se a validação no FrontEnd falhar, o backend garante que não insira mais um registro
            if (parametro is null || _context.Parametros.FirstOrDefault().IdParametro > 0)
            {
                return false;
            }

            try
            {
                _context.Parametros.Add(parametro);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }
        public async Task<bool> AtualizarParametro(Parametro parametro)
        {
            try
            {
                _context.Entry(parametro).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public async Task<bool> DeletarParametro(int idParametro)
        {
            try
            {
                Parametro parametro = _context.Parametros.Find(idParametro);
                _context.Parametros.Remove(parametro);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
