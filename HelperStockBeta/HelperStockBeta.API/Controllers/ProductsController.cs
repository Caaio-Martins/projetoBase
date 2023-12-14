﻿using HelperStockBeta.API.Controllers;
using HelperStockBeta.Application.DTOs;
using HelperStockBeta.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HelperStockBeta.API.Controllers
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
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _productService.GetProducts();

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest("Invalid body Data");
            }

            await _productService.Add(productDTO);

            return new CreatedAtRouteResult("GetProduct", new { id = productDTO.Id }, productDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return BadRequest("Id Not Verified");
            }

            if (productDTO == null)
            {
                return BadRequest("DTO inspection failed");
            }

            await _productService.Update(productDTO);

            return Ok(productDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var product = await _productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productService.Remove(id);
            return Ok(product);
        }
    }
}