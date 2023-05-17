using ContextBuilder.Data;
using Microsoft.AspNetCore.Mvc;
using Models.ContextModels;

namespace ContextBuilder.Controllers
{
    [Route("api/ContextBuilder")]
    [ApiController]

    public class ContextBuilderController : Controller
    {
        private readonly ContextBuilderDb _context;

        public ContextBuilderController(ContextBuilderDb context)
        {
            _context = context;
        }

        //https://localhost:7284/swagger/index.html



        [HttpPost]
        [Route("CreateResquest")]
        public async Task<ActionResult> CreateResquest([FromBody] Request request)
        {
            var wor = _context.Workers.SingleOrDefault(w => w.Id == request.WorkerId);
            if (wor != null)
            {
                request.Worker = wor;
                request.WorkerId = wor.Id;
                try
                {
                    _context.Add(request);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Request: " + request.Id.ToString() + " - Adicionado com Sucesso");
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Request: " + request.Id.ToString() + " - Erro ao adicionar");
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }

            }
            else
            {
                Console.WriteLine("Request: " + request.Id.ToString() + " - Erro ao adicionar");
                return BadRequest();                
            }

            //_context.Clientes.Add(cliente);
            //await _context.SaveChangesAsync();
            //return CreatedAtRouteResult(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }
    }
}
