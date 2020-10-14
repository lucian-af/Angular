using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIWeb.Models
{
    public class Divida
    {
        public int IdDivida { get; set; }
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime DataVencimento { get; set; }
        public int QuantidadeParcela { get; set; }
        public decimal ValorOriginal { get; set; }

        [NotMapped]
        public List<DividaParcela> DividaParcelas { get; set; }
    }
}
