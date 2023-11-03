using ContextBuilder.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.ContextModels;
using Models.FunctionModels;

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

        //receber os requests de outras aplicações
        [HttpPost]
        [Route("CreateResquest")]
        public async Task<ActionResult> CreateResquest([FromBody] Request request)
        {
            try
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
                Console.WriteLine("Request: " + request.Id.ToString() + " - Adicionado com Sucesso");
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine("Request: " + request.Id.ToString() + " - Erro ao adicionar");
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }

        //Receber os missingCompoentes
        [HttpPost]
        [Route("AddMissingComponent")]
        public async Task<ActionResult> AddMissingComponent([FromBody] MissingComponent missingComponente)
        {
            try
            {
                var mc = await _context.missingComponents.FirstOrDefaultAsync(m => m.LineId.Equals(missingComponente.LineId) && m.ComponentId.Equals(missingComponente.ComponentId));
                if(mc != null) 
                {
                    return BadRequest();
                }
                _context.Add(missingComponente);
                await _context.SaveChangesAsync();
                Console.WriteLine("MissingCoponente: LineId-" + missingComponente.LineId.ToString() + " , ComponenteId-" + missingComponente.ComponentId.ToString() + " - Adicionado com Sucesso");
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao adicionar missingComponente");
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("RemoveMissingComponent")]
        public async Task<ActionResult> RemoveMissingComponent([FromBody] MissingComponent missingComponente)
        {
            try
            {
                var mc = await _context.missingComponents.FirstOrDefaultAsync(m => m.LineId.Equals(missingComponente.LineId) && m.ComponentId.Equals(missingComponente.ComponentId));
                if (mc == null)
                {
                    return BadRequest();
                }
                _context.missingComponents.Remove(mc);
                await _context.SaveChangesAsync();
                Console.WriteLine("MissingCoponente: LineId-" + missingComponente.LineId.ToString() + " , ComponenteId-" + missingComponente.ComponentId.ToString() + " - removido com Sucesso");
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao adicionar missingComponente");
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }
    }    
}
