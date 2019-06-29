using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using WebApi.Repository.Entities;

namespace Repository.Context
{
    public class BaseContext : DbContext
    {
        public BaseContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ServerLocal;Database=GerenciamentoProdutos;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PedidoProdutos>().HasKey(sc => new { sc.IdPedido, sc.IdProduto});

            modelBuilder.Entity<PedidoProdutos>()
                .HasOne(sc => sc.Pedido)
                .WithMany(s => s.PedidoProdutos)
                .HasForeignKey(sc => sc.IdPedido);


            modelBuilder.Entity<PedidoProdutos>()
                .HasOne(sc => sc.Produto)
                .WithMany(s => s.PedidoProdutos)
                .HasForeignKey(sc => sc.IdProduto);
        }

        public BaseContext(DbContextOptions<BaseContext> option)
            :base(option)
        { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
    }
}


// Criando Migrations 
// PM> add-migration GerenciamentoProdutosDb
// PM> update-database –verbose
