using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
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
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithBrandsAndTypesSpecification(productParams);

            var countSpec = new ProductWithFiltersForCountSpecifications(productParams);

            var totalItems = await _repoProduct.CountAsync(countSpec);

            var mappedData = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(await _repoProduct.GetAllAsync(spec));

            var returnValue = new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, mappedData);

            return Ok(returnValue);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProducts(long id)
        {
            var spec = new ProductsWithBrandsAndTypesSpecification(id);

            var current = await _repoProduct.GetEntityWithSpec(spec);

            if (current == null) return NotFound(new ApiResponse(404));

            var mapped = _mapper.Map<ProductToReturnDto>(current);

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