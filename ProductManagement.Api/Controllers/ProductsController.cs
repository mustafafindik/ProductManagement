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

            var result = _productService.GetImagesById(id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(new { Message = result.Message });
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
            public string productId { get; set; }
        }

        [HttpPost("UploadImages")]
      

        public IActionResult AddImages([FromForm] FileDto fileDto)
        {

            var file = fileDto.File;
            var productId = Convert.ToInt32(fileDto.productId);
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);


            if (file.Length > 0)
            {
                var result = _productService.UploadImage(productId, file, folderName, pathToSave);

                if (result.IsSuccess)
                {
                    return Ok(new { Message = result.Message });
                }
                return BadRequest(new { Message = result.Message });
            }

            return BadRequest("Dosya Yok");


        }

        [HttpPost("add")]
    

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
