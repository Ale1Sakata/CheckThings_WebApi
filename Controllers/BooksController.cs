using CheckThingsAPI.Entities;
using CheckThingsAPI.Models;
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
    public class BooksController : ControllerBase
    {
        private readonly IProductsService<Book> _bookService;

        public BooksController(IProductsService<Book> bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> GetAllBooks()
        {
            List<Book> books = _bookService.GetAll();

            if (books == null)
                return NotFound();

            return books;
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<Book> GetBookById(string id)
        {
            Book books = _bookService.GetById(id);

            if (books == null)
                return NotFound();

            return books;
        }

        [HttpPost]
        public ActionResult<Book> CreateBook([FromBody] Book book)
        {
            _bookService.Insert(book);

            return Ok();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateBook(string id, Book bookToUp)
        {
            Book book = _bookService.GetById(id);

            if (book == null)
                return NotFound();

            _bookService.Update(id, bookToUp);

            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteBook(string id)
        {
            Book book = _bookService.GetById(id);

            if (book == null)
                return NotFound();

            _bookService.Delete(id);

            return Ok();
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("UploadImage")]
        public ActionResult<ImageUpload> UploadImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "ImagesThings/Books");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var dbPath = _bookService.UploadFile(file, folderName, pathToSave);

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
