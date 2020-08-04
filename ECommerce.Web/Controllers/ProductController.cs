using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Services;
using ECommerce.Web.Models;
using ECommerce.WebUI.Domain;

namespace ECommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductReports _productReports;

        public ProductController()
        {
            _productReports = new ProductReports();
        }
        [Authorize]
        // GET: Product
        public ActionResult Index(ECommerce.WebUI.Domain.ProductViewModel productViewModel)
        {
            List<ECommerce.WebUI.Domain.ProductViewModel> productViewModels = new List<ProductViewModel>();
            return View(productViewModels);
        }
        [HttpPost]
        public ActionResult Index(ECommerce.WebUI.Domain.ProductParameters productParameters )
        {            
            return View(_productReports.ProductViewModels(productParameters.ProductOption, productParameters.StartDate, productParameters.FinishDate));
        }
    }
}