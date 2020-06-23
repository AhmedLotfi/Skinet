using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications.App
{
    public class ProductsWithBrandsAndTypesSpecification : BaseSpecification<Product>
    {
        public ProductsWithBrandsAndTypesSpecification()
        {
            AddInclude(z => z.ProductBrand);
            AddInclude(z => z.ProductType);
        }

        public ProductsWithBrandsAndTypesSpecification(long id) : base(z => z.Id == id)
        {
            AddInclude(z => z.ProductBrand);
            AddInclude(z => z.ProductType);
        }
    }
}