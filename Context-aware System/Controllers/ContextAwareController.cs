using ContextServer.Data;
using ContextServer.Services;
using Microsoft.AspNetCore.Mvc;
using Models.CustomModels;
using Models.FunctionModels;
using Microsoft.EntityFrameworkCore;
using Models.ContextModels;
using Context_aware_System.Data;

namespace Context_aware_System.Controllers

{
    [Route("api/ContextAware")]
    [ApiController]

    public class ContextAwareController : Controller
    {
        private readonly IContextAwareDb _context;
        private readonly SystemLogic _systemLogic;
        private readonly DataService _DataService;
        //para experimentar os datetimes 2023-11-20T11:11:11Z

        public ContextAwareController(IContextAwareDb context, SystemLogic systemLogic, DataService dataService)
        {
            _context = context;
            _systemLogic = systemLogic;
            _DataService = dataService;
        }

        //-----------------------------------Device Info Antigo e corrigido---------------------------

        //[HttpGet]
        //[Route("DeviceInfo")]
        //public async Task<IActionResult> DeviceInfo(int deviceId)
        //{
        //    //Formato da resposta
        //    ResponseDeviceInfo rdi = new ResponseDeviceInfo();

        //    //encontrar o device
        //    var device = _context.Devices.Where(d => d.Id == deviceId).FirstOrDefault();
        //    if (device == null)
        //    {
        //        rdi.Message = "Erro ao identificar o Device!!";
        //        return NotFound(rdi);
        //    }
        //    //O line id já esta no device

        //    //ver qual o tipo do device se é 1- weareble dos operadors, os outros para já vao ser dos coordenadores, exe: 2-tablet
        //    //se for wearable
        //    //adicionar as listas necessárias para esta função

        //    //encontrar a linha
        //    var _line = _context.Lines.Where(l => l.Id == device.LineId)
        //        .Include(l => l.Coordinator)
        //        .FirstOrDefault();
        //    if (_line == null)
        //    {
        //        rdi.Message = "Erro ao identificar a linha de produção!!";
        //        return NotFound(rdi);
        //    }
        //    rdi.line = _line;

        //    if (device.Type == 1)
        //    {

        //        rdi.Type = "Weareble-Operator";
        //        //ver o workshift do operador
        //        WorkShift ws = _systemLogic.GetAtualWorkShift(DateTime.Now);
        //        rdi.WorkShift = ws.Shift;
        //        rdi.WorkShiftString = ws.ShiftString;

        //        //encontrar o product reference
        //        Production_Plan production_Plan = new Production_Plan();
        //        var _production_plan = _context.Production_Plans.Where(p => p.LineId == _line.Id).ToList();
        //        if (_production_plan.Any())
        //        {
        //            foreach (var productionPlan in _production_plan)
        //            {
        //                if (_systemLogic.dateTimeIsActiveNow(productionPlan.InitialDate, productionPlan.EndDate))
        //                {
        //                    production_Plan = productionPlan;
        //                }
        //            }
        //        }

        //        if (production_Plan.Name == "")
        //        {
        //            rdi.Message = "Erro ao identificar o plano de produção!!";
        //            return NotFound(rdi);
        //        }

        //        //encontrar o product pois é lá que tem a a product reference
        //        var product = _context.Products.Where(p => p.Id == production_Plan.ProductId)
        //            .Include(p => p.ComponentProducts)
        //            .FirstOrDefault();
        //        if (product == null)
        //        {
        //            rdi.Message = "Erro ao identificar o product!!";
        //            return NotFound(rdi);
        //        }
        //        //aqui já temos a referencia do produto
        //        rdi.ProductName = product.Name;

        //        //Agora aqui vou ter de encher a lista de missing componentes que para já vou meter todos e encontrar a tag
        //        //para já vai ser os componentes do produto


        //        foreach (var cp in product.ComponentProducts)
        //        {
        //            var component = _context.Components.FirstOrDefault(c => c.Id == cp.ComponentId);
        //            if(component != null)
        //            {
        //                rdi.listMissingComponentes.Add(component);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //se não for wearable vai ser operado por um coordenador
        //        rdi.Type = "Tablet/Outro-Coordenator";

