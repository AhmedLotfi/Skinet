using System;
using Core.InterfacesUtilites;

namespace Core.CoreAudited
{
    public class AuditedEntity<TPrimaryKey> : IPrimaryKey<TPrimaryKey>, ICreationTime
    {
        public TPrimaryKey Id { get; set; }

        public DateTime CreationTime { get; set; }
    }
}