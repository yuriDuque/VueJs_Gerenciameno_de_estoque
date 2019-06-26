using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.Repository.Entities;

namespace Repository.Entities
{
    public class Produto
    {
        [Key]
        public int? IdProduto { get; internal set; }

        [Required]
        public int? CodInterno { get; set; }

        [Required]
        [MaxLength(13)]
        public int? CodBarras { get; set; }

        public string Descricao { get; set; }

        public double ValorVenda { get; set; }

        public List<PedidoProdutos> PedidoProdutos { get; set; } = new List<PedidoProdutos>();
    }


}
