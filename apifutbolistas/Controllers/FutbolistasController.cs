using apifutbolistas.Context;
using apifutbolistas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apifutbolistas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FutbolistasController : ControllerBase
    {
        private readonly AppDbContext context;
        public FutbolistasController(AppDbContext context)
        {
            this.context = context;
        }
        //GET: api/<controller>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.futbolistas.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //GET: api/<controller>/5
        [HttpGet("{id}",Name ="GetFutbolista")]
        public ActionResult Get(int id)
        {
            try
            {
                var futbolista = context.futbolistas.FirstOrDefault(f => f.id == id);
                return Ok(futbolista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]Futbolistas futbolistas)
        {
            try
            {
                context.futbolistas.Add(futbolistas);
                context.SaveChanges();
                return CreatedAtRoute("GetFutbolista",new { id=futbolistas.id},futbolistas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Futbolistas futbolistas)
        {
            try
            {
                if (futbolistas.id==id)
                {
                    context.Entry(futbolistas).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetFutbolista", new { id = futbolistas.id }, futbolistas);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var futbolistas = context.futbolistas.FirstOrDefault(f =>f.id == id);
                if (futbolistas != null)
                {
                    context.futbolistas.Remove(futbolistas);
                    context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
    }
}
