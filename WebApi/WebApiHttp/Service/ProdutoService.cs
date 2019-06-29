using Repository.Entities;
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

            if(p.IdProduto == null && p.CodInterno != null && p.CodBarras != null)
            {
                var codInterno = repository.Get(x => x.CodInterno == p.CodInterno).FirstOrDefault();
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
            else if (p.IdProduto != null)
            {
                // está mantendo o produto "p" em memória e não está buscando no banco
                var oldProduto = repository.Find(p.IdProduto);

                if(p.CodInterno != oldProduto.CodInterno)
                {
                    var codInterno = repository.Get(x => x.CodInterno == p.CodInterno).FirstOrDefault();
                    if (codInterno != null)
                        throw new Exception("Não é possivel alterar um produto com um código duplicado!");
                }

                else if(p.CodBarras != oldProduto.CodBarras)
                {
                    var codBarras = repository.Get(x => x.CodBarras == p.CodBarras).FirstOrDefault();
                    if (codBarras != null)
                        throw new Exception("Não é possivel alterar um produto com um código duplicado!");
                }

                else
                {
                    repository.Update(p);
                    return p;
                }
            }

            return null;
        }

        public Produto BuscarPeloCodInterno(int? cod)
        {
            if(cod != null)
            {
                return repository.Get(x => x.CodInterno == cod).FirstOrDefault();
            }

            return new Produto();
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
