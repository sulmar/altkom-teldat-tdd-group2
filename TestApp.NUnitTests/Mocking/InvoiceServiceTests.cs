using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Mocking;

namespace TestApp.NUnitTests.Mocking
{
    public class InvoiceServiceTests
    {
        private Mock<IMessageSender> messageSender;
        private IInvoiceService invoiceService;
        private Invoice invoice;


        [SetUp]
        public void Setup()
        {
            messageSender = new Mock<IMessageSender>();
            invoiceService = new InvoiceService(messageSender.Object);
            invoice = new Invoice { Number = "a" };
        }

        [Test]
        public void Add_WhenIsArgeement_SendInvoice()
        {
            // Arrange
            invoice.Agreement = true;

            // Act
            invoiceService.Add(invoice);

            // Assets
            messageSender.Verify(ms => ms.Send(It.IsAny<string>()), Times.Exactly(3));
        }


        [Test]
        public void Add_WhenDisagreement_NotSendInvoice()
        {
            // Arrange
            invoice.Agreement = false;

            // Act
            invoiceService.Add(invoice);

            // Assets
            messageSender.Verify(ms => ms.Send(It.IsAny<string>()), Times.Never);
        }
    }
}
