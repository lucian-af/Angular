namespace APIWeb.Models
{
    public class Parametro
    {
        public int IdParametro { get; set; }
        public int QuantidadeParcelaMaxima { get; set; }
        public int TipoJuros { get; set; }
        public decimal PercentualJuros { get; set; }
        public decimal PercentualComissao { get; set; }
    }
}
