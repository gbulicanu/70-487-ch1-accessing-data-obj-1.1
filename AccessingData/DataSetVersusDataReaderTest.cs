using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AccessingData
{
    [TestFixture]
    public class DataSetVersusDataReaderTest
    {
        [TestCase(2)]
        [TestCase(3)]
        public void GetCustomersWithDataAdapter(int customerId)
        {
            // Arrange
            var customerData = new DataSet("CustomerData");
            var customerTable = new DataTable("Customer");
            customerData.Tables.Add(customerTable);

            var sqlBuilder = new StringBuilder()
                .Append("SELECT FirstName, LastName, CustomerId, AccountId")
                .Append(" FROM [dbo].[Customer] WHERE CustomerId = @CustomerId");

            // Act
            // Assumes an app.config has connection string added to <connectionString />
            // section named "TestDB"
            using (var sqlConnection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString))
            {
                using (var sqlCommand = new SqlCommand(
                    sqlBuilder.ToString(),
                    sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        try
                        {
                            sqlDataAdapter.Fill(customerData, "Customer");
                        }
                        finally
                        {
                            // This should be already closed even if we encounter an e
                            if (sqlConnection.State != ConnectionState.Closed)
                            {
                                sqlConnection.Close();
                            }
                        }
                    }
                }
            }

            // Assert
            Assert.That(
                customerTable.Rows.Count,
                Is.EqualTo(1),
                "We expected exactly 1 record to be returned.");
            Assert.That(
                customerTable.Rows[0].ItemArray[customerTable.Columns["customerId"].Ordinal],
                Is.EqualTo(customerId),
                "The record returned has and ID different than expected.");
        }

        [TestCase(2)]
        [TestCase(3)]
        public void GetCustomersWithDateReader(int customerId)
        {
            // Arrange
            var sqlBuilder = new StringBuilder()
                .Append("SELECT FirstName, LastName, CustomerId, AccountId")
                .Append(" FROM [dbo].[Customer] WHERE CustomerId = @CustomerId");
            var customers = new List<Tuple<string, string, int, int>>();

            // Act
            // Assumes an app.config has connection string added to <connectionString />
            // section named "TestDB"
            using (var sqlConnection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString))
            {
                using (var sqlCommand = new SqlCommand(
                    sqlBuilder.ToString(),
                    sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    sqlConnection.Open();
                    using (var sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        try
                        {
                            if (sqlDataReader.HasRows)
                            {
                                var firstNameOrdinal = sqlDataReader.GetOrdinal("FirstName");
                                var lastNameOrdinal = sqlDataReader.GetOrdinal("LastName");
                                var customerIdOrdinal = sqlDataReader.GetOrdinal("CustomerId");
                                var accountIdOrdinal = sqlDataReader.GetOrdinal("AccountId");

                                while (sqlDataReader.Read())
                                {
                                    var customer = new Tuple<string, string, int, int>(
                                        sqlDataReader.GetString(firstNameOrdinal),
                                        sqlDataReader.GetString(lastNameOrdinal),
                                        sqlDataReader.GetInt32(customerIdOrdinal),
                                        sqlDataReader.GetInt32(accountIdOrdinal));
                                    customers.Add(customer);
                                }
                            }
                        }
                        finally
                        {
                            // This should be already closed even if we encounter an e
                            if (sqlConnection.State != ConnectionState.Closed)
                            {
                                sqlConnection.Close();
                            }
                        }
                    }
                }
            }

            // Assert
            Assert.That(
                customers.Count,
                Is.EqualTo(1),
                "We expected exactly 1 record to be returned.");
            Assert.That(
                customers[0].Item3,
                Is.EqualTo(customerId),
                "The record returned has and ID different than expected.");

        }
    }
}
