using CheckThingsAPI.Entities;
using CheckThingsAPI.Models;
using CheckThingsAPI.Repository.Interfaces;
using CheckThingsAPI.Services;
using CheckThingsAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CheckThingsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IProductsService<Film> _filmService;

        public FilmsController(IProductsService<Film> filmService)
        {
            _filmService = filmService;
        }

        [HttpGet]
        public ActionResult<List<Film>> GetAllFilms()
        {
            List<Film> films = _filmService.GetAll();

            if (films == null)
                return NotFound();

            return films;
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<Film> GetFilmById(string id)
        {
            Film films = _filmService.GetById(id);

            if (films == null)
                return NotFound();

            return films;
        }

        [HttpPost]
        public ActionResult<Film> CreateFilm([FromBody] Film film)
        {
            _filmService.Insert(film);

            return Ok();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateFilm(string id, Film filmToUp)
        {
            Film film = _filmService.GetById(id);

            if (film == null)
                return NotFound();

            _filmService.Update(id, filmToUp);

            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteFilm(string id)
        {
            Film film = _filmService.GetById(id);

            if (film == null)
                return NotFound();

            _filmService.Delete(id);

            return Ok();
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("UploadImage")]
        public ActionResult<ImageUpload> UploadImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "ImagesThings/Films");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var dbPath = _filmService.UploadFile(file, folderName, pathToSave);

                    return new ImageUpload() { dbPath = dbPath };
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
