using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIWeb.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Documento { get; set; }
        public string Nome { get; set; }
        public bool EhAtivo { get; set; }
        public List<Divida> Dividas { get; set; }
    }
}
