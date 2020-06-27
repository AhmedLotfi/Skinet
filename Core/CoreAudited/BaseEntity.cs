using Core.InterfacesUtilites;

namespace CoreAudited
{
    public class BaseEntity<TPrimaryKey> : IPrimaryKey<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}