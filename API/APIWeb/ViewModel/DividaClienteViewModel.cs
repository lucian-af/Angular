using System;
using System.Collections.Generic;
using APIWeb.Models;

namespace APIWeb.ViewModel
{
    public class DividaClienteViewModel
    {
        public int IdDivida { get; set; }
        public string NomeCliente  { get; set; }
        public string DocumentoCliente  { get; set; }
        public DateTime DataVencimento { get; set; }
        public int QuantidadeParcela { get; set; }
        public decimal ValorOriginal { get; set; }
        public int DiasAtraso { get; set; }
        public decimal ValorJuros { get; set; }
        public decimal ValorDivida { get; set; }
        public List<DividaParcela> Parcelas { get; set; }

        public DividaClienteViewModel()
        {
            Parcelas = new List<DividaParcela>();
        }
    }
}
