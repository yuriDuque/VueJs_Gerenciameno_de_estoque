using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repository.Entities
{
    public class PedidoProdutos
    {
        public int? IdPedido { get; set; }
        public Pedido Pedido { get; set; }
        public int? IdProduto { get; set; }
        public Produto Produto { get; set; }
    }
}
