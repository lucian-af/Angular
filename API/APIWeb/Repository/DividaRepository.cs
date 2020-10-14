using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWeb.Data;
using APIWeb.ViewModel;
using APIWeb.Models;
using APIWeb.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using static APIWeb.Enums.Enums;

namespace APIWeb.Repository
{
    public class DividaRepository : IDividaRepository
    {
        private readonly Context _context;

        public DividaRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DividaClienteViewModel>> ObterDividasPorCliente(int idCliente)
        {
            var _dividas = new List<DividaClienteViewModel>();
            try
            {
                _dividas = await _context.Dividas.Include(divida => divida.Cliente)
                    .Where(divida => divida.Cliente.IdCliente == idCliente)
                    .Select(divida => new DividaClienteViewModel
                    {
                        IdDivida = divida.IdDivida,
                        NomeCliente = divida.Cliente.Nome,
                        DocumentoCliente = divida.Cliente.Documento,
                        DataVencimento = divida.DataVencimento,
                        QuantidadeParcela = divida.QuantidadeParcela,
                        ValorOriginal = divida.ValorOriginal,
                        DiasAtraso = 0,
                        ValorJuros = 0,
                        ValorDivida = divida.ValorOriginal
                    })
                    .AsNoTracking()
                    .ToListAsync();

                CalcularDivida(ref _dividas);
            }
            catch
            {
                return new List<DividaClienteViewModel>();
            }

            return _dividas;
        }
        private void CalcularDivida(ref List<DividaClienteViewModel> _dividas, bool GerarParcelas = true)
        {
            var _parametro = _context.Parametros.FirstOrDefault();

            _dividas.ForEach(divida =>
            {
                #region Calcular Juros
                divida.DiasAtraso = Math.Abs((int)(DateTime.Now.Date - divida.DataVencimento).TotalDays);
                switch ((eTipoJuros)_parametro.TipoJuros)
                {
                    case eTipoJuros.JurosSimples:
                        divida.ValorDivida += (divida.ValorOriginal * (_parametro.PercentualJuros * divida.DiasAtraso)) / 100;
                        break;
                    case eTipoJuros.JurosComposto:
                        // M = C (1+i)^t                            
                        var fatorJuros = (decimal)Math.Pow((double)(1 + (_parametro.PercentualJuros / 100)), (int)(divida.DiasAtraso / 30));
                        divida.ValorDivida = divida.ValorOriginal * fatorJuros;
                        break;
                }
                divida.ValorJuros = divida.ValorDivida - divida.ValorOriginal;
                #endregion

                #region Gerar Parcelas
                if (GerarParcelas)
                {
                    decimal _ValorParcela = Math.Round(divida.ValorDivida / divida.QuantidadeParcela, 2);
                    for (int i = 1; i <= divida.QuantidadeParcela; i++)
                    {
                        divida.Parcelas.Add(new DividaParcela
                        {
                            NumeroParcela = $"{i.ToString().PadLeft(3, '0')}/{divida.QuantidadeParcela.ToString().PadLeft(3, '0')}",
                            DataVencimento = divida.DataVencimento.AddMonths(i),
                            ValorParcela = i < divida.QuantidadeParcela ? _ValorParcela : divida.ValorDivida - (_ValorParcela * (divida.QuantidadeParcela - 1))
                        });
                    }
                }
                #endregion
            });
        }
        public async Task<bool> InserirDivida(Divida divida)
        {
            var _parametros = _context.Parametros.FirstOrDefault();

            if (divida is null || divida.QuantidadeParcela > _parametros.QuantidadeParcelaMaxima)
            {
                return false;
            }

            try
            {
                _context.Dividas.Add(divida);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }
        public async Task<bool> AtualizarDivida(Divida divida)
        {
            var _parametros = _context.Parametros.FirstOrDefault();

            if (divida is null || divida.QuantidadeParcela > _parametros.QuantidadeParcelaMaxima)
            {
                return false;
            }

            try
            {
                _context.Entry(divida).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public async Task<bool> DeletarDivida(int idDivida)
        {
            try
            {
                Divida divida = _context.Dividas.Find(idDivida);
                _context.Dividas.Remove(divida);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public async Task<IEnumerable<ComissaoViewModel>> ObterComissoes()
        {
            var _dividas = new List<ComissaoViewModel>();
            try
            {
                var _parametro = await _context.Parametros.FirstOrDefaultAsync();

                var _dividaCliente = await _context.Dividas.Include(divida => divida.Cliente)
                    .Select(divida =>
                     new DividaClienteViewModel
                     {
                         NomeCliente = divida.Cliente.Nome,
                         DataVencimento = divida.DataVencimento,
                         QuantidadeParcela = divida.QuantidadeParcela,
                         ValorOriginal = divida.ValorOriginal,
                         DiasAtraso = 0,
                         ValorJuros = 0,
                         ValorDivida = divida.ValorOriginal
                     }).AsNoTracking().ToListAsync();

                CalcularDivida(ref _dividaCliente, false);

                _dividas = _dividaCliente.Select(divida => new ComissaoViewModel
                {
                    NomeCliente = divida.NomeCliente,
                    ValorComissao = (divida.ValorDivida * _parametro.PercentualComissao) / 100,
                    ValorDivida = divida.ValorDivida,
                    ValorTotalComissao = _dividaCliente.Sum(div => (div.ValorDivida * _parametro.PercentualComissao) / 100)
                }).OrderByDescending(divida => divida.ValorComissao).ToList();
            }
            catch (Exception ex)
            {
                return new List<ComissaoViewModel>();
            }

            return _dividas;
        }
    }
}