        //        //encontrar o worker
        //        var coordinator = _context.Coordinators.Where(c => c.Id == _line.CoordinatorId)
        //            .Include(c => c.Worker)
        //            .Include(c => c.Lines)
        //            .FirstOrDefault();
        //        if (coordinator == null)
        //        {
        //            rdi.Message = "Erro ao identificar o worker!!";
        //            return NotFound(rdi);
        //        }
        //        rdi.Coordinator = coordinator;

        //    }
        //    rdi.Message = "Info obtida com sucesso!!";
        //    return Ok(rdi);
        //}
        //[HttpGet]
        //[Route("DeviceInfo")]
        //public async Task<IActionResult> DeviceInfo(int deviceId)
        //{
        //    //Formato da resposta
        //    ResponseDeviceInfo rdi = new ResponseDeviceInfo();
        //    //encontrar o device
        //    var device = _context.Devices.Where(d => d.Id == deviceId).FirstOrDefault();
        //    if (device == null)
        //    {
        //        rdi.Message = "Erro ao identificar o Device!!";
        //        return NotFound(rdi);
        //    }
        //    //O line id já esta no device

        //    //ver qual o tipo do device se é 1- weareble dos operadors, os outros para já vao ser dos coordenadores, exe: 2-tablet
        //    //se for wearable
        //    //adicionar as listas necessárias para esta função

        //    //encontrar a linha
        //    var _line = _context.Lines.Where(l => l.Id == device.LineId)
        //        .Include(l => l.Coordinator)
        //        .FirstOrDefault();
        //    if (_line == null)
        //    {
        //        rdi.Message = "Erro ao identificar a linha de produção!!";
        //        return NotFound(rdi);
        //    }
        //    rdi.line = _line;

        //    if (device.Type == 1)
        //    {

        //        rdi.Type = "Weareble-Operator";
        //        //ver o workshift do operador
        //        WorkShift ws = _systemLogic.GetAtualWorkShift(DateTime.Now);
        //        rdi.WorkShift = ws.Shift;
        //        rdi.WorkShiftString = ws.ShiftString;

        //        //encontrar o product reference
        //        Production_Plan production_Plan = new Production_Plan();
        //        var _production_plan = _context.Production_Plans.Where(p => p.LineId == _line.Id).ToList();
        //        if (_production_plan.Any())
        //        {
        //            foreach (var productionPlan in _production_plan)
        //            {
        //                if (_systemLogic.dateTimeIsActiveNow(productionPlan.InitialDate, productionPlan.EndDate))
        //                {
        //                    production_Plan = productionPlan;
        //                }
        //            }
        //        }

        //        if (production_Plan.Name == "")
        //        {
        //            rdi.Message = "Erro ao identificar o plano de produção!!";
        //            return NotFound(rdi);
        //        }

        //        //encontrar o product pois é lá que tem a a product reference
        //        var product = _context.Products.Where(p => p.Id == production_Plan.ProductId)
        //            .Include(p => p.ComponentProducts)
        //            .FirstOrDefault();
        //        if (product == null)
        //        {
        //            rdi.Message = "Erro ao identificar o product!!";
        //            return NotFound(rdi);
        //        }
        //        //aqui já temos a referencia do produto
        //        rdi.ProductName = product.Name;

        //        //Agora aqui vou ter de encher a lista de missing componentes que para já vou meter todos e encontrar a tag
        //        //para já vai ser os componentes do produto


        //        foreach (var cp in product.ComponentProducts)
        //        {
        //            var component = _context.Components.FirstOrDefault(c => c.Id == cp.ComponentId);
        //            if (component != null)
        //            {
        //                rdi.listMissingComponentes.Add(component);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //se não for wearable vai ser operado por um coordenador
        //        rdi.Type = "Tablet/Outro-Coordenator";

        //        //encontrar o worker
        //        var coordinator = _context.Coordinators.Where(c => c.Id == _line.CoordinatorId)
        //            .Include(c => c.Worker)
        //            .Include(c => c.Lines)
        //            .FirstOrDefault();
        //        if (coordinator == null)
        //        {
        //            rdi.Message = "Erro ao identificar o worker!!";
        //            return NotFound(rdi);
        //        }
        //        rdi.Coordinator = coordinator;

        //    }
        //    rdi.Message = "Info obtida com sucesso!!";
        //    return Ok(rdi);
        //}
        ////---------------

