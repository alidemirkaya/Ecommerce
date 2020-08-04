using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class Product
    {
        private ICollection<OrderDetail> _orderDetails;


        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal VatRate { get; set; }
        public bool IsVate { get; set; }
        public float UnitPrice { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails
        {
            get { return _orderDetails ?? (_orderDetails = new List<OrderDetail>()); }
            protected set { _orderDetails = value; }
        }
        public ICollection<ProductCategories> ProductCategories { get; set; }
    }
}
