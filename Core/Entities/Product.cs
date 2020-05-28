using Core.CoreAudited;

namespace Core.Entities
{
    public class Product : AuditedEntity<long>
    {
        // [Required]
        //[MaxLength(200)]
        public string Name { get; protected set; }

        // [MaxLength(200)]
        public string NameAr { get; protected set; }
    }
}