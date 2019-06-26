using System;
using System.Collections.Generic;
using Repository.Context;
using Repository.Entities;
using WebApi.Repository.EntitieRepository;
using WebApi.Repository.Entities;
using WebApi.Service;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new BaseContext())
            {

                var pService = new ProdutoService();

                var produto = new Produto()
                {
                    CodInterno = 01,
                    CodBarras = 01,
                    Descricao = "teste",
                    ValorVenda = 10
                };

                var produto2 = new Produto()
                {
                    CodInterno = 02,
                    CodBarras = 02,
                    Descricao = "teste 01",
                    ValorVenda = 5
                };

                var produto3 = new Produto()
                {
                    CodInterno = 03,
                    CodBarras = 03,
                    Descricao = "teste 03",
                    ValorVenda = 15
                };

                pService.SalvarProduto(produto);
                pService.SalvarProduto(produto2);
                pService.SalvarProduto(produto3);
                
                var produtos = pService.BuscarTodosOsProdutos();
                /*


                var produtoAlterado = new Produto()
                {
                    IdProduto = 01,
                    CodInterno = 02,
                    CodBarras = 02,
                    Descricao = "teste alterado",
                    ValorVenda = 15.5
                };

                //pService.AlterarProduto(produtoAlterado);

                var produtoBuscado = pService.BuscarPeloCodInterno(produto.CodInterno);

                pService.ExcluirProduto(produtoAlterado.IdProduto);

            
                */

                var pedidoService = new PedidoService();

                var pedido = new Pedido();

                var pedidosProdutos = new List<PedidoProdutos>();

                foreach(var p in produtos)
                {
                    var pd = new PedidoProdutos()
                    {
                        Pedido = pedido,
                        IdProduto = p.IdProduto
                    };

                    pedido.PedidoProdutos.Add(pd);
                }

                pedidoService.SalvarPedido(pedido);
            }
        }
    }
}
