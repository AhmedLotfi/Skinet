using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications.App;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product, long> _repoProduct;
        private readonly IGenericRepository<ProductBrand, int> _repoProductBrand;
        private readonly IGenericRepository<ProductType, int> _repoProductType;

        public ProductsController(IGenericRepository<Product, long> repoProduct,
                 IGenericRepository<ProductBrand, int> repoProductBrand, IGenericRepository<ProductType, int> repoProductType)
        {
            _repoProduct = repoProduct;
            _repoProductBrand = repoProductBrand;
            _repoProductType = repoProductType;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var spec = new ProductsWithBrandsAndTypesSpecification();

            return Ok(await _repoProduct.GetAllAsync(spec));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(long id)
        {
            var spec = new ProductsWithBrandsAndTypesSpecification(id);

            return Ok(await _repoProduct.GetEntityWithSpec(spec));
        }

        [HttpGet("[Action]")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductsBrands()
         => Ok(await _repoProductBrand.GetAllAsync());


        [HttpGet("[Action]")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductsTypes()
         => Ok(await _repoProductType.GetAllAsync());
    }
}