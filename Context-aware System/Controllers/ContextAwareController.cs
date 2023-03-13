using Context_aware_System.ContextDbModels;
using Context_aware_System.Data;
using Context_aware_System.Services;
using Microsoft.AspNetCore.Mvc;
using Models.CustomModels;
using Models.FunctionModels;
using Models.Models;
namespace Context_aware_System.Controllers

{
    [Route("api/ContextAware")]
    [ApiController]
    public class ContextAwareController : Controller
    {
        private readonly Service _service;
        private readonly ContextAwareDataBaseContext _context;
        private readonly SystemLogic _systemLogic;

        //para experimentar os datetimes 2023-11-20T11:11:11Z

        public ContextAwareController(Service service, ContextAwareDataBaseContext context, SystemLogic systemLogic)
        {
            _service = service;
            _context = context;
            _systemLogic = systemLogic; 
        }

        [HttpGet]
        [Route("DeviceInfo")]
        public async Task<IActionResult> DeviceInfo(string DeviceIdFirebase)
        {
            //Formato da resposta
            ResponseDeviceInfo rdi = new ResponseDeviceInfo();

            List<Device> listDevices = new List<Device>();
            List<Worker> listWorkers = new List<Worker>();
            List<Line> listLines = new List<Line>();
            List<Production> listProdutions = new List<Production>();
            List<Product> listProducts = new List<Product>();
            List<Component> listComponents = new List<Component>();
            List<Product_Component> listProduct_Components = new List<Product_Component>();

            listDevices = await _service.GetDevices();
            listWorkers = await _service.GetWorkers();
            listLines = await _service.GetLines();
            listProdutions = await _service.GetProductions();
            listProducts = await _service.GetProducts();
            listComponents = await _service.GetComponents();
            listProduct_Components = await _service.GetProduct_Components();

            //encontrar o device
            var device = listDevices.Where(d => d.IdFirebase == DeviceIdFirebase).FirstOrDefault();
            if (device == null)
            {
                rdi.Message = "Erro ao identificar o Device!!";
                return NotFound(rdi);
            }
            //ver qual o tipo do device se é 1- weareble dos operadors, os outros para já vao ser dos coordenadores, exe: 2-tablet
            //se for wearable
            if(device.Type == 1)
            {
                //adicionar as listas necessárias para esta função

                rdi.Type = "Weareble-Operator";
                //encontrar a linha
                var _line = listLines.Where(l => l.Id == device.LineId).FirstOrDefault();
                if (_line == null)
                {
                    rdi.Message = "Erro ao identificar a linha de produção!!";
                    return NotFound(rdi);
                }
                rdi.line = _line;

                //ver o workshift do operador
                WorkShift ws = _systemLogic.GetAtualWorkShift(DateTime.Now);
                rdi.WorkShift = ws.Shift;
                rdi.WorkShiftString = ws.ShiftString;

                //encontrar o product reference
                var _production = listProdutions.Where(p => p.LineId == _line.Id && _systemLogic.dateTimeIsActiveNow(p.InitialDate,p.EndDate) == true).FirstOrDefault();
                if (_production == null)
                {
                    rdi.Message = "Erro ao identificar a linha de produção!!";
                    return NotFound(rdi);
                }
                //encontrar o product pois é lá que tem a a product reference
                var product = listProducts.Where(p => p.Id == _production.ProductId).FirstOrDefault();
                if (product == null)
                {
                    rdi.Message = "Erro ao identificar o product!!";
                    return NotFound(rdi);
                }
                //aqui já temos a referencia do produto
                rdi.ProductReference = product.Reference;

                //Agora aqui vou ter de encher a lista de missing componentes que para já vou meter todos e encontrar a tag
                

                foreach (var pc in listProduct_Components)
                {
                    if (pc.ProductId == product.Id)
                    {
                        foreach (var comp in listComponents)
                        {
                            if (comp.Id == pc.ComponentId)
                            {
                                //ver se é etiqueta
                                if (comp.Category == 1)
                                {
                                    rdi.TagReference = comp.Name;
                                }
                                rdi.listMissingComponentes.Add(comp);
                            }
                        }
                    }
                }
            }
            else
            {
                //se não for wearable vai ser operado por um coordenador
                rdi.Type = "Tablet/Outro-Coordenator";
                //encontrar a lista de linhas que o coordenador é responsavel
                var _line = listLines.Where(l => l.Id == device.LineId).FirstOrDefault();
                if (_line == null)
                {
                    rdi.Message = "Erro ao identificar a linha de produção!!";
                    return NotFound(rdi);
                }
                //encontrar o worker
                var worker = listWorkers.Where(w => w.Id == _line.CoordenatorId).FirstOrDefault();
                if (worker == null)
                {
                    rdi.Message = "Erro ao identificar o worker!!";

                    return NotFound(rdi);
                }
                rdi.Coordinator.FillWorker(worker);
                //na linha tem o idcoordenador
                rdi.Coordinator.WorkerId = _line.CoordenatorId;
                foreach(var line in listLines)
                {
                    if(line.CoordenatorId == _line.CoordenatorId)
                    {
                        rdi.Coordinator.listLinesResponsable.Add(line);
                    }
                }

            }
            rdi.Message = "Info obtida com sucesso!!";
            return Ok(rdi);
        }

