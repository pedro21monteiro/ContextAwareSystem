using ContextServer.Data;
using ContextServer.Services;
using Microsoft.AspNetCore.Mvc;
using Models.CustomModels;
using Models.FunctionModels;
using Microsoft.EntityFrameworkCore;
using Models.ContextModels;
using Services.DataServices;

namespace Context_aware_System.Controllers

{
    [Route("api/ContextServer")]
    [ApiController]

    public class ContextServerController : Controller
    {
        private readonly IContextAwareDb _context;
        private readonly SystemLogic _systemLogic;
        private readonly IDataService _DataService;
        //para experimentar os datetimes 2023-11-20T11:11:11Z

        public ContextServerController(IContextAwareDb context, SystemLogic systemLogic, IDataService dataService)
        {
            _context = context;
            _systemLogic = systemLogic;
            _DataService = dataService;
        }

        /// <summary>
        /// Fornecerá informações relativas ao dispositivo, incluindo o seu tipo (wearable, tablet, etc). 
        /// Se o dispositivo estiver associado a um operador, serão fornecidas informações sobre a linha de produção à 
        /// qual o dispositivo está associado, o produto atualmente em produção, a sua etiqueta, a lista de componentes 
        /// em falta na linha que precisam de reposição e o turno de trabalho. Se o dispositivo estiver associado a um 
        /// coordenador, serão fornecidas informações sobre o coordenador e as linhas de produção pelas quais é responsável.
        /// </summary>
        [HttpGet]
        [Route("DeviceInfo")]
        public async Task<IActionResult> DeviceInfo(int deviceId)
        {
            ResponseDeviceInfo rdi = new ResponseDeviceInfo();
            //Encontrar o device
            var device = await _DataService.GetDeviceById(deviceId);
            if (device == null)
            {
                rdi.Message = "Erro ao identificar o Device";
                return NotFound(rdi);
            }
            //encontrar a linha
            var line = await _DataService.GetLineById(device.LineId);
            if (line == null)
            {
                rdi.Message = "Erro ao identificar a line";
                return NotFound(rdi);
            }
            rdi.Line = line;
            // ver qual o tipo do device se é 1-weareble(utilizado por operadores, os outros vao utilizados por coordenadores, exe: 2 - tablet
            //se for wearable
            if (device.Type == 1)
            {
                rdi.Type = "Weareble-Operator";
                //chegar ao produto
                // Procura o plano de produção que a linha tem no momento (só pode estar um ativo)
                var productionPlans = await _DataService.GetProdPlansByLineId(line.Id);
                var filteredProductionPlan = productionPlans?.FirstOrDefault(p => _systemLogic.IsAtributeInDatetime(p.InitialDate, p.EndDate, DateTime.Now));
                if (filteredProductionPlan == null)
                {
                    rdi.Message = "A linha não tem nenhum plano de produção ativo no momento";
                    return Ok(rdi);
                }
                //chegar ao produto
                var product = await _DataService.GetProductById(filteredProductionPlan.ProductId);
                if(product == null)
                {
                    rdi.Message = "Erro ao identificar o Produto";
                    return Ok(rdi);
                }
                rdi.ProductName = product.Name;
                rdi.TagReference = product.LabelReference;
                //dizer o turno do momento
                rdi.WorkShift = _systemLogic.GetAtualWorkShift(DateTime.Now).Shift;
                //Função dos missing Components, vai ver àquela linha se tem algum componente em falta
                var missingComponents = await _context.missingComponents.Where(m => m.LineId == line.Id).ToListAsync();
                if (missingComponents.Any())
                {
                    foreach(var mc in missingComponents)
                    {
                        var componente = await _DataService.GetComponentById(mc.ComponentId);
                        if (componente != null)
                        {
                            rdi.listMissingComponentes.Add(componente);
                        }
                    }
                }
                rdi.Message = "Info obtida com sucesso!!";
            }
            //se não for wearable vai ser um coordenador
            else
            {
                rdi.Type = "Tablet/Outro-Coordenator";
                //encontrar o worker
                var coordinator = await _DataService.GetCoordinatorById(line.CoordinatorId);
                if (coordinator == null)
                {
                    rdi.Message = "Erro ao identificar o coordinador";
                    return NotFound(rdi);
                }
                var worker = await _DataService.GetWorkerById(coordinator.WorkerId);
                if (worker == null)
                {
                    rdi.Message = "Erro ao identificar o worker!!";
                    return NotFound(rdi);
                }
                rdi.Worker = worker;
                //encontrar as lines que ele é responsável
                var lines = await _DataService.GetLinesByCoordinatorId(coordinator.Id);
                if (lines == null)
                {
                    rdi.Message = "O coordenador não é responsavel por nenhuma linha de produção";
                    return Ok(rdi);
                }
                rdi.Message = "Info obtida com sucesso!!";
                rdi.listResponsavelLines = lines;
            }
            return Ok(rdi);
        }

