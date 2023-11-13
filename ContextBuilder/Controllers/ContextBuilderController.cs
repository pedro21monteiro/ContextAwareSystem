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
        private static string AlertAppConnectionString = "https://192.168.28.86:8091/api/Alert/SendNotification/";
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
                //no final enviar o alerta
                await SendAlert(missingComponente);
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


        //Enviar avisos para o sistema de alarmes
        [HttpPost]
        [Route("SendAlert")]
        public async Task SendAlert(MissingComponent missingComponente)
        {
            var asr = new SendAlertRequest
            {
                MissingComponent = missingComponente,
                Message = "O componente " + missingComponente.ComponentId + "está a faltar na linha " + missingComponente.LineId + ", por favor efetuar a reposição.",
            };
            var alertHistory = new AlertsHistory
            {
                TypeOfAlet = 3,
                AlertDate = DateTime.Now,
                AlertMessage = asr.Message
            };
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync(AlertAppConnectionString, asr);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Alerta de componente em falta: CoponenteId - " + missingComponente.ComponentId +
                            " LineId- " + missingComponente.LineId + "Enviado com sucesso");
                        alertHistory.AlertSuccessfullySent = true;
                    }
                    else
                    {
                        Console.WriteLine("Alerta de componente em falta: CoponenteId - " + missingComponente.ComponentId +
                            " LineId- " + missingComponente.LineId + "Erro ao enviar Alerta");
                        alertHistory.AlertSuccessfullySent = false;
                    }
                    _context.Add(alertHistory);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                alertHistory.ErrorMessage = e.Message;
                alertHistory.AlertSuccessfullySent = false;
                _context.Add(alertHistory);
                await _context.SaveChangesAsync();
                Console.WriteLine("Alerta de componente em falta: CoponenteId - " + missingComponente.ComponentId +
                           " LineId- " + missingComponente.LineId + "Erro ao enviar Alerta");
                Console.WriteLine(e.Message);
            }
        }
    }    
}
