using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContinentalTestDb.Data;
using Models.ContinentalModels;
using ContinentalTestDb.Services;
using Newtonsoft.Json;

namespace ContinentalTestDb.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly ContinentalTestDbContext _context;
        private readonly RabbitMqService _rabbit;

        public ComponentsController(ContinentalTestDbContext context, RabbitMqService rabbit)
        {
            _context = context;
            _rabbit = rabbit;
        }

        // GET: Components
        public async Task<IActionResult> Index()
        {
              return View(await _context.Components.ToListAsync());
        }

        // GET: Components/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Components == null)
            {
                return NotFound();
            }

            var component = await _context.Components
                .FirstOrDefaultAsync(m => m.Id == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // GET: Components/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Components/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Reference,Category")] Component component)
        {
            if (ModelState.IsValid)
            {
                component.LastUpdate = DateTime.Now;
                _context.Add(component);
                await _context.SaveChangesAsync();

                //await _rabbit.PublishMessage(JsonConvert.SerializeObject(component),"create.component");

                return RedirectToAction(nameof(Index));
            }
            return View(component);
        }

        // GET: Components/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Components == null)
            {
                return NotFound();
            }

            var component = await _context.Components.FindAsync(id);
            if (component == null)
            {
                return NotFound();
            }
            return View(component);
        }

        // POST: Components/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Reference,Category")] Component component)
        {
            if (id != component.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    component.LastUpdate = DateTime.Now;
                    _context.Update(component);
                    await _context.SaveChangesAsync();
                    //await _rabbit.PublishMessage(JsonConvert.SerializeObject(component), "update.component");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentExists(component.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(component);
        }

        // GET: Components/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Components == null)
            {
                return NotFound();
            }

            var component = await _context.Components
                .FirstOrDefaultAsync(m => m.Id == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // POST: Components/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Components == null)
            {
                return Problem("Entity set 'ContinentalTestDbContext.Components'  is null.");
            }
            var component = await _context.Components.FindAsync(id);
            if (component != null)
            {   
                _context.Components.Remove(component);
                //await _rabbit.PublishMessage(JsonConvert.SerializeObject(component), "delete.component");
            }            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentExists(int id)
        {
          return _context.Components.Any(e => e.Id == id);
        }
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        //Editar os components do produto
        // GET: Components/Edit/5
        public async Task<IActionResult> EditComponentProducts(int? id)
        {
            var product = await _context.Products
                .Include(s => s.ComponentProducts)
               .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            List<Component> listcomps = new List<Component>();
            listcomps = _context.Components.ToList();
            foreach (var comp in listcomps)
            {
                foreach (var compprod in product.ComponentProducts.ToList())
                {
                    if (comp.Id == compprod.ComponentId)
                    {
                        comp.IsSelected = true;
                    }
                }
            }
            ViewData["ProductId"] = product.Id;
            ViewData["ProductName"] = product.Name;

            //return View(product.Components.ToList());
            return View(listcomps);
        }

        //--------------------AdicionarComponent-------------------------------------------------------
        public async Task<IActionResult> AdicionarComponent(int? idProd, int? idComp)
        {
            if (idProd == null || idComp == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == idProd);
            var component = await _context.Components.FirstOrDefaultAsync(c => c.Id == idComp);
            if (product == null || component == null)
            {
                return NotFound();
            }
            else
            {
                //adicionar componentproduct à tabela de relação

                ComponentProduct cp = new ComponentProduct();
                cp.Product = product;
                cp.Component = component;
                cp.ProductId = product.Id;
                cp.ComponentId = component.Id;
                cp.Quantidade = 1;
                cp.LastUpdate = DateTime.Now;

                _context.Add(cp);
                await _context.SaveChangesAsync();
            }


            //---------


            return RedirectToAction("EditComponentProducts", "Components", new { id = idProd });
        }

        //--------------------RemoverComponent-------------------------------------------------------
        public async Task<IActionResult> RemoverComponent(int? idProd, int? idComp)
        {
            if (idProd == null || idComp == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == idProd);

            var component = await _context.Components.FirstOrDefaultAsync(c => c.Id == idComp);

            if (product == null || component == null)
            {
                return NotFound();
            }
            else
            {
                var cp = await _context.ComponentProducts.FirstOrDefaultAsync(m => m.ComponentId == component.Id && m.ProductId == product.Id);
                if(cp != null)
                {
                    _context.Remove(cp);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("EditComponentProducts", "Components", new { id = idProd });
        }



    }
}