        /// <summary>
        /// Fornecerá informações relativas ao operador e às linhas de produção nas quais ele está a 
        /// trabalhar no dia atual, juntamente com os respetivos horários de trabalho.
        /// </summary>
        [HttpGet]
        [Route("OperatorInfo")]
        public async Task<IActionResult> OperatorInfo(string OperatorIdFirebase)
        {
            //Formato da resposta
            ResponseOperatorInfo roi = new ResponseOperatorInfo();
            //verificar quem é o worker

            var worker = await _DataService.GetWorkerByIdFirebase(OperatorIdFirebase);                
            if (worker == null)
            {
                roi.Message = "Erro ao identificar o worker!!";

                return NotFound(roi);
            }
            //adicionar o worker ao formato de resposta
            roi.Worker = worker;
            //encontrar o operator
            var ope = await _DataService.GetOperatorByWorkerId(worker.Id);
            if (ope == null)
            {
                roi.Message = "Erro ao identificar o Operator!!";

                return NotFound(roi);
            }
            roi.Operator = ope;
            //verificar se esse operador esta a trabalhar no dia atual
            var listSchedules = await _DataService.GetSchedulesByOperatorId(ope.Id);
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
                var line = await _DataService.GetLineById(a);
                if (line != null)
                {
                    roi.listLine.Add(line);
                }
            }
            //--
            int nLinhas = roi.listLine.Count;
            roi.Message = "O operador " + worker.UserName + " está a trabalhar em " + nLinhas.ToString() + " linha hoje";
            return Ok(roi);
        }

        /// <summary>
        /// Fornece informações sobre as paragens que ocorreram de acordo com as datas inseridas, dependendo de se as paragens foram planeadas ou não.
        /// </summary>
        [HttpGet]
        [Route("StopsInfo")]
        public async Task<IActionResult> StopsInfo(DateTime? dtInitial, DateTime? dtFinal, bool? planned)
        {
            ResponseStopsInfo rnsi = new ResponseStopsInfo();
            var stops = await _DataService.GetStops(null, planned, null, null, null, null, null, null);
            //ver sops que ocorreram entre aquelas datas
            if(dtInitial.HasValue || dtFinal.HasValue)
            {
                var stopsFiltered = stops?.Where(s => _systemLogic.IsAtributeInDatetime(dtInitial, dtFinal, s.InitialDate, s.EndDate));
                stops = stopsFiltered?.ToList();
            }
            if (stops == null || !stops.Any())
            {
                rnsi.Message = "Não existem paragens nessas datas";
                return Ok(rnsi);
            }
            foreach (var stop in stops)
            {
                StopResponse stopResponse = new StopResponse();
                stopResponse.Stop = stop;
                if (stop.ReasonId != null)
                {
                    var reason = await _DataService.GetReasonById(stop.ReasonId.Value);
                    if (reason != null)
                    {
                        stopResponse.Description = reason.Description;
                    }
                }
                rnsi.listNewStops.Add(stopResponse);
            }
            rnsi.Message = "Info obtida com sucesso";
            return Ok(rnsi);
        }