        [HttpGet]
        [Route("MachineInfo")]
        public async Task<IActionResult> MachineInfo(int MachineId, DateTime? dtInitial, DateTime? dtFinal)
        {
            //Formato da resposta
            ResponseMachineInfo rmi = new ResponseMachineInfo();

            List<Machine> listMachines = new List<Machine>();
            List<Line> listLines = new List<Line>();
            List<Stop> listStops = new List<Stop>();

            listMachines = await _service.GetMachines();
            listLines = await _service.GetLines();
            listStops = await _service.GetStops();


            var machine = listMachines.Where(m => m.Id == MachineId).FirstOrDefault();
            if (machine == null)
            {
                rmi.Message = "Erro ao identificar a Machine!!";

                return NotFound(rmi);
            }
            var _line = listLines.Where(p => p.Id == machine.LineId).FirstOrDefault();
            if (_line == null)
            {
                rmi.Message = "Erro ao identificar a Linha de produção!!";

                return NotFound(rmi);
            }

            //ver se aquela linha teve paragen
            var stps = listStops.Where(s=>s.LineId == _line.Id && _systemLogic.IsAtributeInDatetime(dtInitial,dtFinal,s.InitialDate,s.EndDate) == true).ToList();

            rmi.listStops = stps;
            rmi.Message = "Info obtida com sucesso";
            rmi.LineId = _line.Id;
            rmi.line = _line;

            return Ok(rmi);
        }

        [HttpGet]
        [Route("OperatorInfo")]
        public async Task<IActionResult> OperatorInfo(string OperatorIdFirebase)
        {
            //Formato da resposta
            ResponseOperatorInfo roi = new ResponseOperatorInfo();

            List<Operator> listOperators = new List<Operator>();
            List<Line> listLines = new List<Line>();
            List<Schedule_Worker_Line> listSWLs = new List<Schedule_Worker_Line>();
            List<Worker> listWorkers = new List<Worker>();

            listOperators = await _service.GetOperators();
            listLines = await _service.GetLines();
            listSWLs = await _service.GetSchedule_Worker_Lines();
            listWorkers = await _service.GetWorkers();

            //verificar quem é o worker
            var worker = listWorkers.Where(w => w.IdFirebase == OperatorIdFirebase).FirstOrDefault();
            if (worker == null)
            {
                roi.Message = "Erro ao identificar o worker!!";

                return NotFound(roi);
            }
            var ope = listOperators.Where(o => o.WorkerId == worker.Id).FirstOrDefault();
            if (ope == null)
            {
                roi.Message = "Erro ao identificar o Operator!!";

                return NotFound(roi);
            }
            ope.FillOperator(worker);
            roi.Operator = ope;

            //verificar se esse operador esta a trabalhar no dia atual
            foreach (Schedule_Worker_Line swl in listSWLs)
            {
                if(swl.WorkerId == ope.Id && swl.InitialDate.Date == DateTime.Now.Date)
                {
                    roi.listSWL.Add(swl);
                }
            }
            
            foreach(Schedule_Worker_Line swl2 in roi.listSWL)
            {
                foreach(Line l in listLines)
                {
                    if(l.Id == swl2.LineId)
                    {
                        roi.listLine.Add(l);
                    }
                }
                
            }
            //ver em quantas linhas esta a trabalhar
            int WorkLinesday = roi.listLine.Count;

            if(roi.listLine.Count == 0)
            {
                roi.Message = "O operador " + worker.UserName + " não está a trabalhar em nenhuma linha hoje";
            }
            else
            {
                roi.Message = "O operador " + worker.UserName + " está a trabalhar em " + WorkLinesday.ToString() + " linha hoje";
            }

            return Ok(roi);
        }

