using Core.CoreAudited;

namespace Core.Entities
{
    public class ProductBrand : AuditedEntity<int>
    {
        public string Name { get; protected set; }
    }
}