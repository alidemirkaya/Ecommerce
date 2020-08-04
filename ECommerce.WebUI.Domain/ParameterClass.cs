using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.WebUI.Domain
{
    public class ParameterClass
    {
            public string ProductName { get; set; }
            public string CustomerName { get; set; }
            public string Mail { get; set; }
            public DateTime? startDate { get; set; }
            public DateTime? finishDate { get; set; }
            public float? StartPrice { get; set; }
            public float? FinishPrice { get; set; }
            public bool? inactiveCustomer { get; set; }
            public bool? activeCustomer { get; set; }
            public int[] categories { get; set; }
            public int? SelectCustomer { get; set; }
    }
}