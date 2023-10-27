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
                //--------------------------------------------------------------Falta implementar a função dos missing Components
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
        [Route("NewStopsInfo")]
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
        /// Disponibiliza informações relativas ao supervisor e às linhas de produção que ele está a 
        /// supervisionar no dia atual, juntamente com os respetivos horários de trabalho, de acordo com o pedido.
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
        /// Esta função realiza uma operação específica.
        /// </summary>
        /// 
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

    }
}