        [HttpGet]
        [Route("OperatorInfo")]
        public async Task<IActionResult> OperatorInfo(string OperatorIdFirebase)
        {
            //Formato da resposta
            ResponseOperatorInfo roi = new ResponseOperatorInfo();
            //verificar quem é o worker

            var worker = _DataService.GetWorkersByIdFirebase(OperatorIdFirebase).Result.FirstOrDefault();                
            if (worker == null)
            {
                roi.Message = "Erro ao identificar o worker!!";

                return NotFound(roi);
            }
            //adicionar o worker ao formato de resposta
            roi.Worker = worker;
            //encontrar o operator
            var ope = _DataService.GetOperatorsByWorkerId(worker.Id).Result.FirstOrDefault();
            if (ope == null)
            {
                roi.Message = "Erro ao identificar o Operator!!";

                return NotFound(roi);
            }
            roi.Operator = ope;
            //verificar se esse operador esta a trabalhar no dia atual
            var listSchedules = _DataService.GetSchedulesByOperatorId(ope.Id).Result;
            if(listSchedules != null)
            {
                foreach (Schedule_Worker_Line swl in listSchedules.ToList())
                {
                    if (swl.Day.Date == DateTime.Now.Date)
                    {
                        roi.listSWL.Add(swl);
                    }
                }
            }
            //ver em quantas linhas esta a trabalhar no dia atual e guardar numa lista de inteiros para poupar pedidos efetuados à camada de integração
            List<int> listIntLineIds = new List<int>();
            foreach (Schedule_Worker_Line swl2 in roi.listSWL)
            {
                if (!listIntLineIds.Contains(swl2.LineId))
                {
                    listIntLineIds.Add(swl2.LineId);
                }
            }
            //Ir à lista de inteiros que contem os ids das linhas que o operador está a trabalhar e adicionar
            foreach (int a in listIntLineIds)
            {
                var line = _DataService.GetLinesById(a).Result.FirstOrDefault();
                if (line != null)
                {
                    if (!roi.listLine.Contains(line))
                    {
                        //adicionar so uma vez caso ele tenha mais uma linha
                        roi.listLine.Add(line);
                    }
                }
            }
            //--
            int nLinhas = roi.listLine.Count;
            roi.Message = "O operador " + worker.UserName + " está a trabalhar em " + nLinhas.ToString() + " linha hoje";

            return Ok(roi);
        }

        //[HttpGet]
        //[Route("NewStopsInfo")]
        //public async Task<IActionResult> NewStopsInfo(DateTime? dtInitial, DateTime? dtFinal, bool? planned)
        //{
        //    ResponseNewStopsInfo rnsi = new ResponseNewStopsInfo();

        //    //ver as novas paragens
        //    rnsi.listNewStops = new List<Stop>();
        //    List<Stop> stops = new List<Stop>();
        //    if(planned == null)
        //    {
        //        stops = _context.Stops
        //        .Include(s => s.Reason)
        //        .ToList();
        //    }
        //    else
        //    {
        //        stops = _context.Stops.Where(s => s.Planned == planned)
        //        .Include(s => s.Reason)
        //        .ToList();
        //    }

        //    if (!stops.Any())
        //    {
        //        rnsi.Message = "Não existe paragens nessas datas!!";
        //        return NotFound(rnsi);
        //    }
        //    foreach (var stop in stops)
        //    {
        //        if(_systemLogic.IsAtributeInDatetime(dtInitial, dtFinal, stop.InitialDate, stop.EndDate) == true)
        //        {
        //            rnsi.listNewStops.Add(stop);
        //        }
        //    }
        //    if(rnsi.listNewStops.Any())
        //    {
        //        rnsi.Message = "Info obtida com sucesso!!";
        //        return Ok(rnsi);
        //    }
        //    else
        //    {
        //        rnsi.Message = "Não existe paragens nessas datas!!";
        //        return NotFound(rnsi);
        //    }

        //}

        //[HttpGet]
        //[Route("LineInfo")]
        //public async Task<IActionResult> LineInfo(int LineId, DateTime? dtInitial, DateTime? dtFinal)
        //{
        //    //Formato da resposta
        //    ResponseLineInfo rli = new ResponseLineInfo();

        //    var line = _context.Lines.Where(l => l.Id == LineId)
        //        .Include(l => l.Stops)
        //        .Include(l => l.Coordinator)
        //        .FirstOrDefault();
        //    //ver se no coordinator da a informação do worker
        //    if (line == null)
        //    {
        //        rli.Message = "Erro ao identificar a Line!!";