        /// <summary>
        /// Fornecerá informações relativas à linha de produção, disponibilizará dados sobre as paragens e as 
        /// produções de produtos que ocorreram na mesma, de acordo com as datas inseridas. Além disso, fornecerá
        /// informações sobre o coordenador responsável pela linha e os produtos que estão ou estiveram a ser produzidos durante essas datas.
        /// </summary>
        [HttpGet]
        [Route("LineInfo")]
        public async Task<IActionResult> LineInfo(int LineId, DateTime? dtInitial, DateTime? dtFinal)
        {
            // Formato da resposta
            ResponseLineInfo rli = new ResponseLineInfo();
            // Verifica a linha
            var line = await _DataService.GetLineById(LineId);
            if (line == null)
            {
                rli.Message = "Erro ao identificar a Line!!";
                return NotFound(rli);
            }
            rli.Line = line;
            // Verifica o coordenador
            var coord = await _DataService.GetCoordinatorById(line.CoordinatorId);
            if (coord == null)
            {
                rli.Message = "Erro ao identificar o coordinator!!";
                return NotFound(rli);
            }
            rli.Coordinator = coord;
            // verifica o worker para dar informações sobre ele
            var worker = await _DataService.GetWorkerById(coord.WorkerId);
            if (worker == null)
            {
                rli.Message = "Erro ao identificar o worker!!";
                return NotFound(rli);
            }
            rli.Worker = worker;
            // Verifica as paragens que ocorreram naquela linha naquelas datas
            var stops = await _DataService.GetStopsByLineId(line.Id);
            if (stops != null)
            {
                rli.listStops.AddRange(stops.Where(stop => _systemLogic.IsAtributeInDatetime(dtInitial, dtFinal, stop.InitialDate, stop.EndDate)));
                stops.Clear();
            }
            // Primeiro procura pelos planos de produção que houveram nessas datas nessa linha
            var productionPlans = await _DataService.GetProdPlansByLineId(line.Id);
            // Verifica os que estão entre aquelas datas
            if (productionPlans != null)
            {
                var filteredProductionPlans = productionPlans.Where(p => _systemLogic.IsAtributeInDatetime(dtInitial, dtFinal, p.InitialDate, p.EndDate)).ToList();
                productionPlans = filteredProductionPlans;
            }
            if (productionPlans == null)
            {
                rli.Message = "Não existem Planos de produções nessas datas";
                return Ok(rli);
            }
            // Vai buscar as produções daqueles planos de produção e adiciona diretamente ao rli.listProductionsByProductionPlan
            foreach (var pp in productionPlans)
            {
                ProductionPlanResponse PPR = new ProductionPlanResponse();
                PPR.Production_plan = pp;
                var productions = await _DataService.GetProductionsByProdPlanId(pp.Id);
                if (productions != null)
                {
                    var filteredProductions = productions.Where(prod => _systemLogic.IsAtributeInDatetime(dtInitial, dtFinal, prod.Day));
                    if (filteredProductions != null)
                    {
                        PPR.listProductions = filteredProductions.ToList();
                    }
                }
                var product = await _DataService.GetProductById(pp.ProductId);
                if(product != null)
                {
                    PPR.Product = product;
                }
                rli.listProductionsByProductionPlan.Add(PPR);
            }

            rli.Message = "Info obtida com sucesso";
            return Ok(rli);
        }

