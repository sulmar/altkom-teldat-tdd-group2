using System.Collections.Generic;

namespace TestApp.Mocking
{
    public interface ISalesReportBuilder
    {
        void AddOrders(IEnumerable<Order> orders);
        SalesReport Build();
    }
}