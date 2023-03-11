using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MasterAbp.Categories
{
    public class Category : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
    }
}
