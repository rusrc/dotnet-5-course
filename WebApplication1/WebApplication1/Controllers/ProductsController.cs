using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;

using WebApplication1.Domain;
using WebApplication1.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly AppDbContext _context;

        public ProductsController(ILogger<ProductsController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<ProductsController>
        /// <summary>
        /// Список продуктов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return this._context.Products;
        }

        // GET api/<ProductsController>/5
        // https://docs.microsoft.com/ru-ru/aspnet/core/web-api/action-return-types?view=aspnetcore-5.0#asynchronous-action
        /// <summary>
        /// Получаем продукт по id
        /// </summary>
        /// <param name="id">id продукта</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await this._context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST api/<ProductsController>
        /// <summary>
        /// Создаем продукт
        /// </summary>
        /// <param name="product">Продукт</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            this._context.Products.Add(product);
            await this._context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // PUT api/<ProductsController>/5
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromBody] Product product)
        {
            var currentProduct = await _context.Products.FindAsync(product.Id);
            if (currentProduct == null)
            {
                return NotFound();
            }

            currentProduct.Name = product.Name;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await this._context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Remove(product);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
