using ContextServer.Data;
using ContextServer.Services;
using Microsoft.AspNetCore.Mvc;
using Models.CustomModels;
using Models.FunctionModels;
using Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Context_aware_System.Controllers

{
    [Route("api/ContextAware")]
    [ApiController]
    public class ContextAwareController : Controller
    {

        private readonly ContextAwareDb _context;
        private readonly SystemLogic _systemLogic;
        //para experimentar os datetimes 2023-11-20T11:11:11Z

        public ContextAwareController(ContextAwareDb context, SystemLogic systemLogic)
        {
            _context = context;
            _systemLogic = systemLogic;
        }

        [HttpGet]
        [Route("DeviceInfo")]
        public async Task<IActionResult> DeviceInfo(int deviceId)
        {
            //Formato da resposta
            ResponseDeviceInfo rdi = new ResponseDeviceInfo();

            //encontrar o device
            var device = _context.Devices.Where(d => d.Id== deviceId).FirstOrDefault();
            if (device == null)
            {
                rdi.Message = "Erro ao identificar o Device!!";
                return NotFound(rdi);
            }
            //O line id já esta no device

            //ver qual o tipo do device se é 1- weareble dos operadors, os outros para já vao ser dos coordenadores, exe: 2-tablet
            //se for wearable
            //adicionar as listas necessárias para esta função

            
            //encontrar a linha
            var _line = _context.Lines.Where(l => l.Id == device.LineId)
                .Include(l=> l.Coordinator)
                .FirstOrDefault();
            if (_line == null)
            {
                rdi.Message = "Erro ao identificar a linha de produção!!";
                return NotFound(rdi);
            }
            rdi.line = _line;

            if (device.Type == 1)
            {

                rdi.Type = "Weareble-Operator";
                //ver o workshift do operador
                WorkShift ws = _systemLogic.GetAtualWorkShift(DateTime.Now);
                rdi.WorkShift = ws.Shift;
                rdi.WorkShiftString = ws.ShiftString;

                //encontrar o product reference
                Production_Plan production_Plan = new Production_Plan();
                var _production_plan = _context.Production_Plans.Where(p => p.LineId == _line.Id).ToList();
                if (_production_plan.Any())
                {
                    foreach (var productionPlan in _production_plan)
                    {
                        if (_systemLogic.dateTimeIsActiveNow(productionPlan.InitialDate, productionPlan.EndDate))
                        {
                            production_Plan = productionPlan;
                        }
                    }
                }

                if (production_Plan.Name == "")
                {
                    rdi.Message = "Erro ao identificar o plano de produção!!";
                    return NotFound(rdi);
                }
                
                //encontrar o product pois é lá que tem a a product reference
                var product = _context.Products.Where(p => p.Id == production_Plan.ProductId)
                    .Include(p=>p.Components)
                    .FirstOrDefault();
                if (product == null)
                {
                    rdi.Message = "Erro ao identificar o product!!";
                    return NotFound(rdi);
                }
                //aqui já temos a referencia do produto
                rdi.ProductName = product.Name;

                //Agora aqui vou ter de encher a lista de missing componentes que para já vou meter todos e encontrar a tag
                //para já vai ser os componentes do produto
                foreach(var component in product.Components)
                {
                    rdi.listMissingComponentes.Add(component);
                }
            }
            else
            {
                //se não for wearable vai ser operado por um coordenador
                rdi.Type = "Tablet/Outro-Coordenator";

                //encontrar o worker
                var coordinator = _context.Coordinators.Where(c => c.Id == _line.CoordinatorId)
                    .Include(c=>c.Worker)
                    .Include(c=>c.Lines)
                    .FirstOrDefault();
                if (coordinator == null)
                {
                    rdi.Message = "Erro ao identificar o worker!!";
                    return NotFound(rdi);
                }
                rdi.Coordinator = coordinator;

            }
            rdi.Message = "Info obtida com sucesso!!";
            return Ok(rdi);
        }

        [HttpGet]
        [Route("OperatorInfo")]
        public async Task<IActionResult> OperatorInfo(string OperatorIdFirebase)
        {
            //Formato da resposta
            ResponseOperatorInfo roi = new ResponseOperatorInfo();
            //verificar quem é o worker
            var worker = _context.Workers.Where(w => w.IdFirebase == OperatorIdFirebase).FirstOrDefault();
            
            if (worker == null)
            {
                roi.Message = "Erro ao identificar o worker!!";

                return NotFound(roi);
            }
            //encontrar o operator
            var ope = _context.Operators.Where(o => o.WorkerId == worker.Id)
                .Include(o => o.Worker)
                .FirstOrDefault();
            if (ope == null)
            {
                roi.Message = "Erro ao identificar o Operator!!";

                return NotFound(roi);
            }
            roi.Operator = ope;
            //verificar se esse operador esta a trabalhar no dia atual
            foreach (Schedule_Worker_Line swl in _context.Schedule_Worker_Lines.Include(s => s.Operator).ToList())
            {
                if(swl.Operator != null)
                {
                    if (swl.OperatorId == ope.Id && swl.Day.Date == DateTime.Now.Date)
                    {
                        roi.listSWL.Add(swl);
                    }
                }
            }
            //ver em quantas linhas esta a trabalhar
            
            foreach (Schedule_Worker_Line swl2 in roi.listSWL)
            {
                foreach (Line l in _context.Lines)
                {
                    if (l.Id == swl2.LineId)
                    {
                        if (!roi.listLine.Contains(l))
                        {
                            //adicionar so uma vez caso ele tenha mais que um horario numa linha
                            roi.listLine.Add(l);
                        }
                        
                    }
                }

            }
            int nLinhas = roi.listLine.Count;
            if (nLinhas == 0)
            {
                roi.Message = "O operador " + worker.UserName + " não está a trabalhar em nenhuma linha hoje";
            }
            else
            {
                roi.Message = "O operador " + worker.UserName + " está a trabalhar em " + nLinhas.ToString() + " linha hoje";
            }

            return Ok(roi);
        }

        [HttpGet]
        [Route("NewStopsInfo")]
        public async Task<IActionResult> NewStopsInfo(DateTime? dtInitial, DateTime? dtFinal, bool? planned)
        {
            ResponseNewStopsInfo rnsi = new ResponseNewStopsInfo();

            //ver as novas paragens
            rnsi.listNewStops = new List<Stop>();
            List<Stop> stops = new List<Stop>();
            if(planned == null)
            {
                stops = _context.Stops
                .Include(s => s.Reason)
                .ToList();
            }
            else
            {
                stops = _context.Stops.Where(s => s.Planned == planned)
                .Include(s => s.Reason)
                .ToList();
            }

            if (!stops.Any())
            {
                rnsi.Message = "Não existe paragens nessas datas";
                return NotFound(rnsi);
            }
            foreach (var stop in stops)
            {
                if(_systemLogic.IsAtributeInDatetime(dtInitial, dtFinal, stop.InitialDate, stop.EndDate) == true)
                {
                    rnsi.listNewStops.Add(stop);
                }
            }
            rnsi.Message = "Info obtida com sucesso!!";
            return Ok(rnsi);
        }

        [HttpGet]
        [Route("LineInfo")]
        public async Task<IActionResult> LineInfo(int LineId, DateTime? dtInitial, DateTime? dtFinal)
        {
            //Formato da resposta
            ResponseLineInfo rli = new ResponseLineInfo();

            var line = _context.Lines.Where(l => l.Id == LineId)
                .Include(l => l.Stops)
                .Include(l => l.Coordinator)
                .FirstOrDefault();
            //ver se no coordinator da a informação do worker
            if (line == null)
            {
                rli.Message = "Erro ao identificar a Line!!";

                return NotFound(rli);
            }
            rli.Coordinator = line.Coordinator;
            rli.Line = line;
            //Lista de stops
            var stops = _context.Stops.Where(s => s.LineId == line.Id).ToList();
            if (stops.Any())
            {
                rli.listStops = stops;
            }

            var _productions = _context.Production_Plans.Where(p => p.LineId == line.Id).ToList();

            var production_plan = _productions.Where(p => _systemLogic.IsAtributeInDatetime(p.InitialDate, p.EndDate, DateTime.Now) == true).FirstOrDefault();

            if (production_plan == null)
            {
                rli.Message = "Não existe planos de produção na linha no momento!!";
                return NotFound(rli);
            }
            //Encontrar as produções
            var productions = _context.Productions.Where(p => p.Production_PlanId == production_plan.Id).ToList();
            if (!productions.Any())
            {
                rli.Message = "Não existe produções nessa data!!";
            }
            //encontrar o product pois é lá que tem a a product reference
            var product = _context.Products.Where(p => p.Id == production_plan.ProductId).FirstOrDefault();

            if (product == null)
            {
                rli.Message = "Erro ao identificar a product!!";
                return NotFound(rli);
            }

            rli.listProductions = productions;
            rli.Product = product;
            rli.Message = "Info obtida com sucesso";
            return Ok(rli);
        }

        [HttpGet]
        [Route("SupervisorInfo")]
        public async Task<IActionResult> SupervisorInfo(string SupervisorIdFirebase, DateTime? Day)
        {
            //Formato da resposta
            ResponseSupervisorInfo rsi = new ResponseSupervisorInfo();
            var worker = _context.Workers.Where(w => w.IdFirebase == SupervisorIdFirebase).FirstOrDefault();

            if (worker == null)
            {
                rsi.Message = "Erro ao identificar o worker!!";

                return NotFound(rsi);
            }
            //encontrar o operator
            var sup = _context.Supervisors.Where(s => s.WorkerId == worker.Id)
                .Include(s => s.Worker)
                .FirstOrDefault();
            if (sup == null)
            {
                rsi.Message = "Erro ao identificar o Supervisor!!";

                return NotFound(rsi);
            }
            rsi.Supervisor = sup;
            //verificar se esse operador esta a trabalhar no dia atual
            DateTime DataPesquisa = new DateTime();
            if (Day.HasValue)
            {
                DataPesquisa = (DateTime)Day;
            }
            else
            {
                DataPesquisa = DateTime.Now;
            }
            foreach (Schedule_Worker_Line swl in _context.Schedule_Worker_Lines)
            {
                if (swl.Supervisor != null)
                {
                    if (swl.SupervisorId == sup.Id && swl.Day.Date == DataPesquisa.Date)
                    {
                        rsi.listSWL.Add(swl);
                    }
                }
            }
            
            //ver em quantas linhas esta a trabalhar
            foreach (Schedule_Worker_Line swl2 in rsi.listSWL)
            {
                foreach (Line l in _context.Lines)
                {
                    if (l.Id == swl2.LineId)
                    {
                        if (!rsi.listLine.Contains(l))
                        {
                            //adicionar so uma vez caso ele tenha mais que um horario numa linha
                            rsi.listLine.Add(l);
                        }
                    }
                }

            }
            int nLinhas = rsi.listLine.Count;
            if (nLinhas == 0)
            {
                rsi.Message = "O Supervisor " + worker.UserName + " não está a supervisionar nenhuma linha - Dia: " + DataPesquisa.Date.ToString();
            }
            else
            {
                rsi.Message = "O operador " + worker.UserName + " está a supervisionar " + nLinhas.ToString() + " linha(s) - Dia: " + DataPesquisa.Date.ToString();
            }
            return Ok(rsi);            
        }


        ////método que retorna a lista de produção da linha x no turno atual
        [HttpGet]
        [Route("GetProductionsInfo")]
        public async Task<IActionResult> GetProductionsInfo(int LineId, DateTime? dtInitial, DateTime? dtFinal)
        {
            ResponseGetProductionsInfo rgpi = new ResponseGetProductionsInfo();

            var listpordPlans = _context.Production_Plans.Where(p => p.LineId == LineId).ToList();
            if (listpordPlans.Any())
            {
                //se encontrar produções nessa linha vamos ver as produções da mesma
                //var listproductions = _context.Productions.Where(p=>p.Production_PlanId)
                foreach (var pp in listpordPlans)
                {
                    foreach(var production in _context.Productions)
                    {
                        if(production.Production_PlanId == pp.Id && _systemLogic.IsAtributeInDatetime(dtInitial,dtFinal,production.Day) == true)
                        {
                            rgpi.listProductions.Add(production);
                        }
                    }
                }
                rgpi.Message = "Info obtida com sucesso!!";
                return Ok(rgpi);
            }
            else
            {
                rgpi.Message = "Não encontrou produções nessa linha!!";
                return Ok(rgpi);
            }

        }


        ////método que retorna a lista de components a partir do id do device
        [HttpGet]
        [Route("GetComponentsDeviceInfo")]
        public async Task<IActionResult> GetComponentsDeviceInfo(int deviceId)
        {
            ResponseGetComponentsDeviceInfo rgcdi = new ResponseGetComponentsDeviceInfo();
            //encontrar o device
            var device = _context.Devices.Where(d => d.Id == deviceId).FirstOrDefault();
            if (device == null)
            {
                rgcdi.Message = "Erro ao identificar o Device!!";
                return NotFound(rgcdi);
            }
            var _line = _context.Lines.Where(l => l.Id == device.LineId).FirstOrDefault();
            if (_line == null)
            {
                rgcdi.Message = "Erro ao identificar a linha de produção!!";
                return NotFound(rgcdi);
            }
            var _productions = _context.Production_Plans.Where(p => p.LineId == _line.Id)
                .Include(p => p.Product)
                .ToList();

            var production = _productions.Where(p => _systemLogic.IsAtributeInDatetime(p.InitialDate, p.EndDate, DateTime.Now) == true).FirstOrDefault();
            
            if (production == null)
            {
                rgcdi.Message = "O device não se encontra em nenhuma linha no momento!!";
                return NotFound(rgcdi);
            }
            //encontrar o product pois é lá que tem a a product reference
            var product = _context.Products.Where(p => p.Id == production.ProductId)
                .Include(p => p.Components)
                .FirstOrDefault();
            if (product == null)
            {
                rgcdi.Message = "Erro ao identificar a product!!";
                return NotFound(rgcdi);
            }
            rgcdi.listComponents = product.Components.ToList();
            rgcdi.Message = "Info obtida com sucesso!!";
            return Ok(rgcdi);
        }

        [HttpGet]
        [Route("ProductInfo")]
        public async Task<IActionResult> ProductInfo(int LineId)
        {
            //Formato da resposta
            ResponseProductInfo rpi = new ResponseProductInfo();

            var line = _context.Lines.Where(l => l.Id == LineId).FirstOrDefault();
            //ver se no coordinator da a informação do worker
            if (line == null)
            {
                rpi.Message = "Erro ao identificar a Line!!";

                return NotFound(rpi);
            }

            var _productions = _context.Production_Plans.Where(p => p.LineId == line.Id).ToList();
            var production_plan = _productions.Where(p => _systemLogic.IsAtributeInDatetime(p.InitialDate, p.EndDate, DateTime.Now) == true).FirstOrDefault();

            if (production_plan == null)
            {
                rpi.Message = "Não existe planos de produção na linha no momento!!";
                return NotFound(rpi);
            }
            //Encontrar as produções
            var product = _context.Products.Where(p => p.Id == production_plan.ProductId).FirstOrDefault();
            if (product == null)
            {
                rpi.Message = "Erro ao identificar a product!!";
                return NotFound(rpi);
            }
            rpi.Product = product;
            rpi.Message = "Info obtida com sucesso";
            return Ok(rpi);
        }

        [HttpGet]
        [Route("CoordinatorInfo")]
        public async Task<IActionResult> CoordinatorInfo(string WorkerIdFirebase)
        {
            //Formato da resposta
            ResponseCoordinatorInfo rci = new ResponseCoordinatorInfo();
            var worker = _context.Workers.Where(w => w.IdFirebase == WorkerIdFirebase).FirstOrDefault();

            if (worker == null)
            {
                rci.Message = "Erro ao identificar o worker!!";

                return NotFound(rci);
            }
            //encontrar o operator
            var coord = _context.Coordinators.Where(c => c.WorkerId == worker.Id)
                .Include(c => c.Worker)
                .Include(c => c.Lines)
                .FirstOrDefault();
            if (coord == null)
            {
                rci.Message = "Erro ao identificar o Supervisor!!";

                return NotFound(rci);
            }
            rci.Coordinator = coord;
            rci.listLine = coord.Lines.ToList();
            
            return Ok(rci);
        }
    }
}
