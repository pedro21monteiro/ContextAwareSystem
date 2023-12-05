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
        private static string AlertAppConnectionString = "https://localhost:7013/api/ServiceLayer/SendNotification/";
        private readonly IContextBuilderDb _context;

        public ContextBuilderController(IContextBuilderDb context)
        {
            _context = context;
        }

        /// <summary>
        /// Este microserviço recebe informações sobre um pedido, as quais são enviadas pela camada de serviços 
        /// sempre que um utilizador solicita visualizar informações, como gráficos, por exemplo. Estes pedidos
        /// serão armazenados na aplicação.
        /// </summary>
        [HttpPost]
        [Route("CreateResquest")]
        public async Task<ActionResult> CreateResquest([FromBody] Request request)
        {
            try
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Request: {request.Id} - Adicionado com Sucesso");
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Request: {request.Id} - Erro ao adicionar");
                Console.WriteLine($"Detalhes: {e.Message}");
                return BadRequest("Erro ao adicionar Request");
            }
        }

        /// <summary>
        /// Este microserviço recebe informações acerca de um componente em falta numa linha de produção. 
        /// Se os dados forem válidos, a aplicação enviará um aviso para o NAS e, posteriormente, adicionará 
        /// esse componente em falta à base de dados, caso ainda não esteja lá registado.
        /// </summary>
        [HttpPost]
        [Route("AddMissingComponent")]
        public async Task<ActionResult> AddMissingComponent([FromBody] MissingComponent missingComponente)
        {
            try
            {
                if(missingComponente.LineId <= 0 || missingComponente.ComponentId <= 0)
                {
                    return BadRequest();
                }
                await SendAlert(missingComponente);
                var mc = await _context.missingComponents
                    .FirstOrDefaultAsync(m => 
                    m.LineId == missingComponente.LineId && m.ComponentId == missingComponente.ComponentId);
                if (mc != null)
                {
                    Console.WriteLine($"O missingComponente já existe: {missingComponente.String()}.");
                    return BadRequest("MissingComponente já existe na base de dados.");
                }
                _context.Add(missingComponente);
                await _context.SaveChangesAsync();
                Console.WriteLine($"{missingComponente.String()} adicionado com sucesso.");
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao adicionar missingComponente: {missingComponente.String()}.");
                Console.WriteLine($"Detalhes: {e.Message}");
                return BadRequest("Erro ao adicionar MissingComponente.");
            }
        }

        /// <summary>
        /// Este microserviço recebe informações de um componente em falta numa linha de produção e o remove caso esteja 
        /// presente na base de dados. O objetivo deste endpoint é eliminar da base de dados o componente que estava em 
        /// falta na linha de produção após sua reposição.
        /// </summary>
        [HttpPost]
        [Route("RemoveMissingComponent")]
        public async Task<ActionResult> RemoveMissingComponent([FromBody] MissingComponent missingComponente)
        {
            try
            {
                var mc = await _context.missingComponents.FirstOrDefaultAsync(m => 
                m.LineId == missingComponente.LineId && m.ComponentId == missingComponente.ComponentId);

                if (mc == null)
                {
                    Console.WriteLine($"Não foi possível encontrar o missingComponente: {missingComponente.String()}.");
                    return BadRequest("MissingComponente não encontrado.");
                }

                _context.missingComponents.Remove(mc);
                await _context.SaveChangesAsync();

                Console.WriteLine($"{missingComponente.String()} removido com sucesso.");
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao remover missingComponente: {missingComponente.String()}.");
                Console.WriteLine($"Detalhes: {e.Message}");
                return BadRequest("Erro ao remover MissingComponente.");
            }
        }

        /// <summary>
        /// Este microsserviço vai ser encarregue de enviar avisos de componentes em falta para o serviço de notificações e alertas (NAS)
        /// </summary>
        [HttpPost]
        [Route("SendAlert")]
        public async Task SendAlert(MissingComponent missingComponente)
        {
            var asr = new SendAlertRequest
            {
                MissingComponent = missingComponente,
                Message = $"O componente {missingComponente.ComponentId} está a faltar na linha {missingComponente.LineId}, por favor efetuar a reposição.",
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
                    Console.WriteLine($"Alerta de componente em falta: ComponenteId - {missingComponente.ComponentId}, LineId - {missingComponente.LineId} enviado com sucesso");
                    alertHistory.AlertSuccessfullySent = true;
                }
                _context.Add(alertHistory);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar alerta: ComponenteId - {missingComponente.ComponentId}, LineId - {missingComponente.LineId}");
                Console.WriteLine($"Erro: {ex.Message}");
                alertHistory.ErrorMessage = ex.Message;
                alertHistory.AlertSuccessfullySent = false;
                _context.Add(alertHistory);
                await _context.SaveChangesAsync();
            }
        }
    }    
}
