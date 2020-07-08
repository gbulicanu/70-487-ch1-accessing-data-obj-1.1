using InteractivePreGeneratedViews;

using NUnit.Framework;

using System;
using System.Linq;

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
            InteractiveViews.SetViewCacheFactory(
                entities,
                new FileViewCacheFactory(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                    @"\70487AccessingData\EFCache\Cache.xml"));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            entities.Dispose();
        }

        [TestCase(2)]
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
