using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.WebUI.Domain
{
    public class ProductParameters
    {
        public int? ProductOption { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
    }
}