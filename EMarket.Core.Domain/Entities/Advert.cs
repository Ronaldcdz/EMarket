using EMarket.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Core.Domain.Entities
{
    public class Advert : AuditableBaseEntity
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }


        //public int ImageId { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }

        public int UserId { get; set; }


        // Navigation Property
        public Category? Category { get; set; }


        public User? User { get; set; }

        //public ICollection<Image>? Images { get; set; }




    }
}
