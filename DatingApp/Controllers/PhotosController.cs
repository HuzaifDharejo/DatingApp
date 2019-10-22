using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatingApp.Data;
using DatingApp.Models;
using AutoMapper;
using Microsoft.Extensions.Options;
using DatingApp.Helpers;
using CloudinaryDotNet;
using DatingApp.Dtos;
using System.Security.Claims;
using CloudinaryDotNet.Actions;

namespace DatingApp.Controllers
{
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;

        private Cloudinary _cloudinary;

        public PhotosController(IDatingRepository repo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _repo = repo;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKay,
                _cloudinaryConfig.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(acc);
        }
        public PhotosController(DataContext context )
        {
            _context = context;
        }

        // GET: api/Photos
        [HttpPost]
        public async Task<ActionResult> AddPhotoForUser (int userId, PhotoForPhotoCreationDto PhotoForCreationDto)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return BadRequest();
            }

            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var userFromRepo = await _repo.GetUser(userId);
            var file = PhotoForCreationDto.File;
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            { using (var stream = file.OpenReadStream())
                { var uploadParmas = new ImageUploadParams()
                {
                    File = new FileDescription(file.Name, stream),
                    Transformation = new Transformation()
                    .Width(500).Height(500).Crop("fill").Gravity("face")

                };
                    uploadResult = _cloudinary.Upload(uploadParmas);
                }
                    

            }
            PhotoForCreationDto.Url = uploadResult.Uri.ToString();
            PhotoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(PhotoForCreationDto);
            if (!userFromRepo.Photos.Any(u => u.IsMain))
                photo.IsMain = true;


            userFromRepo.Photos.Add(photo);
            
            if (await _repo.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForPhotoReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoToReturn);
            }
            return BadRequest("Could Not Add The Photo");
        }

        // GET: api/Photos/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<PhotoForPhotoReturnDto>(photoFromRepo);
            return Ok(photo);
        }

        // PUT: api/Photos/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoto(int id, Photo photo)
        {
            if (id != photo.Id)
            {
                return BadRequest();
            }

            _context.Entry(photo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Photos
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Photo>> PostPhoto(Photo photo)
        {
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhoto", new { id = photo.Id }, photo);
        }

        // DELETE: api/Photos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Photo>> DeletePhoto(int id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo == null)
            {
                return NotFound();
            }

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();

            return photo;
        }

        private bool PhotoExists(int id)
        {
            return _context.Photos.Any(e => e.Id == id);
        }
    }
}
