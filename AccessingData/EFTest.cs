using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessingData
{
    [TestFixture]
    public class EFTest
    {
        TestEntities entities;

        [OneTimeSetUp]
        public void Setup()
        {
            entities = new TestEntities();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            entities.Dispose();
        }

        [TestCase(3)]
        public void GetCustomerById(int customerId) 
        {
            // Arrnge
            //var database = new TestEntities();

            // Act
            Customer result = entities.Customers.Single(c => c.CustomerId == customerId);

            // Assert
            Assert.That(
                result, Is.Not.Null,
                "Expected a value. Null here indicates no record with this ID.");
            Assert.That(result.CustomerId, Is.EqualTo(customerId), "Uh oh!");
        }
    }
}