        //        return NotFound(rli);
        //    }
        //    rli.Coordinator = line.Coordinator;
        //    rli.Line = line;
        //    //Lista de stops
        //    var stops = _context.Stops.Where(s => s.LineId == line.Id).ToList();
        //    if (stops.Any())
        //    {
        //        rli.listStops = stops;
        //    }

        //    var _productions = _context.Production_Plans.Where(p => p.LineId == line.Id).ToList();

        //    var production_plan = _productions.Where(p => _systemLogic.IsAtributeInDatetime(p.InitialDate, p.EndDate, DateTime.Now) == true).FirstOrDefault();

        //    if (production_plan == null)
        //    {
        //        rli.Message = "Não existe planos de produção na linha no momento!!";
        //        return NotFound(rli);
        //    }
        //    //Encontrar as produções
        //    var productions = _context.Productions.Where(p => p.Production_PlanId == production_plan.Id).ToList();
        //    if (!productions.Any())
        //    {
        //        rli.Message = "Não existe produções nessa data!!";
        //    }
        //    //encontrar o product pois é lá que tem a a product reference
        //    var product = _context.Products.Where(p => p.Id == production_plan.ProductId).FirstOrDefault();

        //    if (product == null)
        //    {
        //        rli.Message = "Erro ao identificar a product!!";
        //        return NotFound(rli);
        //    }

        //    rli.listProductions = productions;
        //    rli.Product = product;
        //    rli.Message = "Info obtida com sucesso";
        //    return Ok(rli);
        //}

        //[HttpGet]
        //[Route("SupervisorInfo")]
        //public async Task<IActionResult> SupervisorInfo(string SupervisorIdFirebase, DateTime? Day)
        //{
        //    //Formato da resposta
        //    ResponseSupervisorInfo rsi = new ResponseSupervisorInfo();
        //    var worker = _context.Workers.Where(w => w.IdFirebase == SupervisorIdFirebase).FirstOrDefault();

        //    if (worker == null)
        //    {
        //        rsi.Message = "Erro ao identificar o worker!!";

        //        return NotFound(rsi);
        //    }
        //    //encontrar o operator
        //    var sup = _context.Supervisors.Where(s => s.WorkerId == worker.Id)
        //        .Include(s => s.Worker)
        //        .FirstOrDefault();
        //    if (sup == null)
        //    {
        //        rsi.Message = "Erro ao identificar o Supervisor!!";

        //        return NotFound(rsi);
        //    }
        //    rsi.Supervisor = sup;
        //    //verificar se esse operador esta a trabalhar no dia atual
        //    DateTime DataPesquisa = new DateTime();
        //    if (Day.HasValue)
        //    {
        //        DataPesquisa = (DateTime)Day;
        //    }
        //    else
        //    {
        //        DataPesquisa = DateTime.Now;
        //    }
        //    foreach (Schedule_Worker_Line swl in _context.Schedule_Worker_Lines)
        //    {
        //        if (swl.Supervisor != null)
        //        {
        //            if (swl.SupervisorId == sup.Id && swl.Day.Date == DataPesquisa.Date)
        //            {
        //                rsi.listSWL.Add(swl);
        //            }
        //        }
        //    }

        //    //ver em quantas linhas esta a trabalhar
        //    foreach (Schedule_Worker_Line swl2 in rsi.listSWL)
        //    {
        //        foreach (Line l in _context.Lines)
        //        {
        //            if (l.Id == swl2.LineId)
        //            {
        //                if (!rsi.listLine.Contains(l))
        //                {
        //                    //adicionar so uma vez caso ele tenha mais que um horario numa linha
        //                    rsi.listLine.Add(l);
        //                }
        //            }
        //        }

        //    }
        //    int nLinhas = rsi.listLine.Count;
        //    if (nLinhas == 0)
        //    {
        //        rsi.Message = "O Supervisor " + worker.UserName + " não está a supervisionar nenhuma linha - Dia: " + DataPesquisa.Date.ToString();
        //    }
        //    else
        //    {
        //        rsi.Message = "O operador " + worker.UserName + " está a supervisionar " + nLinhas.ToString() + " linha(s) - Dia: " + DataPesquisa.Date.ToString();
        //    }
        //    return Ok(rsi);            
        //}


