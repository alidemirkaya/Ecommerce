using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int CategoryDisplayOrder{ get; set; }
        public int ParentId { get; set; }
        public virtual ICollection<ProductCategories> ProductCategories
        {
            get; set;
        }
    }
}
