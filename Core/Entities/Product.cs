using Core.CoreAudited;

namespace Core.Entities
{
    public class Product : AuditedEntity<long>
    {
        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public decimal Price { get; protected set; }

        public string PictureUrl { get; protected set; }

        public ProductType ProductType { get; protected set; }

        public int ProductTypeId { get; protected set; }

        public ProductBrand ProductBrand { get; protected set; }

        public int ProductBrandId { get; protected set; }
    }
}