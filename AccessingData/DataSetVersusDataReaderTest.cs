using NUnit.Framework;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AccessingData
{
    [TestFixture]
    public class DataSetVersusDataReaderTest
    {
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
    }
}
