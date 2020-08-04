using ECommerce.WebUI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.WebUI.Domain
{
    public class ProductViewModel
    {
        public int ProductId { get; set; } 
        public string ProductName { get; set; }
        public float UnitPrice { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

    }
}
