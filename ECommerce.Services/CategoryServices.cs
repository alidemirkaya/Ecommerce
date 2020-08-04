using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Core.Entities;
using ECommerce.Data;
namespace ECommerce.Services
{
    public class CategoryServices
    {
        public ECommerceDbContext dc = new ECommerceDbContext();
        public List<Category> AllCategory()
        {
            return dc.Categories.ToList();
        }
    }
}
