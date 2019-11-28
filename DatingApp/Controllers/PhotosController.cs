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

        public PhotosController(DataContext context, IDatingRepository repo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _repo = repo;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;
            _context = context;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKay,
                _cloudinaryConfig.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(acc);
        }

        // GET: api/Photos
        [HttpPost]
        public async Task<ActionResult> AddPhotoForUser(int userId, [FromForm]IFormFile File)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return BadRequest();
            }

            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var PhotoForCreationDto = new PhotoForPhotoCreationDto
            {
                File = File
            };

            var userFromRepo = await _repo.GetUser(userId);
            var localFile = PhotoForCreationDto.File;
            var uploadResult = new ImageUploadResult();
            if (localFile.Length > 0)
            { using (var stream = localFile.OpenReadStream())
                { var uploadParmas = new ImageUploadParams()
                {
                    File = new FileDescription(localFile.Name, stream),
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

                return Created($"/api/users/{userId}/photos/{photo.Id}", photoToReturn);
            }
            return BadRequest("Could Not Add The Photo");
        }

        // GET: api/Photos/5
        [HttpGet("{id}")]
        [ActionName("GetPhoto")]
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

        private bool PhotoExists(int id)
        {
            return _context.Photos.Any(e => e.Id == id);
        }
        [HttpPost("{id}/setMain")]
        public async Task<ActionResult> SetMainPhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            var user = await _repo.GetUser(userId);
            if (!user.Photos.Any(p => p.Id == id))
            {
                return Unauthorized();
            }
            var photoFromRepo = await _repo.GetPhoto(id);
            if (photoFromRepo.IsMain)
            {
                return BadRequest("This is already main photo");
            }
            var currentMainPhoto = await _repo.GetMainPhotoForUser(userId);
            currentMainPhoto.IsMain = false;
            photoFromRepo.IsMain = true;
            if (await _repo.SaveAll())
            {
                return NoContent();
            }
            return BadRequest("Could not set photo to main ");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int userId, int id) 
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            var user = await _repo.GetUser(userId);
            if (!user.Photos.Any(p => p.Id == id))
            {
                return Unauthorized();
            }
            var photoFromRepo = await _repo.GetPhoto(id);
            if (photoFromRepo.IsMain)
            {
                return BadRequest("You cant Delete Your main photo");
            }
            if (photoFromRepo.PublicId != null )
            {
                var deleParmas = new DeletionParams(photoFromRepo.PublicId);
                var results = _cloudinary.Destroy(deleParmas);
                if (results.Result == "ok")
                {
                    _repo.Delet(photoFromRepo);
                }

            }
            if (photoFromRepo.PublicId == null)
            {
                _repo.Delet(photoFromRepo);
            }
            if (await _repo.SaveAll())
            {
                return Ok();
            }

            return BadRequest("This photo can't be Deleted");
        }
    }
}
