using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;

namespace _01_ExecutingCommands
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteNonQuery();
            ExecuteReader();
            ExecuteScalar();
            ExecuteXmlReader();
        }

        static void ExecuteNonQuery()
        {
            string select = "UPDATE Customers " +
                            "SET ContactName = 'Bill' " +
                            "WHERE ContactName = 'Bob'";
            SqlConnection conn = new SqlConnection(GetDatabaseConnection());
            conn.Open();
            SqlCommand cmd = new SqlCommand(select, conn);
            int rowsReturned = cmd.ExecuteNonQuery();
            Console.WriteLine("{0} rows returned.", rowsReturned);
            conn.Close();
        }

        static void ExecuteReader()
        {
            string select = "SELECT ContactName,CompanyName FROM Customers";
            SqlConnection conn = new SqlConnection(GetDatabaseConnection());
            conn.Open();
            SqlCommand cmd = new SqlCommand(select, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("Contact: {0,-20} Company: {1}",
                                   reader[0], reader[1]);
            }
        }

        static void ExecuteScalar()
        {
            string select = "SELECT COUNT(*) FROM Customers";
            SqlConnection conn = new SqlConnection(GetDatabaseConnection());
            conn.Open();
            SqlCommand cmd = new SqlCommand(select, conn);
            object o = cmd.ExecuteScalar();
            Console.WriteLine(o);
        }

        static void ExecuteXmlReader()
        {
            string select = "SELECT ContactName,CompanyName " +
                            "FROM Customers FOR XML AUTO";
            //SqlConnection conn = new SqlConnection(GetDatabaseConnection());
            DbConnection conn1 = GetDatabaseConnection("Northwind");
            SqlConnection conn = conn1 as SqlConnection;
            conn.Open();
            SqlCommand cmd = new SqlCommand(select, conn);
            XmlReader xr = cmd.ExecuteXmlReader();
            xr.Read();
            string data;
            do
            {
                data = xr.ReadOuterXml();
                if (!string.IsNullOrEmpty(data))
                    Console.WriteLine(data);
            } while (!string.IsNullOrEmpty(data));
            conn.Close();

        }

        static string GetDatabaseConnection()
        {
            return "server=DARK\\SQLEXPRESS;" +
                "integrated security=SSPI;" +
                "database=Northwind";
        }
        static DbConnection GetDatabaseConnection(string name)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            DbProviderFactory factory = DbProviderFactories.GetFactory(settings.ProviderName);
            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = settings.ConnectionString;
            return conn;
        }

    }
}
