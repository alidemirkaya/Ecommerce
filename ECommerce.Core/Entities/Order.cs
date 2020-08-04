using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class Order
    {
        private ICollection<OrderDetail> _orderDetails;
        public int Id { get; set; }
        public string PayStatu { get; set; }
        public string ShipmentStatu { get; set; }
        public System.DateTime OrderDate { get; set; }
        public float OrderTotal { get; set; }
        public decimal VatRate { get; set; }
        public float VatTotal { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails
        {
            get { return _orderDetails ?? (_orderDetails = new List<OrderDetail>()); }
            protected set { _orderDetails = value; }
        }
    }
}
