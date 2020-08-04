using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data
{
    public class ECommerceInitializer : CreateDatabaseIfNotExists<ECommerceDbContext>
    {
        protected override void Seed(ECommerceDbContext context)
        {
            base.Seed(context);
        }
    }
}