        //////método que retorna a lista de produção da linha x no turno atual
        //[HttpGet]
        //[Route("GetProductionsInfo")]
        //public async Task<IActionResult> GetProductionsInfo(int LineId, DateTime? dtInitial, DateTime? dtFinal)
        //{
        //    ResponseGetProductionsInfo rgpi = new ResponseGetProductionsInfo();

        //    var listpordPlans = _context.Production_Plans.Where(p => p.LineId == LineId).ToList();
        //    if (listpordPlans.Any())
        //    {
        //        //se encontrar produções nessa linha vamos ver as produções da mesma
        //        //var listproductions = _context.Productions.Where(p=>p.Production_PlanId)
        //        foreach (var pp in listpordPlans)
        //        {
        //            foreach(var production in _context.Productions.Include(p=>p.Prod_Plan).ThenInclude(x=>x.Product))
        //            {
        //                if(production.Production_PlanId == pp.Id && _systemLogic.IsAtributeInDatetime(dtInitial,dtFinal,production.Day) == true)
        //                {
        //                    rgpi.listProductions.Add(production);
        //                }
        //            }
        //        }
        //        rgpi.Message = "Info obtida com sucesso!!";
        //        return Ok(rgpi);
        //    }
        //    else
        //    {
        //        rgpi.Message = "Não encontrou produções nessa linha!!";
        //        return Ok(rgpi);
        //    }

        //}


        //////método que retorna a lista de components a partir do id do device
        //[HttpGet]
        //[Route("GetComponentsDeviceInfo")]
        //public async Task<IActionResult> GetComponentsDeviceInfo(int deviceId)
        //{
        //    ResponseGetComponentsDeviceInfo rgcdi = new ResponseGetComponentsDeviceInfo();
        //    //encontrar o device
        //    var device = _context.Devices.Where(d => d.Id == deviceId).FirstOrDefault();
        //    if (device == null)
        //    {
        //        rgcdi.Message = "Erro ao identificar o Device!!";
        //        return NotFound(rgcdi);
        //    }
        //    var _line = _context.Lines.Where(l => l.Id == device.LineId).FirstOrDefault();
        //    if (_line == null)
        //    {
        //        rgcdi.Message = "Erro ao identificar a linha de produção!!";
        //        return NotFound(rgcdi);
        //    }
        //    var _productions = _context.Production_Plans.Where(p => p.LineId == _line.Id)
        //        .Include(p => p.Product)
        //        .ToList();

        //    var production = _productions.Where(p => _systemLogic.IsAtributeInDatetime(p.InitialDate, p.EndDate, DateTime.Now) == true).FirstOrDefault();

        //    if (production == null)
        //    {
        //        rgcdi.Message = "O device não se encontra em nenhuma linha no momento!!";
        //        return NotFound(rgcdi);
        //    }
        //    //encontrar o product pois é lá que tem a a product reference
        //    var product = _context.Products.Where(p => p.Id == production.ProductId)
        //        .Include(p => p.ComponentProducts)
        //        .FirstOrDefault();
        //    if (product == null)
        //    {
        //        rgcdi.Message = "Erro ao identificar a product!!";
        //        return NotFound(rgcdi);
        //    }
        //    foreach (var cp in product.ComponentProducts)
        //    {
        //        var component = _context.Components.FirstOrDefault(c => c.Id == cp.ComponentId);
        //        if (component != null)
        //        {
        //            rgcdi.listComponents.Add(component);
        //        }
        //    }
        //    rgcdi.Message = "Info obtida com sucesso!!";
        //    return Ok(rgcdi);
        //}

        //[HttpGet]
        //[Route("ProductInfo")]
        //public async Task<IActionResult> ProductInfo(int LineId)
        //{
        //    //Formato da resposta
        //    ResponseProductInfo rpi = new ResponseProductInfo();

        //    var line = _context.Lines.Where(l => l.Id == LineId).FirstOrDefault();
        //    //ver se no coordinator da a informação do worker
        //    if (line == null)
        //    {
        //        rpi.Message = "Erro ao identificar a Line!!";

        //        return NotFound(rpi);
        //    }

        //    var _productions = _context.Production_Plans.Where(p => p.LineId == line.Id).ToList();
        //    var production_plan = _productions.Where(p => _systemLogic.IsAtributeInDatetime(p.InitialDate, p.EndDate, DateTime.Now) == true).FirstOrDefault();

