using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.Repository.Entities;

namespace Repository.Entities
{
    public class Pedido
    {
        [Key]
        public int? IdPedido { get; internal set; }

        public DateTime DataPedido { get; set; }

        public double ValorTotal { get; set; }

        [Required]
        public ICollection<PedidoProdutos> PedidoProdutos { get; set; } = new List<PedidoProdutos>();
    }
}
