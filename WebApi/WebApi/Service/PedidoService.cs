﻿using Repository.Entities;
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

        public Pedido SalvarPedido(Pedido p)
        {
            if (p.IdPedido != null)
                throw new Exception("Não é possivel salvar um pedido vazio!");

            else if (p.PedidoProdutos != null && p.PedidoProdutos[0].IdProduto != null)
            {
                p.DataPedido = DateTime.Now;

                var rodutoRepository = new ProdutoRepository();

                foreach(var pd in p.PedidoProdutos)
                {
                    var produto = rodutoRepository.Find(pd.IdProduto);

                    p.ValorTotal += produto.ValorVenda;
                }

                var pedidoProdutos = p.PedidoProdutos;
                p.PedidoProdutos = new List<PedidoProdutos>();

                repository.Save(p);

                foreach(var pd in pedidoProdutos)
                {
                    pd.IdPedido = p.IdPedido;
                    p.PedidoProdutos.Add(pd);
                }

                repository.Update(p);

            }

            return null;
        }
    }
}
