using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NLog;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Mocking
{
    public interface IOrderService
    {
        IEnumerable<Order> Get(DateTime from, DateTime to);
    }

    public interface IUserService
    {
        IEnumerable<Employee> GetBosses();
        Bot GetBot();
    }

    public class DbUserService : IUserService
    {
        private readonly SalesContext salesContext;

        public DbUserService(SalesContext context)
        {
            this.salesContext = context;
        }

        public IEnumerable<Employee> GetBosses()
        {
            var employees
                = salesContext.Users.OfType<Employee>().Where(e => e.IsBoss).ToList();

            return employees;
        }

        public Bot GetBot()
        {
            return salesContext.Users.OfType<Bot>().Single();
        }
    }

    public class ReportService
    {
        private const string apikey = "your_secret_key";

        public delegate void ReportSentHandler(object sender, ReportSentEventArgs e);
        public event ReportSentHandler ReportSent;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IOrderService orderService;
        private readonly ISalesReportBuilder salesReportBuilder;
        private readonly IUserService userService;

        public ReportService(IOrderService orderService,
            ISalesReportBuilder salesReportBuilder, 
            IUserService userService)
        {
            this.orderService = orderService;
            this.salesReportBuilder = salesReportBuilder;
            this.userService = userService;
        }

        public async Task SendSalesReportEmailAsync(DateTime date)
        {
            var orders = orderService.Get(date.AddDays(-7), date);

            if (!orders.Any())
            {
                return;
            }

            salesReportBuilder.AddOrders(orders);
            salesReportBuilder.AddOrders(orders);

            SalesReport report = salesReportBuilder.Build();

            // dotnet add package SendGrid
            SendGridClient client = new SendGridClient(apikey);


            var recipients = userService.GetBosses();

            recipients = recipients.Where(r => !string.IsNullOrEmpty(r.Email));

            var sender = userService.GetBot();

            foreach (var recipient in recipients)
            {
                var message = MailHelper.CreateSingleEmail(
                    new EmailAddress(sender.Email, $"{sender.FirstName} {sender.LastName}"),
                    new EmailAddress(recipient.Email, $"{recipient.FirstName} {recipient.LastName}"),
                    "Raport sprzedaży",
                    report.ToString(),
                    report.ToHtml());


                Logger.Info($"Wysyłanie raportu do {recipient.FirstName} {recipient.LastName} <{recipient.Email}>...");

                var response = await client.SendEmailAsync(message);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    ReportSent?.Invoke(this, new ReportSentEventArgs(DateTime.Now));

                    Logger.Info($"Raport został wysłany.");
                }
                else
                {
                    Logger.Error($"Błąd podczas wysyłania raportu.");

                    throw new ApplicationException("Błąd podczas wysyłania raportu.");
                }
            }
        }

        
    }



    public class SalesReportBuilder : ISalesReportBuilder
    {
         private IEnumerable<Order> orders;

        public void AddOrders(IEnumerable<Order> orders)
        {
            this.orders = orders;
        }

        public SalesReport Build()
        {
            return Create(orders);
        }

        private static SalesReport Create(IEnumerable<Order> orders)
        {
            SalesReport salesReport = new SalesReport();

            salesReport.TotalAmount = orders.Sum(o => o.Total);

            return salesReport;
        }
    }

    public class ReportSentEventArgs : EventArgs
    {
        public readonly DateTime SentDate;

        public ReportSentEventArgs(DateTime sentDate)
        {
            this.SentDate = sentDate;
        }
    }

    public class OrderService : IOrderService
    {
        private readonly SalesContext context;

        public OrderService()
        {
            context = new SalesContext();
        }

        public IEnumerable<Order> Get(DateTime from, DateTime to)
        {
            return context.Orders.Where(o => o.OrderedDate > from && o.OrderedDate < to).ToList();
        }
    }

    public class SalesContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
    }


    #region Models

    public abstract class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }


    public class Employee : User
    {
        public bool IsBoss { get; set; }
    }

    public class Bot : User
    {

    }

    public abstract class Report
    {
        public DateTime CreatedOn { get; set; }

        public string Name { get; }

        public Report()
        {
            CreatedOn = DateTime.Now;
        }
    }

    public class SalesReport : Report
    {
        public TimeSpan TotalTime { get; set; }

        public decimal TotalAmount { get; set; }


        public override string ToString()
        {
            return $"Report created on {CreatedOn} \r\n TotalAmount: {TotalAmount}";
        }

        public string ToHtml()
        {
            return $"<html>Report created on <b>{CreatedOn}</b> <p>TotalAmount: {TotalAmount}<p></html>";
        }
    }

    #endregion

}