        //    if (production_plan == null)
        //    {
        //        rpi.Message = "Não existe planos de produção na linha no momento!!";
        //        return NotFound(rpi);
        //    }
        //    //Encontrar as produções
        //    var product = _context.Products.Where(p => p.Id == production_plan.ProductId).FirstOrDefault();
        //    if (product == null)
        //    {
        //        rpi.Message = "Erro ao identificar a product!!";
        //        return NotFound(rpi);
        //    }
        //    rpi.Product = product;
        //    rpi.Message = "Info obtida com sucesso";
        //    return Ok(rpi);
        //}

        //[HttpGet]
        //[Route("CoordinatorInfo")]
        //public async Task<IActionResult> CoordinatorInfo(string WorkerIdFirebase)
        //{
        //    //Formato da resposta
        //    ResponseCoordinatorInfo rci = new ResponseCoordinatorInfo();
        //    var worker = _context.Workers.Where(w => w.IdFirebase == WorkerIdFirebase).FirstOrDefault();

        //    if (worker == null)
        //    {
        //        rci.Message = "Erro ao identificar o worker!!";

        //        return NotFound(rci);
        //    }
        //    //encontrar o operator
        //    var coord = _context.Coordinators.Where(c => c.WorkerId == worker.Id)
        //        .Include(c => c.Worker)
        //        .Include(c => c.Lines)
        //        .FirstOrDefault();
        //    if (coord == null)
        //    {
        //        rci.Message = "Erro ao identificar o Coordinator!!";

        //        return NotFound(rci);
        //    }
        //    rci.Message = "Info obtida com sucesso!!";
        //    rci.Coordinator = coord;
        //    rci.listLine = coord.Lines.ToList();

        //    return Ok(rci);
        //}

        //[HttpGet]
        //[Route("NotificationRecommendation")]
        //public async Task<IActionResult> NotificationRecommendation(int type, int workerId)
        //{
        //    //2023-06-22T00:00:00

        //    //vou fazer o código por mim pois não encontrei um algoritmo próprio
        //    ResponseNotificationRecommendation rnr = new ResponseNotificationRecommendation();
        //    //verificar se existe esse worker
        //    var wor = _context.Workers.SingleOrDefault(w => w.Id == workerId);
        //    if (wor == null)
        //    {
        //        rnr.Message = "Erro ao identificar o Worker!!";
        //        return NotFound();
        //    }
        //    //primeiro vou a todos os requests e ver se existe pelo menos 3
        //    var requests = _context.requests.Where(r => r.WorkerId == workerId && r.Type == type).ToList();
        //    if (!requests.Any() || requests.Count < 3)
        //    {
        //        rnr.Message = "É necessário um mínimo de 3 datas para realizar a previsão";
        //        return NotFound();
        //    }
        //    //Parametros para calcular a data
        //    float parte1 = 0;
        //    float parte2 = 0;
        //    //data anterior no foreach
        //    DateTime anterior = new DateTime();
        //    int days = -1;
        //    //lista de inteiros de dias entre pedidos para fazer a média
        //    List<int> diasInt = new List<int>();
        //    foreach (var request in requests.OrderBy(r=>r.Date))
        //    {
        //        //garantir que as datas dos requests são menores que as datas atuais
        //        if(request.Date.CompareTo(DateTime.Now) < 0)
        //        {
        //            //vamos ver em que parte do turno é mais frequente ver
        //            int ParteTurno = _systemLogic.GetShiftSplit(request.Date);
        //            TimeSpan timeSpanMounths = DateTime.Now.Subtract(request.Date);
        //            int MounthsLaps = Convert.ToInt32(timeSpanMounths.Days / 30);
        //            if (MounthsLaps == 0 || MounthsLaps == 1)
        //            {
        //                if (ParteTurno == 1)
        //                {
        //                    parte1++;
        //                }
        //                if (ParteTurno == 2)
        //                {
        //                    parte2++;
        //                }
        //            }
        //            if (MounthsLaps > 1)
        //            {
        //                if (ParteTurno == 1)
        //                {
        //                    parte1 = (float)(parte1 + (1 - (0.05 * MounthsLaps)));
        //                }
        //                if (ParteTurno == 2)
        //                {
        //                    parte2 = (float)(parte2 + (1 - (0.05 * MounthsLaps)));
        //                }
        //            }
        //            //vamos ver o timelaps medio entre cada request em dias
        //            TimeSpan timeSpanBetweenRequests;
        //            if (anterior != new DateTime())
        //            {
        //                timeSpanBetweenRequests = request.Date.Subtract(anterior);
        //                days = timeSpanBetweenRequests.Days;
        //                diasInt.Add(days);
        //            }
        //            anterior = request.Date;
        //        }

