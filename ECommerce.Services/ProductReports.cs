using ECommerce.Core.Entities;
using ECommerce.Data;
using ECommerce.WebUI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class ProductReports
    {
        public List<ProductViewModel> ProductViewModels(int? ProductOption ,DateTime? startDate,DateTime? finisDate)
        {
            var context = new ECommerceDbContext();
            var query = from products in context.Products
                        join orderDetails in context.OrderDetails on products.Id equals orderDetails.Product.Id
                        select new
                        {
                            products,
                            orderDetails
                        };
            if (ProductOption.HasValue)
            {
                if (ProductOption == 0)
                {
                    var queryProduct = from products in context.Products
                                       join orderDetail in context.OrderDetails on products.Id equals orderDetail.Product.Id
                                       select products;
                    var query2 = context.Products.Except<Product>(queryProduct);
                    return query2.ToList().Select(productList => new ProductViewModel
                    {
                        ProductId=productList.Id,
                        ProductName=productList.ProductName,
                        Description=productList.Description,
                        UnitPrice=productList.UnitPrice,
                        Quantity=0

                    }).ToList();
                }
                else
                {
                    if (startDate.HasValue && finisDate.HasValue)
                    {
                        var bestSellersBetweenTwoDate = (from orders in context.Orders
                                                         join orderDetail in context.OrderDetails on orders.Id equals orderDetail.Order.Id
                                                         join product in context.Products on orderDetail.Product.Id equals product.Id
                                                         where (orders.OrderDate >= startDate && finisDate >= orders.OrderDate)
                                                         select new ProductViewModel
                                                         {
                                                             ProductId = product.Id,
                                                             ProductName = product.ProductName,
                                                             Quantity = orderDetail.Quantity,
                                                             Description = product.Description,
                                                             UnitPrice = product.UnitPrice
                                                         }).ToList();
                        var querybestSellersBetweenTwoDate = bestSellersBetweenTwoDate.GroupBy(x => new
                        {
                            x.ProductId,
                            x.ProductName,
                            x.Description,
                            x.UnitPrice
                        })
                       .Select(x => new
                       {
                           ProductName = x.Key.ProductName,
                           ProductId = x.Key.ProductId,
                           Description=x.Key.Description,
                           UnitPrice=x.Key.UnitPrice,
                           Total = x.Sum(xp => xp.Quantity)
                       })
                       .OrderByDescending(x => x.Total)
                       .Take(5)
                       .ToList().Select(x => new ProductViewModel
                       {
                           ProductName = x.ProductName,
                           ProductId=x.ProductId,
                           UnitPrice=x.UnitPrice,
                           Description=x.Description,
                           Quantity = x.Total
                       }).ToList();
                        return querybestSellersBetweenTwoDate;
                    }
                    else
                    {
                        var query2 = query.GroupBy(x =>new
                                                    {
                                                        x.products.Id,
                                                        x.products.ProductName,
                                                        x.products.Description,
                                                        x.products.UnitPrice
                                                    })
                                                    .Select(x => new
                                                    {
                                                        ProductId = x.Key.Id,
                                                        ProductName=x.Key.ProductName,
                                                        ProductDescription=x.Key.Description,
                                                        UnitPrice=x.Key.UnitPrice,
                                                        Total = x.Sum(xp => xp.orderDetails.Quantity)
                                                    })
                                                    .OrderByDescending(x => x.Total)
                                                    .Take(5)
                                                    .ToList().Select(x => new ProductViewModel
                                                    {
                                                        ProductName = x.ProductName,
                                                        ProductId=x.ProductId,
                                                        Description=x.ProductDescription,
                                                        Quantity = x.Total,
                                                        UnitPrice=x.UnitPrice
                                                    }).ToList();
                        return query2;
                    }
                }
            }
            else
            {
                return query.Select(x => new ProductViewModel
                {
                     Description=x.products.Description,
                     ProductId=x.products.Id,
                     ProductName=x.products.ProductName,
                     Quantity=x.orderDetails.Quantity,
                     UnitPrice=x.products.UnitPrice
                }).ToList();
            }
        }
    }
}
