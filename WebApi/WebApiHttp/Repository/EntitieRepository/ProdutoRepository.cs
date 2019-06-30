using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Entities;

namespace WebApi.Repository.EntitieRepository
{
    public class ProdutoRepository : Repository<Produto>
    {
        BaseContext ctx = new BaseContext();

        public void UpdateProduto(Produto obj)
        {
            ctx.Entry(obj).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
