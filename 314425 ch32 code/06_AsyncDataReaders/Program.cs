﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_AsyncDataReaders
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running tasks...");

            MethodTimer.TimeMethod(() =>
            {
                var t1 = GetEmployeeCount();
                var t2 = GetOrderCount();
                Console.WriteLine("Executing queries...");
                Task.WaitAll(t1, t2);
                Console.WriteLine("Number of employes: {0}, Number of orders: {1}", t1.Result, t2.Result);
            }, 1, "Getting data took {1}ms");
        }

        public async static Task GetEmployeesAndOrders()
        {
            int employees = await GetEmployeeCount();
            int orders = await GetOrderCount();

            Console.WriteLine("Number of employes: {0}, Number of orders: {1}", employees, orders);
        }

        public async static Task<int> GetEmployeeCount()
        {
            using (SqlConnection conn = new SqlConnection(GetDatabaseConnection()))
            {
                SqlCommand cmd = new SqlCommand("WAITFOR DELAY '0:0:02';select count(*) from employees", conn);
                conn.Open();

                return await cmd.ExecuteScalarAsync().ContinueWith(t => Convert.ToInt32(t.Result));
            }
        }

        public async static Task<int> GetOrderCount()
        {
            using (SqlConnection conn = new SqlConnection(GetDatabaseConnection()))
            {
                SqlCommand cmd = new SqlCommand("WAITFOR DELAY '0:0:02';select count(*) from orders", conn);
                conn.Open();

                return await cmd.ExecuteScalarAsync().ContinueWith(t => Convert.ToInt32(t.Result));
            }
        }

        private static string GetDatabaseConnection()
        {
            return "server=DARK\\SQLEXPRESS;" +
                    "integrated security=SSPI;" +
                    "database=Northwind";
            //return "server=(local);" +
            //    "integrated security=SSPI;" +
            //    "database=Northwind";

            
        }

    }

    public class MethodTimer
    {
        public static void TimeMethod(Action method, int iterations, string message)
        {
            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < iterations; i++)
                method();

            sw.Stop();

            Console.WriteLine(message, iterations, sw.ElapsedMilliseconds);
        }
    }

}
