using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Entities;
using WebApi.Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PedidoService service = new PedidoService();

        // GET: api/Pedidos
        [HttpGet]
        public IEnumerable<Pedido> GetPedidos()
        {
            return service.BuscarTodosOsPedidos();
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoById([FromRoute] int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pedido = service.BuscarPedidoPeloId(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        // GET: api/PedidosData
        [Route("api/PedidosData")]
        [HttpGet]
        public async Task<IActionResult> GetPedidoByDate([FromBody] DateTime dataInicial, DateTime dataFinal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pedidos = service.BuscarPedidoPeloIntervalo(dataInicial, dataFinal);

            return CreatedAtAction("GetPedido", pedidos);
        }

        // POST: api/Pedidos
        [HttpPost]
        public async Task<IActionResult> PostPedido([FromBody] List<Produto> produtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(produtos.Count == 0)
            {
                return BadRequest(ModelState);
            }

            var pedidoResposta = service.SalvarPedido(produtos);

            return CreatedAtAction("GetPedido", new { id = pedidoResposta.IdPedido }, pedidoResposta);
        }
    }
}