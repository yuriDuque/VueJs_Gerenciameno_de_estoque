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
        private ProdutoRepository repository;

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
    }
}
