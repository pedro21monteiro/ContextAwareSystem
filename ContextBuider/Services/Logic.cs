using ContextBuider.Data;
using ContextBuider.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ContextBuider.Services
{
    public class Logic : ILogic
    {
        

        public async Task readMessage(string routingKey, string message, ContextAwareDb _context)
        {

            if(routingKey != "" && routingKey != null && message != "" && message != null)
            {
                //ler e separar a routing key
                string[] valores = routingKey.Split('.');
                string metodo = valores[0];
                string classe = valores[1];
                //buscar a classe

                switch (classe)
                {                  
                    case "component":
                       
                        var component = GetComponent(message);
                        if (metodo == "create")
                        {
                            _context.Add(component);
                            await _context.SaveChangesAsync();
                            Console.WriteLine(message + " - Adicionado com suceso");
                        }
                        if (metodo == "update")
                        {
                            var comp = _context.Components.SingleOrDefault(c => c.Id == component.Id);
                            if(comp != null)
                            {
                                comp.Name = component.Name;
                                comp.Reference = component.Reference;
                                comp.Category = component.Category;

                                _context.Update(comp);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Atualizado com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao atualizar ");
                            }
                            
                        }
                        if (metodo == "delete")
                        {
                            var comp = _context.Components.SingleOrDefault(c => c.Id == component.Id);
                            if (comp != null)
                            {
                                _context.Remove(comp);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Removido com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao remover ");
                            }
                        }
                        break;
                    case "coordinator":
                        var coordinator = GetCoordinator(message);
                        if (metodo == "create")
                        {
                            var wor = _context.Workers.SingleOrDefault(w => w.Id == coordinator.WorkerId);
                            if (wor != null)
                            {
                                coordinator.Worker = wor;
                                coordinator.WorkerId = wor.Id;
                                _context.Add(coordinator);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Adicionado com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao adicionar");
                            }

                        }
                        if (metodo == "update")
                        {
                            var coord = _context.Coordinators.SingleOrDefault(c => c.Id == coordinator.Id);
                            if (coord != null)
                            {
                                var wor = _context.Workers.SingleOrDefault(w => w.Id == coordinator.WorkerId);
                                if(wor != null)
                                {
                                    coord.Worker = wor;
                                    coord.WorkerId = wor.Id;

                                    _context.Update(coord);
                                    await _context.SaveChangesAsync();
                                    Console.WriteLine(message + " - Atualizado com suceso");
                                }
                                else
                                {
                                    Console.WriteLine(message + " - Erro ao atualizar ");
                                }                               
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao atualizar ");
                            }

                        }
                        if (metodo == "delete")
                        {
                            var coord = _context.Coordinators.SingleOrDefault(c => c.Id == coordinator.Id);
                            if (coord != null)
                            {
                                _context.Remove(coord);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Removido com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao remover ");
                            }
                        }
                        break;
                    case "device":
                        var device = GetDevice(message);
                        if (metodo == "create")
                        {
                            var li = _context.Lines.SingleOrDefault(c => c.Id == device.LineId);
                            if (li != null)
                            {
                                device.Line = li;
                                device.LineId = li.Id;                               
                                _context.Add(device);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Adicionado com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao adicionar");
                            }

                        }
                        if (metodo == "update")
                        {
                            var dev = _context.Devices.SingleOrDefault(c => c.Id == device.Id);
                            if (dev != null)
                            {
                                var li = _context.Lines.SingleOrDefault(c => c.Id == device.LineId);
                                if (li != null)
                                {
                                    dev.Line = li;
                                    dev.LineId = li.Id;
                                    dev.Type = device.Type;

                                    _context.Update(dev);
                                    await _context.SaveChangesAsync();
                                    Console.WriteLine(message + " - Atualizado com suceso");
                                }
                                else
                                {
                                    Console.WriteLine(message + " - Erro ao atualizar");
                                }
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao atualizar ");
                            }
                        }
                        if (metodo == "delete")
                        {
                            var dev = _context.Devices.SingleOrDefault(c => c.Id == device.Id);
                            if (dev != null)
                            {
                                _context.Remove(dev);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Removido com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao remover ");
                            }
                        }
                        break;
                    case "line":
                        var line = GetLine(message);
                        if (metodo == "create")
                        {
                            var coord = _context.Coordinators.SingleOrDefault(c => c.Id == line.CoordinatorId);
                            if(coord != null)
                            {
                                line.Coordinator = coord;
                                line.CoordinatorId = coord.Id;
                                _context.Add(line);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Adicionado com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao adicionar");
                            }
                        }
                        if (metodo == "update")
                        {
                            var l = _context.Lines.SingleOrDefault(c => c.Id == line.Id);
                            if (l != null)
                            {
                                var coord = _context.Coordinators.SingleOrDefault(c => c.Id == line.CoordinatorId);
                                if( coord != null)
                                {
                                    line.Coordinator = coord;
                                    line.CoordinatorId = coord.Id;

                                    l.Name = line.Name;
                                    l.CoordinatorId = line.CoordinatorId;

                                    _context.Update(l);
                                    await _context.SaveChangesAsync();
                                    Console.WriteLine(message + " - Atualizado com suceso");
                                }
                                else
                                {
                                    Console.WriteLine(message + " - Erro ao atualizar ");
                                }

                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao atualizar ");
                            }
                        }
                        if (metodo == "delete")
                        {
                            var l = _context.Lines.SingleOrDefault(c => c.Id == line.Id);
                            if (l != null)
                            { 
                                _context.Remove(l);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Removido com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao remover");
                            }
                        }
                        break;
                    case "operator":
                        var operato = GetOperator(message);
                        if (metodo == "create")
                        {
                            var wor = _context.Workers.SingleOrDefault(w => w.Id == operato.WorkerId);
                            if (wor != null)
                            {
                                operato.Worker = wor;
                                operato.WorkerId = wor.Id;
                                _context.Add(operato);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Adicionado com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao adicionar");
                            }
                        }
                        if (metodo == "update")
                        {
                            var o = _context.Operators.SingleOrDefault(c => c.Id == operato.Id);
                            if (o != null)
                            {
                                o.Worker = operato.Worker;
                                o.WorkerId = operato.WorkerId;

                                _context.Update(o);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Atualizado com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao atualizar ");
                            }
                        }
                        if (metodo == "delete")
                        {
                            var o = _context.Operators.SingleOrDefault(c => c.Id == operato.Id);
                            if (o != null)
                            {
                                _context.Remove(o);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Removido com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao remover ");
                            }
                        }
                        break;
                    case "product":
                        var product = GetProduct(message);
                        if (metodo == "create")
                        {

                            _context.Add(product);
                            await _context.SaveChangesAsync();
                            Console.WriteLine(message + " - Adicionado com suceso");
                        }
                        if (metodo == "update")
                        {
                            var p = _context.Products.SingleOrDefault(c => c.Id == product.Id);
                            if (p != null)
                            {
                                p.Name = product.Name;
                                p.LabelReference = product.LabelReference;
                                p.Cycle = product.Cycle;
                                p.Components.Clear();
                                foreach (var comp in product.Components)
                                {
                                    var c = _context.Components.SingleOrDefault(c => c.Id == comp.Id);
                                    if (c != null)
                                    {
                                        p.Components.Add(c);
                                    }
                                }

                                _context.Update(p);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Atualizado com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao atualizar ");
                            }
                        }
                        if (metodo == "delete")
                        {
                            var p = _context.Products.SingleOrDefault(c => c.Id == product.Id);
                            if (p != null)
                            {
                                _context.Remove(p);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Removido com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao remover ");
                            }
                        }
                        break;
                    case "production":
                        var production = GetProduction(message);
                        if (metodo == "create")
                        {
                            var pd = _context.Production_Plans.SingleOrDefault(p=>p.Id == production.Production_PlanId);
                            if (pd != null)
                            {
                                production.Prod_Plan = pd;
                                _context.Add(production);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Adicionado com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao adicionar");
                            }
                        }
                        if (metodo == "update")
                        {
                            var p = _context.Productions.SingleOrDefault(c => c.Id == production.Id);
                            if (p != null)
                            {
                                var pd = _context.Production_Plans.SingleOrDefault(p => p.Id == production.Production_PlanId);
                                if (pd != null)
                                {
                                    p.Prod_Plan = pd;
                                    p.Hour = production.Hour;
                                    p.Day = production.Day;
                                    p.Quantity = production.Quantity;
                                    _context.Update(p);
                                    await _context.SaveChangesAsync();
                                    Console.WriteLine(message + " - Atualizado com suceso");
                                }
                                else
                                {
                                    Console.WriteLine(message + " - Erro ao atualizar");
                                }
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao atualizar ");
                            }
                        }
                        if (metodo == "delete")
                        {
                            var p = _context.Productions.SingleOrDefault(c => c.Id == production.Id);
                            if (p != null)
                            {
                                _context.Remove(p);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Removido com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao remover ");
                            }
                        }
                        break;
                    case "production_plan":
                        var production_plan = GetProduction_Plan(message);
                        if (metodo == "create")
                        {
                            var produ= _context.Products.SingleOrDefault(p => p.Id == production_plan.ProductId);
                            var lin = _context.Lines.SingleOrDefault(l => l.Id == production_plan.LineId);
                            if(lin != null && produ != null)
                            {
                                production_plan.Line = lin;
                                production_plan.Product = produ;
                                _context.Add(production_plan);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Adicionado com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao adicionar");
                            }
                            
                        }
                        if (metodo == "update")
                        {
                            var p = _context.Production_Plans.SingleOrDefault(c => c.Id == production_plan.Id);
                            if (p != null)
                            {
                                var produ = _context.Products.SingleOrDefault(p => p.Id == production_plan.ProductId);
                                var lin = _context.Lines.SingleOrDefault(l => l.Id == production_plan.LineId);
                                if (lin != null && produ != null)
                                {
                                    p.Goal = production_plan.Goal;
                                    p.Name = production_plan.Name;
                                    p.InitialDate = production_plan.InitialDate;
                                    p.EndDate = production_plan.EndDate;
                                    p.Shift = production_plan.Shift;
                                    p.Line = lin;
                                    p.Product = produ;
                                    _context.Update(p);
                                    await _context.SaveChangesAsync();
                                    Console.WriteLine(message + " - Atualizado com suceso");
                                }
                                else
                                {
                                    Console.WriteLine(message + " - Erro ao atualizar");
                                }
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao atualizar ");
                            }
                        }
                        if (metodo == "delete")
                        {
                            var p = _context.Production_Plans.SingleOrDefault(c => c.Id == production_plan.Id);
                            if (p != null)
                            {
                                _context.Remove(p);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Removido com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao remover ");
                            }
                        }
                        break;
                    case "reason":
                        var reason = GetReason(message);
                        if (metodo == "create")
                        {
                            _context.Add(reason);
                            await _context.SaveChangesAsync();
                            Console.WriteLine(message + " - Adicionado com suceso");
                        }
                        if (metodo == "update")
                        {
                            var r = _context.Reasons.SingleOrDefault(c => c.Id == reason.Id);
                            if (r != null)
                            {
                                r.Description = reason.Description;
                                _context.Update(r);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Atualizado com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao atualizar ");
                            }
                        }
                        if (metodo == "delete")
                        {
                            var r = _context.Reasons.SingleOrDefault(c => c.Id == reason.Id);
                            if (r != null)
                            {
                                _context.Remove(r);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Removido com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao Remover ");
                            }
                        }
                        break;
                    case "swl":
                        var swl = GetSchedule_Worker_Line(message);
                        if (metodo == "create")
                        {
                            var l = _context.Lines.SingleOrDefault(l => l.Id == swl.LineId);
                            var o = _context.Operators.SingleOrDefault(o => o.Id == swl.OperatorId);
                            var s = _context.Supervisors.SingleOrDefault(s => s.Id == swl.SupervisorId);
                            if (l != null)
                            {
                                swl.Line = l;
                                swl.Operator = o;
                                swl.Supervisor = s;
                                _context.Add(swl);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Adicionado com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao adicionar");
                            }
                        }
                        if (metodo == "update")
                        {
                            var s = _context.Schedule_Worker_Lines.SingleOrDefault(c => c.Id == swl.Id);                          
                            if (s != null)
                            {
                                var l = _context.Lines.SingleOrDefault(l => l.Id == swl.LineId);
                                var o = _context.Operators.SingleOrDefault(o => o.Id == swl.OperatorId);
                                var sup = _context.Supervisors.SingleOrDefault(s => s.Id == swl.SupervisorId);
                                if (l != null)
                                {
                                    s.Day = swl.Day;
                                    s.Shift = swl.Shift;
                                    s.Line = l;
                                    if(o!= null)
                                    {
                                        s.Operator = o;
                                    }
                                    if (sup != null)
                                    {
                                        s.Supervisor = sup;
                                    }
                                    _context.Update(s);
                                    await _context.SaveChangesAsync();
                                    Console.WriteLine(message + " - Adicionado com suceso");
                                }
                                else
                                {
                                    Console.WriteLine(message + " - Erro ao adicionar");
                                }

                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao atualizar ");
                            }
                        }
                        if (metodo == "delete")
                        {
                            var s = _context.Schedule_Worker_Lines.SingleOrDefault(c => c.Id == swl.Id);
                            if (s != null)
                            {   
                                _context.Remove(s);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Removido com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao remover ");
                            }
                        }
                        break;
                    case "stop":
                        var stop = GetStop(message);
                        if (metodo == "create")
                        {
                            var l = _context.Lines.SingleOrDefault(l => l .Id == stop.LineId);
                            var r = _context.Reasons.SingleOrDefault(r => r.Id == stop.ReasonId);

                            if(l != null)
                            {
                                stop.Line = l;
                                stop.LineId = l.Id;
                                if (r != null)
                                {
                                    stop.Reason = r;
                                    stop.ReasonId = r.Id;
                                    
                                }
                                _context.Add(stop);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Adicionado com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao adicionar");
                            }
                          
                        }
                        if (metodo == "update")
                        {
                            var s = _context.Stops.SingleOrDefault(c => c.Id == stop.Id);
                            if(s!= null)
                            {
                                var l = _context.Lines.SingleOrDefault(l => l.Id == stop.LineId);
                                var r = _context.Reasons.SingleOrDefault(r => r.Id == stop.ReasonId);

                                if (l != null)
                                {
                                    s.Planned = stop.Planned;
                                    s.InitialDate = stop.InitialDate;
                                    s.EndDate = stop.EndDate;
                                    s.Duration = stop.Duration;
                                    s.Shift = stop.Shift;
                                    s.Line = l;
                                    s.LineId = l.Id;
                                    if (r != null)
                                    {
                                        s.Reason = r;
                                        s.ReasonId = r.Id;

                                    }
                                    _context.Update(s);
                                    await _context.SaveChangesAsync();
                                    Console.WriteLine(message + " - Adicionado com suceso");
                                }
                                else
                                {
                                    Console.WriteLine(message + " - Erro ao adicionar");
                                }
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao adicionar");
                            }
                                                    
                        }
                        if (metodo == "delete")
                        {
                            var s = _context.Stops.SingleOrDefault(c => c.Id == stop.Id);
                            if (s != null)
                            {
                                _context.Remove(s);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Removido com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao remover ");
                            }
                        }
                        break;
                    case "supervisor":
                        var supervisor = GetSupervisor(message);
                        if (metodo == "create")
                        {
                            var wor = _context.Workers.SingleOrDefault(w => w.Id == supervisor.WorkerId);
                            if (wor != null)
                            {
                                supervisor.Worker = wor;
                                supervisor.WorkerId = wor.Id;
                                _context.Add(supervisor);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Adicionado com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao adicionar");
                            }
                        }
                        if (metodo == "update")
                        {
                            var s = _context.Supervisors.SingleOrDefault(c => c.Id == supervisor.Id);
                            if (s != null)
                            {
                                s.Worker = supervisor.Worker;
                                s.WorkerId = supervisor.WorkerId;

                                _context.Update(s);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Atualizado com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao atualizar ");
                            }
                        }
                        if (metodo == "delete")
                        {
                            var s = _context.Supervisors.SingleOrDefault(c => c.Id == supervisor.Id);
                            if (s != null)
                            {
                                _context.Remove(s);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Removido com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao Remover ");
                            }
                        }
                        break;
                    case "worker":
                        var worker = GetWorker(message);
                        if (metodo == "create")
                        {
                            _context.Add(worker);
                            await _context.SaveChangesAsync();
                            Console.WriteLine(message + " - Adicionado com suceso");
                        }
                        if (metodo == "update")
                        {
                            var w = _context.Workers.SingleOrDefault(c => c.Id == worker.Id);
                            if (w != null)
                            {
                                w.IdFirebase = worker.IdFirebase;
                                w.UserName = worker.UserName;
                                w.Email = worker.Email;
                                w.Role = worker.Role;

                                _context.Update(w);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Atualizado com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao atualizar ");
                            }
                        }
                        if (metodo == "delete")
                        {
                            var w = _context.Workers.SingleOrDefault(c => c.Id == worker.Id);
                            if (w != null)
                            {
                                _context.Remove(w);
                                await _context.SaveChangesAsync();
                                Console.WriteLine(message + " - Removido com suceso");
                            }
                            else
                            {
                                Console.WriteLine(message + " - Erro ao remover ");
                            }
                        }
                        break;
                    default:

                        break;


                }


            }
        }
        //----------------- Gets das classes
        public Component GetComponent(string message)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<Component>(message);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public Coordinator GetCoordinator(string message)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<Coordinator>(message);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public Device GetDevice(string message)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<Device>(message);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public Line GetLine(string message)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<Line>(message);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Operator GetOperator(string message)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<Operator>(message);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public Product GetProduct(string message)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<Product>(message);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Production GetProduction(string message)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<Production>(message);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public Production_Plan GetProduction_Plan(string message)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<Production_Plan>(message);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public Reason GetReason(string message)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<Reason>(message);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public Schedule_Worker_Line GetSchedule_Worker_Line(string message)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<Schedule_Worker_Line>(message);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public Stop GetStop(string message)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<Stop>(message);
                return result;
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }
        public Supervisor GetSupervisor(string message)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<Supervisor>(message);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public Worker GetWorker(string message)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<Worker>(message);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }
}
