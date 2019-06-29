using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using WebApi.Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoService service = new ProdutoService();

        // GET: api/Produtos
        [HttpGet]
        public IEnumerable<Produto> GetProdutos()
        {
            return service.BuscarTodosOsProdutos();
        }

        // POST: api/Produtos
        [HttpPost]
        public async Task<IActionResult> PostProduto(Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            service.SalvarProduto(produto);

            return CreatedAtAction("GetProduto", new { id = produto.IdProduto }, produto);
        }

        // PUT: api/Produtos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto([FromRoute] int? id, [FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produto.IdProduto)
            {
                return BadRequest();
            }

            produto.IdProduto = id;

            try
            {
                service.AlterarProduto(produto);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // GET: api/Produtos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProdutoById([FromRoute] int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var produto = service.BuscarPeloCodInterno(id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto([FromRoute] int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var produto = service.BuscarPeloCodInterno(id);

            if (produto == null)
            {
                return NotFound();
            }

            service.ExcluirProduto(id);

            return Ok(produto);
        }
    }
}