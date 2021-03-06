using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {


            var result = _productService.GetAll();
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
