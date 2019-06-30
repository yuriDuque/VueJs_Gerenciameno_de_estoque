using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Repository.EntitieRepository;
using WebApi.Repository.Entities;

namespace WebApi.Service
{
    public class PedidoService
    {
        private PedidoRepository repository = new PedidoRepository();
        private ProdutoService produtoService = new ProdutoService();

        public Pedido SalvarPedido(List<Produto> produtos)
        {
            if (produtos == null)
                throw new Exception("Não é possivel salvar um pedido vazio!");

            else if (produtos[0].CodInterno != 0)
            {                
                var pedido = new Pedido()
                {
                    PedidoProdutos = new List<PedidoProdutos>()
                };
                pedido.DataPedido = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

                foreach (var p in produtos)
                { 
                    var produto = produtoService.BuscarPeloCodInterno(p.CodInterno);
                    pedido.ValorTotal += produto.ValorVenda;
                    var pd = new PedidoProdutos()
                    {
                        IdProduto = produto.IdProduto
                    };
                    pedido.PedidoProdutos.Add(pd);
                }

                var pedidoProdutos = pedido.PedidoProdutos;
                pedido.PedidoProdutos = new List<PedidoProdutos>();

                repository.Save(pedido);

                foreach(var pd in pedidoProdutos)
                {
                    pd.IdPedido = pedido.IdPedido;
                    pedido.PedidoProdutos.Add(pd);
                }

                repository.Update(pedido);
            }

            return null;
        }

        public IQueryable<Pedido> BuscarTodosOsPedidos()
        {
            var teste = repository.GetAll();
            return repository.GetAll();
        }

        public Pedido BuscarPedidoPeloId(int? id)
        {
            var pedido = repository.Find(id);

            var produtoRepository = new ProdutoRepository();

            for(var i = 0; i < pedido.PedidoProdutos.Count; i++)
            {
                pedido.PedidoProdutos[i].Produto = produtoRepository.Find(pedido.PedidoProdutos[i].IdProduto);
            }

            return pedido;
        }

        public List<Pedido> BuscarPedidoPeloIntervalo(DateTime dataInicial, DateTime dataFinal)
        {
            if (dataInicial != null && dataInicial != new DateTime())
                if (dataFinal != null && dataFinal != new DateTime())
                    return (List<Pedido>) repository.Get(x => x.DataPedido >= dataInicial && x.DataPedido <= dataFinal);

            return null;
        }

    }
}
