using System;
using Repository.Context;
using Repository.Entities;
using WebApi.Repository.EntitieRepository;
using WebApi.Repository.Entities;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new BaseContext())
            {
                var pRepository = new ProdutoRepository();

                var produto = new Produto()
                {
                    CodInterno = 01,
                    CodBarras = 01,
                    Descricao = "teste",
                    ValorVenda = 10.5
                };

                pRepository.Save(produto);

                var produtos = pRepository.GetAll();




                var pedidoRepository = new PedidoRepository();

                var pedido = new Pedido();
                pedido.DataPedido = DateTime.Now;
                pedido.ValorTotal = 0;

                foreach (var p in produtos)
                {
                    pedido.ValorTotal += p.ValorVenda;
                }

                pedidoRepository.Save(pedido);




                var pdRepository = new PedidoProdutosRepository();

                foreach(var p in produtos)
                {
                    var pedidoProduto = new PedidoProdutos()
                    {
                        IdPedido = pedido.IdPedido,
                        IdProduto = p.IdProduto
                    };

                    pdRepository.Save(pedidoProduto);
                }




                produtos = pRepository.GetAll();
                var pedidos = pedidoRepository.GetAll();
                var pds = pdRepository.GetAll();


            }
        }
    }
}