        [HttpGet]
        [Route("NewProducedProductsInfo")]
        public async Task<IActionResult> NewProducedProductsInfo(int LineId)
        {
            //Formato da resposta
            ResponseNewProductsProducedInfo rnpi = new ResponseNewProductsProducedInfo();

            List<Production> listProductions = new List<Production>();

            listProductions = await _service.GetProductions();
            //Aqui se encontrar a prodution line vai ter de procurar Production e os componentes da mesma
            var production = listProductions.Where(p => p.Id == LineId).FirstOrDefault();
            if (production == null)
            {
                rnpi.Message = "Erro ao identificar a production!!";
                return NotFound(rnpi);
            }         
            if (listProductions.Count() > _context.NewProducedProductsInfos.Count())
            {
                foreach (Production pd in listProductions)
                {
                    var nppi = _context.NewProducedProductsInfos.SingleOrDefault(n => n.ProductionId == pd.Id);
                    if (nppi == null)
                    {
                        //vamos adicionar na base de dados
                        NewProducedProductsInfo np = new NewProducedProductsInfo();
                        np.ProductionId = pd.Id;
                        np.ProductionObjective = pd.Objective;
                        np.ProductionProduced = pd.Produced;
                        np.ProductionLineId = pd.LineId;
                        np.VerificationDate = DateTime.Now;
                        _context.NewProducedProductsInfos.Add(np);
                        _context.SaveChanges();
                    }
                }
            }
            //atualizar a lista que vai ser escrita
            var productionContext = _context.NewProducedProductsInfos.Where(p => p.ProductionId == production.Id).FirstOrDefault();
            if (productionContext == null)
            {
                rnpi.Message = "Não existe essa produção!!";
                return NotFound(rnpi);
            }
            //preencher a resposta
            if (productionContext.ProductionProduced >= production.Produced)
            {
                rnpi.Message = "Não foram produzidos novos produtos";
                rnpi.LastProduced = productionContext.ProductionProduced;
                rnpi.LastCheck = productionContext.VerificationDate;
                rnpi.AtualProduced = production.Produced;
                rnpi.AtualCheck = DateTime.Now;
            }
            else
            {
                rnpi.Message = "Foram produzidos novos produtos";
                rnpi.LastProduced = productionContext.ProductionProduced;
                rnpi.LastCheck = productionContext.VerificationDate;
                rnpi.AtualProduced = production.Produced;
                rnpi.AtualCheck = DateTime.Now;
                rnpi.NewProduced = production.Produced - productionContext.ProductionProduced; ;
            }
            //atualizar os dados daquela produção
            productionContext.ProductionProduced = production.Produced;
            productionContext.VerificationDate = rnpi.AtualCheck;
            _context.NewProducedProductsInfos.Update(productionContext);
            _context.SaveChanges();

            //retornar a resposta
            return Ok(rnpi);
        }

        [HttpGet]
        [Route("NewStopsInfo")]
        public async Task<IActionResult> NewStopsInfo()
        {
            ResponseNewStopsInfo rnsi = new ResponseNewStopsInfo();
            List<Stop> listStops = new List<Stop>();
            List<Reason> listReasons = new List<Reason>();

            listStops = await _service.GetStops();
            listReasons = await _service.GetReasons();
                  
            //ver as novas paragens
            rnsi.listNewStops = new List<Stop>();
           
            var LastStopverification = _context.StopsVerifications.OrderBy(s => s.VerificationDate).Last();
            if(LastStopverification == null)
            {
                rnsi.Message = "Erro ao encontrar ultima verificação !!";
                return NotFound(rnsi);
            }
            foreach (Stop stop in listStops)
            {
                if(DateTime.Compare(stop.InitialDate, LastStopverification.VerificationDate) > 0)
                {
                    //tbm vai mostrar a reason da paragem
                    Stop stop1 = new Stop();
                    foreach(Reason r in listReasons)
                    {
                        if(r.StopId == stop.Id)
                        {
                            stop1 = stop;
                            stop1.Reason = r;
                        }
                    }
                    rnsi.listNewStops.Add(stop1);
                }
            }
            if(rnsi.listNewStops.Count == 0)
            {
                rnsi.Message = "Não existe novas paragens !!";
            }
            else
            {
                rnsi.Message = "Existe novas paragens !!";
            }
            //antes de retornar vai guardar a nova vaerificação nda bd
            StopsVerification sv = new StopsVerification();
            sv.VerificationDate = DateTime.Now;
            sv.NewStopsCount = rnsi.listNewStops.Count;
            _context.StopsVerifications.Add(sv);
            _context.SaveChanges();

            return Ok(rnsi);
        }

