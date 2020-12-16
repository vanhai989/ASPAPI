using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDApi.Data;
using CRUDApi.Models.Images;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CRUDApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageModelsController : ControllerBase
    {
        private readonly FormFileContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ImageModelsController(FormFileContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: api/ImageModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageModel>>> GetImageModels()
        {
            return await _context.ImageModels.ToListAsync();
        }

        // GET: api/ImageModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageModel>> GetImageModel(int id)
        {
            var imageModel = await _context.ImageModels.FindAsync(id);

            if (imageModel == null)
            {
                return NotFound();
            }

            return imageModel;
        }

        // PUT: api/ImageModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImageModel(int id, ImageModel imageModel)
        {
            if (id != imageModel.FormFileID)
            {
                return BadRequest();
            }

            if (imageModel.File != null)
            {
                DeleteImage(imageModel.Usename);
                imageModel.Usename = await SaveImage(imageModel.File);
            }

            _context.Entry(imageModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageModelExists(id))
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

        // POST: api/ImageModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImageModel>> PostImageModel(ImageModel imageModel)
        {
            _context.ImageModels.Add(imageModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImageModel", new { id = imageModel.FormFileID }, imageModel);
        }

        // post file
        [HttpPost("uploadFile")]
        #region
        public async Task<ActionResult<ImageModel>> PostImage([FromForm] ImageModel imageModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            imageModel.Usename = await SaveImage(imageModel.File);
            _context.ImageModels.Add(imageModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }

            return StatusCode(201);
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
        #endregion
        // DELETE: api/ImageModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImageModel(int id)
        {
            var imageModel = await _context.ImageModels.FindAsync(id);
            if (imageModel == null)
            {
                return NotFound();
            }

            _context.ImageModels.Remove(imageModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool ImageModelExists(int id)
        {
            return _context.ImageModels.Any(e => e.FormFileID == id);
        }
    }
}
