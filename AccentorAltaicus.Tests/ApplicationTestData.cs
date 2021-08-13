using System;
using System.Collections.Generic;
using Entities.Enums;
using Entities.Models;

namespace AccentorAltaicus.Tests
{
    public static class ApplicationTestData
    {

        public static IEnumerable<Order> Orders => new List<Order>() {
            new Order {Id = 1, Status = OrderStatus.Created, CreateDate = new DateTime(2020, 11, 01)}
        };
        public static IEnumerable<Product> Products => new List<Product>() {
            new Product { Id = 1, Name = "Product 1", Price = 1, Weight = 1 },
            new Product { Id = 2, Name = "Product 2", Price = 10, Weight = 10 },
            new Product { Id = 3, Name = "Product 3", Price = 100, Weight = 100 },
        };
        public static IEnumerable<OrderItem> OrderItems => new List<OrderItem>() {
            new OrderItem {OrderId = 1, ProductId = 1, Quantity = 1},
            new OrderItem {OrderId = 1, ProductId = 2, Quantity = 2},
        };
    }
}