        [HttpGet]
        [Route("LineInfo")]
        public async Task<IActionResult> LineInfo(int LineId,DateTime? dtInitial, DateTime? dtFinal)
        {
            //Formato da resposta
            ResponseLineInfo rli = new ResponseLineInfo();

            List<Line> listLines = new List<Line>();
            List<Stop> listStops = new List<Stop>();
            List<Worker> listWorkers = new List<Worker>();
            List<Coordinator> listCoordinators = new List<Coordinator>();
            List<Production> listProductions = new List<Production>();
            List<Product> listProducts = new List<Product>();

            listLines = await _service.GetLines();
            listStops = await _service.GetStops();
            listWorkers = await _service.GetWorkers();
            listCoordinators = await _service.GetCoordinators();
            listProductions = await _service.GetProductions();
            listProducts = await _service.GetProducts();

            var line = listLines.Where(l => l.Id == LineId).FirstOrDefault();
            if (line == null)
            {
                rli.Message = "Erro ao identificar a Line!!";

                return NotFound(rli);
            }
            var lineStops = listStops.Where(s => s.LineId == LineId && _systemLogic.IsAtributeInDatetime(dtInitial, dtFinal, s.InitialDate, s.EndDate) == true).ToList();
            rli.listStops = lineStops;

            var worker = listWorkers.Where(w=> w.Id == line.CoordenatorId).FirstOrDefault();
            var coordinator = listCoordinators.Where(c=>c.WorkerId == line.CoordenatorId).FirstOrDefault(); 
            if(worker == null || coordinator == null)
            {
                rli.Message = "Erro ao identificar o coordenador!";

                return NotFound(rli);
            }
            coordinator.FillWorker(worker);
            rli.Coordinator = coordinator;

            var production = listProductions.Where(p => p.LineId == line.Id && _systemLogic.dateTimeIsActiveNow(p.InitialDate, p.EndDate) == true).FirstOrDefault();
            if (production == null)
            {
                rli.Message = "Erro ao identificar a linha de produção!!";
                return NotFound(rli);
            }
            rli.Production = production;
            //encontrar o product pois é lá que tem a a product reference
            var product = listProducts.Where(p => p.Id == production.ProductId).FirstOrDefault();
            if (product == null)
            {
                rli.Message = "Erro ao identificar a product!!";
                return NotFound(rli);
            }
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

            List<Supervisor> listSupervisors = new List<Supervisor>();
            List<Line> listLines = new List<Line>();
            List<Schedule_Worker_Line> listSWLs = new List<Schedule_Worker_Line>();
            List<Worker> listWorkers = new List<Worker>();

            listSupervisors = await _service.GetSupervisors();
            listLines = await _service.GetLines();
            listSWLs = await _service.GetSchedule_Worker_Lines();
            listWorkers = await _service.GetWorkers();

            //verificar quem é o worker
            var worker = listWorkers.Where(w => w.IdFirebase == SupervisorIdFirebase).FirstOrDefault();
            if (worker == null)
            {
                rsi.Message = "Erro ao identificar o worker!!";

                return NotFound(rsi);
            }
            var supervisor= listSupervisors.Where(s => s.WorkerId == worker.Id).FirstOrDefault();
            if (supervisor == null)
            {
                rsi.Message = "Erro ao identificar o Supervisor!!";

                return NotFound(rsi);
            }
            
            supervisor.FillOperator(worker);
            rsi.Supervisor = supervisor;

            //verificar se esse operador esta a trabalhar no dia atual
            if (Day.HasValue)
            {
                var swl = listSWLs.Where(s => s.WorkerId == worker.Id && s.InitialDate.Date == Day.Value.Date).ToList();
                rsi.listSWL = swl;
            }
            else
            {
                var swl = listSWLs.Where(s => s.WorkerId == worker.Id && s.InitialDate.Date == DateTime.Now.Date).ToList();
                rsi.listSWL = swl;
            }

            var lines = listLines.Where(l => rsi.listSWL.SingleOrDefault(sw => sw.LineId == l.Id) != null).ToList();
            rsi.listLine = lines;

            //ver em quantas linhas esta a trabalhar
            int WorkLinesday = rsi.listLine.Count;

            if (rsi.listLine.Count == 0)
            {
                rsi.Message = "O Supervisor " + worker.UserName + " não está a supervisionar em nenhuma linha hoje";
            }
            else
            {
                rsi.Message = "O Supervisor " + worker.UserName + " está a supervisionar " + WorkLinesday.ToString() + " linhas hoje";
            }

            return Ok(rsi);
        }