        /// <summary>
        /// Disponibiliza informações relativas ao supervisor e às linhas de produção que ele esteve a 
        /// supervisionar no dia inserido, juntamente com os respetivos horários de trabalho, de acordo com o pedido.
        /// </summary>
        [HttpGet]
        [Route("SupervisorInfo")]
        public async Task<IActionResult> SupervisorInfo(string SupervisorIdFirebase, DateTime? Day)
        {
            //Formato da resposta
            ResponseSupervisorInfo rsi = new ResponseSupervisorInfo();
            var worker = await _DataService.GetWorkerByIdFirebase(SupervisorIdFirebase);
            if (worker == null)
            {
                rsi.Message = "Erro ao identificar o worker!!";
                return NotFound(rsi);
            }
            rsi.Worker = worker;
            //encontrar o supervisor
            var sup = await _DataService.GetSupervisorByWorkerId(worker.Id);
            if (sup == null)
            {
                rsi.Message = "Erro ao identificar o Supervisor!!";
                return NotFound(rsi);
            }
            rsi.Supervisor = sup;
            //verificar se esse operador esta a trabalhar no dia atual
            //Se a DataPesquisa estiver preenchida fica igual a day, senão fica igual a datetime
            DateTime DataPesquisa = Day ?? DateTime.Now;
            //vai buscar os horarios do supervisor
            var schedules = await _DataService.GetSchedulesBySupervisorId(sup.Id);
            //vai aos schedules e guarda no formato de resposta os horarios do superviso do dia atual
            if(schedules != null)
            {
                rsi.listSWL = schedules.Where(s => s.Day.Date.Equals(DataPesquisa.Date)).ToList();
            }
            //ver em quantas linhas esta a trabalhar e guardar numa lista de inteiros para poupar pedidos efetuados à camada de integração
            List<int> listIntLineIds = new List<int>();
            foreach (Schedule_Worker_Line swl in rsi.listSWL)
            {
                if (!listIntLineIds.Contains(swl.LineId))
                {
                    listIntLineIds.Add(swl.LineId);
                }
            }
            //Ir à lista de inteiros que contem os ids das linhas que o supervisor está a trabalhar e adicionar
            foreach (int a in listIntLineIds)
            {
                var line = await _DataService.GetLineById(a);
                if (line != null)
                {
                    rsi.listLine.Add(line);
                }
            }
            //--
            int nLinhas = rsi.listLine.Count;
            rsi.Message = "O supervisor " + worker.UserName + " supervisionou " + nLinhas.ToString() + " linhas no dia: " + DataPesquisa.Date.ToString("yyyy-MM-dd");
            return Ok(rsi);
        }


        /// <summary>
        /// Fornece a lista de produções de produtos ocorridas de acordo com as datas inseridas.
        /// </summary>
        [HttpGet]
        [Route("GetProductionsInfo")]
        public async Task<IActionResult> GetProductionsInfo(int LineId, DateTime? dtInitial, DateTime? dtFinal)
        {
            ResponseGetProductionsInfo rgpi = new ResponseGetProductionsInfo();
            var line = await _DataService.GetLineById(LineId);
            if (line == null)
            {
                rgpi.Message = "Erro ao identificar a Line!!";
                return NotFound(rgpi);
            }
            // Primeiro procura pelos planos de produção que houveram nessas datas nessa linha
            var productionPlans = await _DataService.GetProdPlansByLineId(line.Id);
            // Verifica os que estão entre aquelas datas
            if (productionPlans != null)
            {
                var filteredProductionPlans = productionPlans.Where(p => _systemLogic.IsAtributeInDatetime(dtInitial, dtFinal, p.InitialDate, p.EndDate)).ToList();
                productionPlans = filteredProductionPlans;
            }
            if (productionPlans == null)
            {
                rgpi.Message = "Não existem Planos de produções nessas datas";
                return Ok(rgpi);
            }
            foreach (var pp in productionPlans)
            {
                var productions = await _DataService.GetProductionsByProdPlanId(pp.Id);
                if (productions != null)
                {
                    var filteredProductions = productions.Where(prod => _systemLogic.IsAtributeInDatetime(dtInitial, dtFinal, prod.Day));
                    if (filteredProductions != null)
                    {
                        rgpi.listProductions.AddRange(filteredProductions.ToList());
                    }
                }
            }
            if(rgpi.listProductions == null || !rgpi.listProductions.Any())
            {
                rgpi.Message = "Não foram encontradas produções nessa linha";
                return Ok(rgpi);
            }
            rgpi.Message = "Info obtida com sucesso!!";
            return Ok(rgpi);
        }


