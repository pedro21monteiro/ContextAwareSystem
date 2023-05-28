﻿using ContextBuilder.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.ContextModels;

namespace ContextBuilder.Controllers
{
    [Route("api/ContextBuilder")]
    [ApiController]

    public class ContextBuilderController : Controller
    {
        private readonly ContextBuilderDb _context;

        public ContextBuilderController(ContextBuilderDb context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("CreateResquest")]
        public async Task<ActionResult> CreateResquest([FromBody] Request request)
        {
            try
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
                Console.WriteLine("Request: " + request.Id.ToString() + " - Adicionado com Sucesso");
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Request: " + request.Id.ToString() + " - Erro ao adicionar");
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

        //vou ter de criar uma função que elimina os requests que com mais de 1 ano e 8 meses
      
    }    
}
