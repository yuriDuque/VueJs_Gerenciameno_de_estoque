﻿using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Repository.EntitieRepository;

namespace WebApi.Service
{
    public class ProdutoService
    {
        private ProdutoRepository repository = new ProdutoRepository();

        public IEnumerable<Produto> BuscarTodosOsProdutos()
        {
            return repository.GetAll();
        }

        public Produto SalvarProduto(Produto p)
        {
            if (p == null)
                throw new Exception("Não é possivel salvar um produto vazio!");

            if(p.IdProduto == 0 && p.CodInterno != 0 && p.CodBarras != 0)
            {
                //Busca se existe algum produto cadastrado com um codInterno que será cadastrado
                var codInterno = repository.Get(x => x.CodInterno == p.CodInterno).FirstOrDefault();

                //Busca se existe algum produto cadastrado com um codBarras que será cadastrado
                var codBarras = repository.Get(x => x.CodBarras == p.CodBarras).FirstOrDefault();

                if(codInterno != null)
                    throw new Exception("Não é possivel salvar um produto com um código duplicado!");
                else if (codBarras != null)
                    throw new Exception("Não é possivel salvar um produto com um código duplicado!");
                else
                {
                    repository.Save(p);
                    return repository.Get(x => x.CodInterno == p.CodInterno).FirstOrDefault();
                }
            }

            return null;
        }

        public Produto AlterarProduto(Produto p)
        {
            if (p == null)
                throw new Exception("Não é possivel alterar um produto vazio!");
            else if (p.IdProduto != 0)
            {
                // está mantendo o produto "p" em memória e não está buscando no banco
                var oldProduto = repository.Find(p.IdProduto);

                //Busca se existe algum produto cadastrado com um codBarras que será cadastrado
                if (p.CodInterno != oldProduto.CodInterno)
                {
                    var codInterno = repository.Get(x => x.CodInterno == p.CodInterno).FirstOrDefault();
                    if (codInterno != null)
                        throw new Exception("Não é possivel alterar um produto com um código duplicado!");
                }
                //Busca se existe algum produto cadastrado com um codBarras que será cadastrado
                else if (p.CodBarras != oldProduto.CodBarras)
                {
                    var codBarras = repository.Get(x => x.CodBarras == p.CodBarras).FirstOrDefault();
                    if (codBarras != null)
                        throw new Exception("Não é possivel alterar um produto com um código duplicado!");
                }

                else
                {
                    repository.UpdateProduto(p);
                    return p;
                }
            }

            return null;
        }

        public Produto BuscarPeloCodInterno(int cod)
        {
            if(cod != 0)
            {
                return repository.Get(x => x.CodInterno == cod).FirstOrDefault();
            }

            return new Produto();
        }

        public Produto BuscarPeloId(int id)
        {
            if(id != 0)
            {
                return repository.Find(id);
            }
            return null;
        }

        public bool ExcluirProduto(int? id)
        {
            if(id != null)
            {
                var pdRepository = new PedidoProdutosRepository();

                var pd = repository.Find(id);

                if (pd != null)
                {
                    repository.Delete(x => x.IdProduto == id);
                    return true;
                }
            }

            return false;
        }
    }
}
