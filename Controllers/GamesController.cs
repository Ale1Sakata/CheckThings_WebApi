using System;
using System.Collections.Generic;
using System.IO;
using CheckThingsAPI.Entities;
using CheckThingsAPI.Models;
using CheckThingsAPI.Repository.Interfaces;
using CheckThingsAPI.Services;
using CheckThingsAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CheckThingsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IProductsService<Game> _gameService;

        public GamesController(IProductsService<Game> gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public ActionResult<List<Game>> GetAllGames()
        {
            List<Game> games = _gameService.GetAll();

            if (games == null)
                return NotFound();

            return games;
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<Game> GetGameById(string id)
        {
            Game games = _gameService.GetById(id);

            if (games == null)
                return NotFound();

            return games;
        }

        [HttpPost]
        public ActionResult<Game> CreateGame([FromBody] Game game)
        {
            _gameService.Insert(game);

            return Ok();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateGame(string id, [FromBody] Game gameToUp)
        {
            Game game = _gameService.GetById(id);

            if (game == null)
                return NotFound();

            _gameService.Update(id, gameToUp);

            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteGame(string id)
        {
            Game game = _gameService.GetById(id);

            if (game == null)
                return NotFound();

            _gameService.Delete(id);

            return Ok();
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("UploadImage")]
        public ActionResult<ImageUpload> UploadImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "ImagesThings/Games");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var dbPath = _gameService.UploadFile(file, folderName, pathToSave);

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