        /// <summary>
        /// Disponibiliza a lista de componentes que estão a ser utilizados na linha de produção à qual o dispositivo está associado.
        /// </summary>
        [HttpGet]
        [Route("GetComponentsDeviceInfo")]
        public async Task<IActionResult> GetComponentsDeviceInfo(int deviceId)
        {
            ResponseGetComponentsDeviceInfo rgcdi = new ResponseGetComponentsDeviceInfo();
            //encontrar o device
            var device = await _DataService.GetDeviceById(deviceId);
            if (device == null)
            {
                rgcdi.Message = "Erro ao identificar o Device!!";
                return NotFound(rgcdi);
            }
            var line = await _DataService.GetLineById(device.LineId);
            if (line == null)
            {
                rgcdi.Message = "Erro ao identificar a linha de produção!!";
                return NotFound(rgcdi);
            }
            //encontrar o plano de produção na linha no momento
            var productionPlans = await _DataService.GetProdPlansByLineId(line.Id);
            if (productionPlans == null || !productionPlans.Any())
            {
                rgcdi.Message = "Não existe nenhum plano de produção na linha no momento!!";
                return NotFound(rgcdi);
            }
            //Verifica os que estão entre aquelas datas
            var filteredProductionPlans = productionPlans.Where(p => _systemLogic.IsAtributeInDatetime(p.InitialDate, p.EndDate, DateTime.Now)).ToList();
            var productionPlan = filteredProductionPlans?.FirstOrDefault();
            if (productionPlan == null)
            {
                rgcdi.Message = "Não existe nenhum plano de produção na linha no momento!!";
                return NotFound(rgcdi);
            }
            //encontrar o produto
            var product = await _DataService.GetProductById(productionPlan.ProductId);
            if (product == null)
            {
                rgcdi.Message = "Erro ao identificar a product!!";
                return NotFound(rgcdi);
            }
            //Ir buscar os components daquele produto
            var listComponentProducts = await _DataService.GetComponentProductsByProductId(product.Id);
            if(listComponentProducts == null || !listComponentProducts.Any())
            {
                rgcdi.Message = "O produto " + product.Name + " ainda não tem os componentes definidos";
                return Ok(rgcdi);
            }
            foreach(var cp in listComponentProducts)
            {
                var component = await _DataService.GetComponentById(cp.ComponentId);
                if(component != null)
                {
                    rgcdi.listComponents.Add(component);
                }
            }
            rgcdi.Message = "Info obtida com sucesso!!";
            return Ok(rgcdi);
        }

        /// <summary>
        /// Fornecerá informações relativas ao produto que está a ser produzido na linha de produção no momento em que o pedido foi efetuado.
        /// </summary>
        [HttpGet]
        [Route("ProductInfo")]
        public async Task<IActionResult> ProductInfo(int LineId)
        {
            //Formato da resposta
            ResponseProductInfo rpi = new ResponseProductInfo();
            var line = await _DataService.GetLineById(LineId);
            if (line == null)
            {
                rpi.Message = "Erro ao identificar a Line!!";
                return NotFound(rpi);
            }
            //Ver se naquela linha tem algum plano de produção ativo no momento
            var productionPlans = await _DataService.GetProdPlansByLineId(line.Id);
            if (productionPlans == null || !productionPlans.Any())
            {
                rpi.Message = "Não existe nenhum plano de produção na linha no momento!!";
                return NotFound(rpi);
            }
            // Verifica os que estão entre aquelas datas
            var filteredProductionPlans = productionPlans.Where(p => _systemLogic.IsAtributeInDatetime(p.InitialDate, p.EndDate, DateTime.Now)).ToList();
            var productionPlan = filteredProductionPlans?.FirstOrDefault();
            if (productionPlan == null)
            {
                rpi.Message = "Não existe nenhum plano de produção na linha no momento!!";
                return NotFound(rpi);
            }
            //vai procurar o produto
            var product = await _DataService.GetProductById(productionPlan.ProductId);
            if (product == null)
            {
                rpi.Message = "Erro ao identificar a product!!";
                return NotFound(rpi);
            }
            rpi.Product = product;
            rpi.Message = "Info obtida com sucesso";
            return Ok(rpi);
        }

