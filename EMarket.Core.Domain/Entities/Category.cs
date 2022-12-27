using EMarket.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Core.Domain.Entities
{
    public class Category : AuditableBaseEntity
    {


        public string Name { get; set; }

        public string Description { get; set; }


        public ICollection<Advert>? Adverts { get; set; }
    }
}
