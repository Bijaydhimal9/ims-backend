using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class AuditableEntity : IdentifiableEntity
    {

        public Guid CreatedBy { get; set; }


        public DateTime CreatedOn { get; set; }


        public Guid? UpdatedBy { get; set; }


        public DateTime? UpdatedOn { get; set; }


        protected AuditableEntity()
        {
        }
    }
}