        /// <summary>
        /// Fornecerá informações relativas ao coordenador e às linhas de produção das quais ele é responsável, de acordo com o pedido.
        /// </summary>
        [HttpGet]
        [Route("CoordinatorInfo")]
        public async Task<IActionResult> CoordinatorInfo(string WorkerIdFirebase)
        {
            //Formato da resposta
            ResponseCoordinatorInfo rci = new ResponseCoordinatorInfo();
            var worker = await _DataService.GetWorkerByIdFirebase(WorkerIdFirebase);              
            if (worker == null)
            {
                rci.Message = "Erro ao identificar o worker!!";
                return NotFound(rci);
            }
            rci.Worker = worker;
            //encontrar o coordinador
            var coord = await _DataService.GetCoordinatorByWorkerId(worker.Id);
            if (coord == null)
            {
                rci.Message = "Erro ao identificar o Coordinator!!";
                return NotFound(rci);
            }
            rci.Coordinator = coord;
            //encontrar as lines que ele é responsável
            var lines = await _DataService.GetLinesByCoordinatorId(coord.Id);
            if(lines != null)
            {
                rci.listLine = lines;
            }
            rci.Message = "Info obtida com sucesso!!";

            return Ok(rci);
        }

        /// <summary>
        /// Devolve a informação sobre os componentes em falta, as respetivas linhas afetadas e a data em que o pedido
        /// de reposição do componente foi efetuado.
        /// </summary>
        [HttpGet]
        [Route("GetMissingComponents")]
        public async Task<IActionResult> GetMissingComponents()
        {
            //Formato da resposta
            ResponseGetMissingComponents rgmc = new ResponseGetMissingComponents();
            var missingComponentes = await _context.missingComponents.ToListAsync();
            if(missingComponentes != null)
            {
                foreach(var missingComponente in missingComponentes)
                {
                    var line = await _DataService.GetLineById(missingComponente.LineId);
                    var componente = await _DataService.GetComponentById(missingComponente.ComponentId);
                    if(line!= null && componente != null)
                    {
                        MissingComponentResponse mcr = new MissingComponentResponse();
                        mcr.OrderDate = missingComponente.OrderDate;
                        mcr.Line = line;
                        mcr.Component = componente;
                        rgmc.listMissingComponentes.Add(mcr);
                    }
                }
            }
            rgmc.Message = "Info obtida com sucesso!!";
            return Ok(rgmc);
        }

