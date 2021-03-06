﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DatingApp.Models;
using DatingApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DatingApp.Controllers
{
 
    [Route("Values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            this._context = context;
        }
        // GET: api/Values
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await _context.Users.ToListAsync();
            return Ok(values);
        }

        // GET: api/Values/5
        [AllowAnonymous]
        [HttpGet("{id}")] 
        public async Task <IActionResult> GetValues(int Id)
        {
            var values =await _context.Values
                .FirstOrDefaultAsync(x=> x.Id == Id);
            return Ok(values);
        }

        // POST: api/Values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