        //    }
        //    rnr.Message = "Info obtida com sucesso";
        //    //escolher a parte do turno
        //    if (parte1 > parte2)
        //    {
        //        rnr.ShiftSlipt = 1;
        //    }
        //    else
        //    {
        //        rnr.ShiftSlipt = 2;
        //    }
        //    //--------
        //    //fazer a média de dias
        //    int soma = 0;
        //    foreach (int numero in diasInt)
        //    {
        //        soma += numero;
        //    }
        //    int media = Convert.ToInt32(soma/diasInt.Count);
        //    //ver à quantos dias foi o ultimo pedido
        //    int lastRequestDays = DateTime.Now.Subtract(anterior).Days;
        //    //obrigar o sistema a enviar a notificação pelo menos uma vez pôr mês
        //    if (media > 31)
        //    {
        //        media = 31;
        //    }
        //    //ver a data para enviar a recomendação
        //    //o anterior vai ser a ultima data
        //    anterior = anterior.Date;
        //    anterior = anterior.AddDays(media);
        //    //não permitir que a data recomendada seja num domingo
        //    if (anterior.DayOfWeek == DayOfWeek.Sunday)
        //    {
        //        anterior.AddDays(1);
        //    }
        //    //Se o next date for menor que a data atual o sistema vai mandar a data atua e meter uma notificação a dizer que já
        //    //devia ter mandado a notificação
        //    if (anterior.CompareTo(DateTime.Now) < 0 || lastRequestDays > 31)
        //    {
        //        anterior = DateTime.Now;
        //        rnr.Message = "Já devia ter mandado a notificação";
        //        return Ok(rnr);
        //    }
        //    if(parte1 >= parte2)
        //    {
        //        //se forem iguais vai definir a parte 1
        //        rnr.ShiftSlipt = 1;
        //    }
        //    else
        //    {
        //        rnr.ShiftSlipt = 2;
        //    }
        //    //para depois ver se realmente existe
        //    rnr.ExistSchedule = false;
        //    //vai verificar se o worker tem algum horario de trabalho nesse dia para saber o turno
        //    //primeiro vai ter de ver se o woker é supervisor ou operator
        //    var ope = _context.Operators.SingleOrDefault(o => o.WorkerId == workerId);
        //    var sup = _context.Supervisors.SingleOrDefault(s => s.WorkerId == workerId);
        //    if(ope != null)
        //    {
        //        //vai ver se tem horario naquele dia
        //        var scheduleope = _context.Schedule_Worker_Lines.FirstOrDefault(s => s.OperatorId == ope.Id && s.Day.Date.Equals(anterior.Date));
        //        if(scheduleope != null)
        //        {
        //            //vai ver o schedule e vai atribuir a hora dependente do shift

        //            //vai definir a hora para enviar
        //            anterior = anterior.AddHours(_systemLogic.GetShiftHourByShiftAndPart(scheduleope.Shift, rnr.ShiftSlipt));

        //            //defir os parametros
        //            rnr.ExistSchedule = true;

        //        }
        //    }
        //    if (sup != null)
        //    {
        //        //vai ver se tem horario naquele dia
        //        var schedulesup = _context.Schedule_Worker_Lines.FirstOrDefault(s => s.SupervisorId == sup.Id && s.Day.Date.Equals(anterior.Date));
        //        if (schedulesup != null)
        //        {
        //            //vai ver o schedule e vai atribuir a hora dependente do shift
        //            //vai definir a hora para enviar
        //            anterior = anterior.AddHours(_systemLogic.GetShiftHourByShiftAndPart(schedulesup.Shift,rnr.ShiftSlipt));

        //            rnr.ExistSchedule = true;
        //        }

        //    }
        //    rnr.nextDate = anterior;
        //    return Ok(rnr);
        //}



        //-----------depois apagar é só para testar------------------------------------
        [HttpGet]
        [Route("GetDeviceById")]
        public async Task<IActionResult> GetDeviceById(int id)
        {
            var device = await _DataService.GetDeviceById(id);
            return Ok(device);
        }


    }
}
