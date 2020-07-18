using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications.App
{
    public class ProductsWithBrandsAndTypesSpecification : BaseSpecification<Product>
    {
        public ProductsWithBrandsAndTypesSpecification(string sort)
        {
            AddInclude(z => z.ProductBrand);
            AddInclude(z => z.ProductType);
            AddOrderBy(z => z.Name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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