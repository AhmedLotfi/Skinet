using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications.App
{
    public class ProductsWithBrandsAndTypesSpecification : BaseSpecification<Product>
    {
        public ProductsWithBrandsAndTypesSpecification(ProductSpecParams productParams)
        : base(z =>
             (!productParams.BrandId.HasValue || z.ProductBrandId == productParams.BrandId) &&
             (!productParams.TypeId.HasValue || z.ProductTypeId == productParams.TypeId)
         )
        {
            AddInclude(z => z.ProductBrand);
            AddInclude(z => z.ProductType);
            AddOrderBy(z => z.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(z => z.Price); break;
                    case "priceDesc":
                        AddOrderByDesc(z => z.Price); break;
                    default:
                        AddOrderBy(z => z.Name); break;
                }
            }
        }

        public ProductsWithBrandsAndTypesSpecification(long id) : base(z => z.Id == id)
        {
            AddInclude(z => z.ProductBrand);
            AddInclude(z => z.ProductType);
        }
    }
}