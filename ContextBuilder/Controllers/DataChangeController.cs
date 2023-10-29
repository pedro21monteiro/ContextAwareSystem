using ContextBuilder.Data;
using Microsoft.AspNetCore.Mvc;
using Models.ContextModels;
using Models.JsonModels;

namespace ContextBuilder.Controllers
{
    [Route("api/DataChange")]
    [ApiController]
    public class DataChangeController : Controller
    {
        private readonly ContextBuilderDb _context;


        public DataChangeController(ContextBuilderDb context)
        {
            _context = context;
        }

        //-----------Funçoes CRUD da Base de dados da continental
        // o int operation tem a ver com a função que é para fazer, 1-create, 2-update, 3-delete para já ainda não recebe a dizer o que é para fazer
        // por isso tem de verificar
     
        //---------------Production
        [HttpPost]
        [Route("ChangeProduction")]
        public async Task<ActionResult> ChangeProduction([FromBody] Production production)
        {
            var pExistInContext = _context.Productions.SingleOrDefault(p => p.Id == production.Id);
            if (pExistInContext == null)
            {
                try
                {
                    _context.Add(production);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Production: " + production.Id.ToString() + " - Adicionado com suceso");
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
            else
            {
                //fazer update
                try
                {
                    pExistInContext.Production_PlanId = production.Production_PlanId;
                    pExistInContext.Hour = production.Hour;
                    pExistInContext.Day = production.Day;
                    pExistInContext.Quantity = production.Quantity;
                    _context.Update(pExistInContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Production: " + production.Id.ToString() + " - Atualizada com suceso");
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
        }
        //---------------Stop
        [HttpPost]
        [Route("ChangeStop")]
        public async Task<ActionResult> ChangeStop([FromBody] Stop stop)
        {
            var sExistInContext = _context.Stops.SingleOrDefault(s => s.Id == stop.Id);
            if (sExistInContext == null)
            {
                try
                {
                    _context.Add(stop);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("stop: " + stop.Id.ToString() + " - Adicionado com suceso");
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
            else
            {
                //fazer update
                try
                {
                    if (stop.ReasonId != null)
                    {
                        sExistInContext.ReasonId = stop.ReasonId;
                    }
                    sExistInContext.LineId = stop.LineId;
                    //o resto do stop
                    sExistInContext.Planned = stop.Planned;
                    sExistInContext.InitialDate = stop.InitialDate;
                    sExistInContext.EndDate = stop.EndDate;
                    sExistInContext.Duration = stop.Duration;
                    sExistInContext.Shift = stop.Shift;
                    _context.Update(sExistInContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("stop: " + stop.Id.ToString() + " - atualizado com suceso");
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
        }
    }
}