        //método que retorna a lista de produção da linha x no turno atual
        [HttpGet]
        [Route("GetProductionsInfo")]
        public async Task<IActionResult> GetProductionsInfo(int LineId, DateTime? dtInitial, DateTime? dtFinal)
        {
            ResponseGetProductionsInfo rgpi = new ResponseGetProductionsInfo();
            List<Line> listLines = new List<Line>();
            List<Production> listProductions = new List<Production>();

            listLines = await _service.GetLines();
            listProductions = await _service.GetProductions();
            
            var line = listLines.Where(l => l.Id == LineId).FirstOrDefault();
            if (line == null)
            {
                rgpi.Message = "Erro ao identificar a Line!!";

                return NotFound(rgpi);
            }

            WorkShift ws = _systemLogic.GetAtualWorkShift(DateTime.Now);
            //verificar as linhas de produção de um horaio de trabalho
            if(!dtInitial.HasValue && !dtFinal.HasValue)
            {
                var productions = listProductions.Where(p => p.LineId == line.Id && ws.IsProductionInWorkshift(p) == false).ToList();
                rgpi.listProductions = productions;
            }
            else
            {
                var productions = listProductions.Where(p => p.LineId == line.Id && _systemLogic.IsAtributeInDatetime(dtInitial, dtFinal, p.InitialDate, p.EndDate) == true).ToList();
                rgpi.listProductions = productions;
            }
            
            rgpi.Message = "Info Obtida com sucesso!!";
            return Ok(rgpi);

        }


        //método que retorna a lista de components a partir do id do device
        [HttpGet]
        [Route("GetComponentsDeviceInfo")]
        public async Task<IActionResult> GetComponentsDeviceInfo(string DeviceIdFirebase)
        {
            ResponseGetComponentsDeviceInfo rgcdi = new ResponseGetComponentsDeviceInfo();

            List<Device> listDevices = new List<Device>();
            List<Line> listLines = new List<Line>();
            List<Production> listProdutions = new List<Production>();
            List<Product> listProducts = new List<Product>();
            List<Component> listComponents = new List<Component>();
            List<Product_Component> listProduct_Components = new List<Product_Component>();

            listDevices = await _service.GetDevices();
            listLines = await _service.GetLines();
            listProdutions = await _service.GetProductions();
            listProducts = await _service.GetProducts();
            listComponents = await _service.GetComponents();
            listProduct_Components = await _service.GetProduct_Components();

            //encontrar o device
            var device = listDevices.Where(d => d.IdFirebase == DeviceIdFirebase).FirstOrDefault();
            if (device == null)
            {
                rgcdi.Message = "Erro ao identificar o Device!!";
                return NotFound(rgcdi);
            }
            var _line = listLines.Where(l => l.Id == device.LineId).FirstOrDefault();
            if (_line == null)
            {
                rgcdi.Message = "Erro ao identificar a linha de produção!!";
                return NotFound(rgcdi);
            }
            var _production = listProdutions.Where(p => p.LineId == _line.Id && _systemLogic.dateTimeIsActiveNow(p.InitialDate, p.EndDate) == true).FirstOrDefault();
            if (_production == null)
            {
                rgcdi.Message = "Erro ao identificar a linha de produção!!";
                return NotFound(rgcdi);
            }
            //encontrar o product pois é lá que tem a a product reference
            var product = listProducts.Where(p => p.Id == _production.ProductId).FirstOrDefault();
            if (product == null)
            {
                rgcdi.Message = "Erro ao identificar a product!!";
                return NotFound(rgcdi);
            }
            Product_Component pcn = new Product_Component();

            var components = listComponents.Where(c => listProduct_Components.SingleOrDefault(pc=>pc.ProductId == product.Id && pc.ComponentId == c.Id) != null).ToList();

            rgcdi.listComponents = components;
            rgcdi.Message = "Info obtida com sucesso!!";
            return Ok(rgcdi);
        }

        [HttpGet]
        [Route("GetUsersFirebase")]
        public async Task<IActionResult> GetUsersFirebase()
        {
            //https://www.youtube.com/watch?v=Fgrpbesqc6A

            return Ok();
        }
    }
}
