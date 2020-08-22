using Core.Entities;

namespace Core.Specifications.App
{
    public class ProductWithFiltersForCountSpecifications : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecifications(ProductSpecParams productParams)
          : base(z =>
             (!productParams.BrandId.HasValue || z.ProductBrandId == productParams.BrandId) &&
             (!productParams.TypeId.HasValue || z.ProductTypeId == productParams.TypeId)
         )
        { }
    }
}