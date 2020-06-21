using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    /// <summary>
    /// Summary description for UnitTests
    /// </summary>
    [TestClass]
    public class UnitTests
    {

        private TestContext testContextInstance;
        private SqlConnection connection;
        
        public UnitTests()
        {
            string sqlconnection = "DATA SOURCE=MSSQLServer26;" + "INITIAL CATALOG=Project; INTEGRATED SECURITY=SSPI;";
            connection = new SqlConnection(sqlconnection);
            connection.Open();

        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        [TestMethod]
        public void GeoMeanTest()
        {
            double res = 0;
            try
            {
                SqlCommand command = new SqlCommand("SELECT dbo.GeoMean(Value) FROM [dbo].[TestTable]", connection);
                SqlDataReader datareader = command.ExecuteReader();
                datareader.Read();
                res = (double)datareader[0];
            }
            catch (Exception e)
            {
            }

            Assert.AreEqual(2.3643, res, 0.001);
        }

        [TestMethod]
        public void MedianTest()
        {
            double res = 0;
            try
            {
                SqlCommand command = new SqlCommand("SELECT dbo.Median(Value) FROM [dbo].[TestTable]", connection);
                SqlDataReader datareader = command.ExecuteReader();
                datareader.Read();
                res = (double)datareader[0];
            }
            catch (Exception e)
            {
            }

            Assert.AreEqual(2.5, res, 0.001);
        }

        [TestMethod]
        public void ModeTest()
        {
            double res = 0;
            try
            {
                SqlCommand command = new SqlCommand("SELECT dbo.Mode(Value) FROM [dbo].[TestTable]", connection);
                SqlDataReader datareader = command.ExecuteReader();
                datareader.Read();
                res = (double)datareader[0];
            }
            catch (Exception e) {}

            Assert.AreEqual(2.5, res, 0.001);
        }

        [TestMethod]
        public void RMSTest()
        {
            double res = 0;
            try
            {
                SqlCommand command = new SqlCommand("SELECT dbo.RMS(Value) FROM [dbo].[TestTable]", connection);
                SqlDataReader datareader = command.ExecuteReader();
                datareader.Read();
                res = (double)datareader[0];
            }
            catch (Exception e){}

            Assert.AreEqual(3.1024, res, 0.001);
        }

        [TestMethod]
        public void TruncatedMeanTest()
        {
            double res = 0;
            try
            {
                SqlCommand command = new SqlCommand("SELECT dbo.TruncatedMean(Value) FROM [dbo].[TestTable]", connection);
                SqlDataReader datareader = command.ExecuteReader();
                datareader.Read();
                res = (double)datareader[0];
            }
            catch (Exception e) { }

            Assert.AreEqual(2.75, res, 0.001);
        }
    }
}
