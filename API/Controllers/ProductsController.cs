using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductsController(IMapper mapper, IGenericRepository<Product, long> repoProduct,
                 IGenericRepository<ProductBrand, int> repoProductBrand, IGenericRepository<ProductType, int> repoProductType)
        {
            _mapper = mapper;
            _repoProduct = repoProduct;
            _repoProductBrand = repoProductBrand;
            _repoProductType = repoProductType;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithBrandsAndTypesSpecification();

            var mapped = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(await _repoProduct.GetAllAsync(spec));

            return Ok(mapped);
        }

        [HttpGet("{id}")]
        public async Task<ProductToReturnDto> GetProducts(long id)
        {
            var spec = new ProductsWithBrandsAndTypesSpecification(id);

            var mapped = _mapper.Map<ProductToReturnDto>(await _repoProduct.GetEntityWithSpec(spec));

            return mapped;
        }

        [HttpGet("[Action]")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductsBrands()
         => Ok(await _repoProductBrand.GetAllAsync());


        [HttpGet("[Action]")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductsTypes()
         => Ok(await _repoProductType.GetAllAsync());
    }
}