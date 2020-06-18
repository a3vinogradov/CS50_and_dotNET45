using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Linq;
using System.Text;

namespace QueryDemo
{
  class Program
  {
    static void Main(string[] args)
    {
      // EntitySqlDemo();
      // EntitySqlDemo2();
      // EntitySqlWithParameters();
      // ObjectQuery();
      // ObjectQueryFiltering();
      // LinqToEntities();
      LinqToEntities2();
      Console.ReadLine();
    }

    private static void LinqToEntities2()
    {
      using (var data = new Formula1Entities())
      {
        var query = from r in data.Racers
                    from rr in r.RaceResults
                    where rr.Position <= 3 && rr.Position >= 1 &&
                        r.Nationality == "Switzerland"
                    group r by r.Id into g
                    let podium = g.Count()
                    orderby podium descending
                    select new
                    {
                      Racer = g.FirstOrDefault(),
                      Podiums = podium
                    };
        foreach (var r in query)
        {
          Console.WriteLine("{0} {1} {2}", r.Racer.FirstName, r.Racer.LastName,
                            r.Podiums);
        }
      }

    }

    private static void LinqToEntities()
    {
      using (var data = new Formula1Entities())
      {
        var racers = from r in data.Racers
                     where r.Wins > 40
                     orderby r.Wins descending
                     select r;
        foreach (Racer r in racers)
        {
          Console.WriteLine("{0} {1}", r.FirstName, r.LastName);
        }
      }

    }

    private static void ObjectQueryFiltering()
    {
      using (var data = new Formula1Entities())
      {
        string country = "USA";
        ObjectQuery<Racer> racers = data.Racers.Where("it.Nationality = @Country",
            new ObjectParameter("Country", country))
            .OrderBy("it.Wins DESC, it.Starts DESC")
            .Top("3");
        foreach (var racer in racers)
        {
          Console.WriteLine("{0} {1}, wins: {2}, starts: {3}",
              racer.FirstName, racer.LastName, racer.Wins, racer.Starts);
        }
      }

    }

    private static void ObjectQuery()
    {
      using (Formula1Entities data = new Formula1Entities())
      {
        //ObjectSet<Racer> racers = data.Racers;
        //Console.WriteLine(racers.CommandText);
        //Console.WriteLine(racers.ToTraceString());

        string country = "Brazil";
        ObjectQuery<Racer> racers = data.Racers.Where("it.Nationality = @Country",
          new ObjectParameter("Country", country));

        Console.WriteLine(racers.CommandText);
        Console.WriteLine(racers.ToTraceString());
      }

    }

    private static async void EntitySqlWithParameters()
    {
      string connectionString = ConfigurationManager.ConnectionStrings["Formula1Entities"].ConnectionString;
      var connection = new EntityConnection(connectionString);
      await connection.OpenAsync();
      EntityCommand command = connection.CreateCommand();
      command.CommandText = "SELECT VALUE it FROM [Formula1Entities].[Racers] AS it " +
        "WHERE it.Nationality = @Country";
      command.Parameters.AddWithValue("Country", "Austria");

      DbDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection);
      while (await reader.ReadAsync())
      {
        Console.WriteLine("{0} {1}", reader["FirstName"], reader["LastName"]);
      }
      reader.Close();
    }

    private static async void EntitySqlDemo()
    {
      string connectionString = ConfigurationManager.ConnectionStrings["Formula1Entities"].ConnectionString;
      var connection = new EntityConnection(connectionString);
      await connection.OpenAsync();
      EntityCommand command = connection.CreateCommand();
      command.CommandText = "[Formula1Entities].[Racers]";
      DbDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection);
      while (await reader.ReadAsync())
      {
        Console.WriteLine("{0} {1}", reader["FirstName"], reader["LastName"]);
      }
      reader.Close();
    }

    private static async void EntitySqlDemo2()
    {
      string connectionString = ConfigurationManager.ConnectionStrings["Formula1Entities"].ConnectionString;
      var connection = new EntityConnection(connectionString);
      await connection.OpenAsync();
      EntityCommand command = connection.CreateCommand();
      command.CommandText = "SELECT Racers.FirstName, Racers.LastName FROM Formula1Entities.Racers";
      DbDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection);
      while (await reader.ReadAsync())
      {
        Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
      }
      reader.Close();
    }
  }
}
