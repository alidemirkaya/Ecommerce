using ECommerce.Core.Entities;
using ECommerce.Data;
using ECommerce.WebUI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Razor.Parser;

namespace ECommerce.Services
{
    public class OrderReports
    {

        //public List<OrderViewModel> orderViewModels(string productName, int[] categories, string customerName, string mail, DateTime? startDate, DateTime? finishDate, float? startPrice, float? finishPrice, bool? activeCustomer, bool? inactiveCustomer)
        public List<OrderViewModel> orderViewModels(ECommerce.WebUI.Domain.ParameterClass parameterClass)
        {
            var dc = new ECommerceDbContext();
            var query = from products in dc.Products
                        join orderDetails in dc.OrderDetails on products.Id equals orderDetails.Product.Id
                        join order in dc.Orders on orderDetails.Order.Id equals order.Id
                        join customer in dc.Customers on order.CustomerId equals customer.Id
                        join productCategories in dc.ProductCategories on products.Id equals productCategories.ProductId
                        join categories in dc.Categories on productCategories.CategoryId equals categories.Id
                        select new 
                        {
                            products,orderDetails,order,customer,productCategories,categories
                        };
            var allCategory = dc.Categories;
            if (!string.IsNullOrEmpty(parameterClass.ProductName))
            {
                query = query.Where(a => a.products.ProductName == parameterClass.ProductName);
            }
            if (!string.IsNullOrEmpty(parameterClass.CustomerName))
            {
                query = query.Where(a => a.customer.FirstName == parameterClass.CustomerName);
            }
            if (!string.IsNullOrEmpty(parameterClass.Mail))
            {
                query = query.Where(a => a.customer.Mail == parameterClass.Mail);
            }
            if (parameterClass.startDate.HasValue && parameterClass.finishDate.HasValue)
            {
                query = query.Where(x => x.order.OrderDate >= parameterClass.startDate && x.order.OrderDate <= parameterClass.finishDate);
            }
            if (parameterClass.StartPrice.HasValue && parameterClass.FinishPrice.HasValue)
            {
                query = query.Where(x => x.order.OrderTotal >= parameterClass.StartPrice && x.order.OrderTotal <= parameterClass.FinishPrice);
            }
            if (parameterClass.SelectCustomer.HasValue)
            {
                if (parameterClass.SelectCustomer == 1)
                {
                    query = query.Where(x => x.customer.Status == true);
                }
                else if (parameterClass.SelectCustomer == 2)
                {
                    query = query.Where(x => x.customer.Status == false);
                }
            }
            if (parameterClass.categories != null)
            {
                List<int> categoryList = new List<int>();
                foreach (var item in parameterClass.categories.ToList())
                {
                    var query1 = dc.Categories.First(x => x.Id == item);
                    var query2 = dc.Categories.Where(x => x.CategoryName.StartsWith(query1.CategoryName)).Select(x => x.Id).ToList();
                    categoryList.AddRange(query2);
                }
                query = query.Where(x => categoryList.Contains(x.categories.Id));
            }
            var results =  query.Select(x => new OrderViewModel
            {
                ProductId = x.products.Id,
                OrderId = x.order.Id,
                Customer = x.customer.FirstName + " " +x.customer.LastName,
                Phone = x.customer.Phone,
                Status = x.customer.Status,
                OrderTotal = x.order.OrderTotal,
                PaymentStatus = x.order.PayStatu,
                ProductName = x.products.ProductName,
                Quantity = x.orderDetails.Quantity,
                ShipmentStatus = x.order.ShipmentStatu,
                UnitPrice = x.orderDetails.UnitPrice,
                DetailId=x.orderDetails.Id,
            }).Distinct().ToList();

            results.ForEach(r =>
            {
                var productCategoryIds = dc.ProductCategories.Where(pc => pc.ProductId == r.ProductId).Select(p => p.CategoryId).ToList();
                var tempCategories = dc.Categories.Where(c => productCategoryIds.Contains(c.Id)).Select(c => c.CategoryName).Distinct().ToList();
                r.CategoryName = string.Join(", ", tempCategories);
            });

            return results;
        }

    }
}
