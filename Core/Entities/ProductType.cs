using Core.CoreAudited;

namespace Core.Entities
{
    public class ProductType : AuditedEntity<int>
    {
        public string Name { get; protected set; }
    }
}