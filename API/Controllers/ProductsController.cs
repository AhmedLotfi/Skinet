using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repoProduct;

        public ProductsController(IProductRepository repoProduct)
        {
            _repoProduct = repoProduct;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
         => Ok(await _repoProduct.GetProductsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(long id)
         => Ok(await _repoProduct.GetProductByIdAsync(id));


        [HttpGet("[Action]")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductsBrands()
         => Ok(await _repoProduct.GetProductsBrandsAsync());


        [HttpGet("[Action]")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductsTypes()
         => Ok(await _repoProduct.GetProductsTypesAsync());
    }
}