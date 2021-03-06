using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using ProductManagement.Business.Abstract;
using ProductManagement.Entities.Concrete;

namespace ProductManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Get()
        {


            var result = _productService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(new { Message = result.Message });

        }

        [HttpGet("images/{id}")]
        public IActionResult GetImages(int id)
        {

            var images = new[] {
            new ProductImage() {  Id = 1, ImagePath = "https://miro.medium.com/max/1200/1*mk1-6aYaf_Bes1E3Imhc0A.jpeg"},
            new ProductImage() {  Id = 2, ImagePath = "https://miro.medium.com/max/1200/1*mk1-6aYaf_Bes1E3Imhc0A.jpeg"},
        };



            return Ok(images);
        }



        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _productService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(new { Message = result.Message });
        }


        public class FileDto
        {
            public IFormFile File { get; set; }

            public string FolderPath { get; set; }

            public string FileName { get; set; }
        }

        [HttpPost("UploadImages")]
        [Authorize(Roles = "Admin")]

        public IActionResult AddImages([FromForm] FileDto fileDto)
        {
            try
            {
                var files = Request.Form.Files;
                var folderName = Path.Combine("StaticFiles", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (files.Any(f => f.Length == 0))
                {
                    return BadRequest();
                }
                foreach (var file in files)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
                    var fullPath = Path.Combine(pathToSave.ToString(), fileName.ToString());
                    var dbPath = Path.Combine(folderName, fileName.ToString());
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Ok(new { Message = "result.Message" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]

        public IActionResult Add(Product product)
        {

            var result = _productService.Add(product);
            if (result.IsSuccess)
            {
                return Ok(new { Message = result.Message });
            }

            return BadRequest(new { Message = result.Message });
        }

        [HttpPost("update")]
        [Authorize(Roles = "Admin")]

        public IActionResult Update([FromBody] Product product)
        {


            var result = _productService.Update(product);
            if (result.IsSuccess)
            {
                return Ok(new { Message = result.Message });
            }

            return BadRequest(new { Message = result.Message });
        }


        [HttpPost("delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Product product)
        {
            var result = _productService.Delete(product);
            if (result.IsSuccess)
            {
                return Ok(new { Message = result.Message });
            }
            return BadRequest(new { Message = result.Message });
        }
    }
}
