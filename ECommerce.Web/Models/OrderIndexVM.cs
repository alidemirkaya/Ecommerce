using ECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Web.Models
{
    public class OrderIndexVM
    {
        public List<ECommerce.WebUI.Domain.OrderViewModel> orderViewModels { get; set; }
        public List<Category> categories { get; set; }
    }
}