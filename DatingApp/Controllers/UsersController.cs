using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Data;
using DatingApp.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{

    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _maper;
        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _maper = mapper;
        }
        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _repo.GetUsers();
            var userToReturn = _maper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(userToReturn);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _repo.GetUser(id);
            var userToReturn = _maper.Map<UserForDetailedDto>(user);
            return Ok(userToReturn);
        }

        // POST: api/Users
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int Id, [FromBody]UserForUpdateDto userForUpdatreDto)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return BadRequest();
            }

            if (Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var userFromRepo = await _repo.GetUser(Id);
            _maper.Map(userForUpdatreDto, userFromRepo);
            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception($"Updating User {Id} Failed on save ");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