        /// <summary>
        /// Fornecerá, se possível (caso o trabalhador já tenha horário de trabalho definido para aquele dia), uma 
        /// data para enviar-lhe uma notificação relativa a determinada informação, como, por exemplo, o envio de um
        /// gráfico ou outros dados. Caso não seja possível, enviará o dia e a parte do turno em que a notificação deve ser enviada.
        /// </summary>   
        [HttpGet]
        [Route("NotificationRecommendation")]
        public async Task<IActionResult> NotificationRecommendation(int type, int workerId)
        {
            ResponseNotificationRecommendation rnr = new ResponseNotificationRecommendation();
            var worker = await _DataService.GetWorkerById(workerId);
            if (worker == null)
            {
                rnr.Message = "Erro ao identificar o Worker!!";
                return BadRequest(rnr);
            }
            var requests = await _context.requests.Where(r => r.WorkerId == workerId && r.Type == type && r.Date < DateTime.Now)
                .OrderBy(r => r.Date)
                .ToListAsync();
            if (requests.Count < 3)
            {
                rnr.Message = "É necessário um mínimo de 3 datas para realizar a previsão";
                return BadRequest(rnr);
            }
            float parte1 = 0;
            float parte2 = 0;
            List<int> diasInt = new List<int>();
            DateTime anterior = DateTime.MinValue;
            foreach (var request in requests)
            {
                //ver em que parte do turno foi feito aquele request
                int ParteTurno = _systemLogic.GetShiftSplit(request.Date);
                TimeSpan timeSpanMonths = DateTime.Now.Subtract(request.Date);
                //ver à quantos meses foi
                int monthsLaps = Convert.ToInt32(timeSpanMonths.Days / 30);
                //se foi nos ultimos 2 meses vai contar como valor 1, depois a cada mes que passe perde valor
                if (monthsLaps == 0 || monthsLaps == 1)
                {
                    if (ParteTurno == 1)
                    {
                        parte1++;
                    }
                    if (ParteTurno == 2)
                    {
                        parte2++;
                    }
                }
                else if (monthsLaps > 1)
                {
                    if (ParteTurno == 1)
                    {
                        parte1 += (float)(1 - (0.05 * monthsLaps));
                    }
                    if (ParteTurno == 2)
                    {
                        parte2 += (float)(1 - (0.05 * monthsLaps));
                    }
                }

                if (anterior != DateTime.MinValue)
                {
                    int daysBetweenRequests = (int)(request.Date - anterior).TotalDays;
                    diasInt.Add(daysBetweenRequests);
                }

                anterior = request.Date;
            }
            if (parte1 >= parte2)
            {
                rnr.ShiftSlipt = 1;
            }
            else
            {
                rnr.ShiftSlipt = 2;
            }
            //fazer a média de dias
            int soma = 0;
            foreach (int numero in diasInt)
            {
                soma += numero;
            }
            int media = Convert.ToInt32(soma / diasInt.Count);
            //ver à quantos dias foi o ultimo pedido
            int lastRequestDays = DateTime.Now.Subtract(anterior).Days;
            //obrigar o sistema a enviar a notificação pelo menos uma vez pôr mês
            if (media > 31)
            {
                media = 31;
            }
            //ver a data para mandar pedido
            anterior = anterior.Date.AddDays(media);
            //não pode mandar ao domingi
            if (anterior.DayOfWeek == DayOfWeek.Sunday)
            {
                anterior = anterior.AddDays(1);
            }
            //se já o ultimo envio foi à mais de um mes vai enviar ou já deveria ter mandado pedido vai mandar jà
            lastRequestDays = (int)(DateTime.Now - anterior).TotalDays;
            if (lastRequestDays > 31 ||anterior.CompareTo(DateTime.Now) < 0)
            {
                anterior = DateTime.Now.Date;
                rnr.Message = "Já devia ter mandado a notificação";
            }
            //ver se já tem horario de trabalho nesse dia
            var ope = await _DataService.GetOperatorByWorkerId(workerId);              
            if (ope != null)
            {
                var schedulesope = await _DataService.GetSchedules(null, anterior.Date, null, null, ope.Id, null);
                if(schedulesope != null)
                {
                    var scheduleope = schedulesope.FirstOrDefault();
                    if (scheduleope != null)
                    {
                        anterior = anterior.AddHours(_systemLogic.GetShiftHourByShiftAndPart(scheduleope.Shift, rnr.ShiftSlipt));
                        rnr.ExistSchedule = true;
                    }
                }       
            }
            var sup = await _DataService.GetSupervisorByWorkerId(workerId);
            if (sup != null)
            {
                var schedulessup = await _DataService.GetSchedules(null, anterior.Date, null, null, null, sup.Id);
                if (schedulessup != null)
                {
                    var schedulesup = schedulessup.FirstOrDefault();
                    if (schedulesup != null)
                    {
                        anterior = anterior.AddHours(_systemLogic.GetShiftHourByShiftAndPart(schedulesup.Shift, rnr.ShiftSlipt));
                        rnr.ExistSchedule = true;
                    }
                }
            }
            rnr.Message = "Info obtida com sucesso";
            rnr.nextDate = anterior;
            return Ok(rnr);
        }

        /// <summary>
        /// Retorna o histórico de alertas enviados pela aplicação.
        /// </summary>
        [HttpGet]
        [Route("GetAlertsHistory")]
        public async Task<IActionResult> GetAlertsHistory()
        {
            return Ok(await _context.alertsHistories.ToListAsync());
        }
    }
}
