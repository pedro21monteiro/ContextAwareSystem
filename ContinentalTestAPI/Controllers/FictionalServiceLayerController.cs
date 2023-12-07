using Microsoft.AspNetCore.Mvc;
using Models.FunctionModels;

namespace ContinentalTestAPI.Controllers
{
    [Route("api/ServiceLayer")]
    [ApiController]
    public class FictionalServiceLayerController : Controller
    {

        /// <summary>
        /// Endpoint HTTP POST para simular o serviço de notificações e alertas presente na camada de serviços.
        /// </summary>
        [HttpPost]
        [Route("SendNotification")]
        public async Task<ActionResult> SendNotification([FromBody] SendAlertRequest AlertRequest)
        {
            if(AlertRequest.MissingComponent == null && AlertRequest.Production == null && AlertRequest.Stop == null)
            {
                return BadRequest();
            }
            if(AlertRequest.MissingComponent != null)
            {
                Console.WriteLine($"Alerta de componente em falta: ComponenteId - {AlertRequest.MissingComponent.ComponentId} " +
                    $"LineId - {AlertRequest.MissingComponent.LineId} Recebido com Sucesso.");
                Console.WriteLine(AlertRequest.Message);
                return Ok();
            }
            if (AlertRequest.Stop != null)
            {
                Console.WriteLine($"Alerta de paragem urgente: Id - {AlertRequest.Stop.Id} Recebido com Sucesso.");
                Console.WriteLine(AlertRequest.Message);
                return Ok();
            }
            if (AlertRequest.Production != null)
            {
                Console.WriteLine($"Alerta de nova produção: Id - {AlertRequest.Production.Id} Recebido com Sucesso.");
                Console.WriteLine(AlertRequest.Message);
                return Ok();
            }
            return Ok();
        }
    }
}
