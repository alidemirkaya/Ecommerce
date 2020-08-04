using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.WebUI.Domain
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Customer { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
        public string ShipmentStatus { get; set; }
        public string PaymentStatus { get; set; }
        public Single OrderTotal { get; set; }
        public string ProductName { get; set; }
        public Single UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }
        public int DetailId { get; set; }
    }
}
