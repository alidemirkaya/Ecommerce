using ECommerce.Core.Entities;
using ECommerce.Services;
using ECommerce.Web.Models;
using ECommerce.WebUI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ECommerce.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderReports _orderReports;
        private readonly CategoryServices _categoryServices;
        public OrderController()
        {
            _orderReports = new OrderReports();
            _categoryServices = new CategoryServices();
        }
        [Authorize]
        // GET: Order
        public ActionResult Index(OrderIndexVM orderIndexVM)
        {
            var userName = HttpContext.User.Identity.Name;
            //var user = GetUserByUserName();
            List<ECommerce.WebUI.Domain.OrderViewModel> orderViewModels = new List<ECommerce.WebUI.Domain.OrderViewModel>();
            orderIndexVM.orderViewModels = orderViewModels;
            orderIndexVM.categories = _categoryServices.AllCategory();
            return View(orderIndexVM);
        }
        [HttpPost]
        public ActionResult Index(ECommerce.WebUI.Domain.ParameterClass parameterClass)
        {
            OrderIndexVM orderIndexVM = new OrderIndexVM();
            orderIndexVM.orderViewModels = _orderReports.orderViewModels(parameterClass);
            orderIndexVM.categories = _categoryServices.AllCategory();
            return View(orderIndexVM);
        }
    }
}