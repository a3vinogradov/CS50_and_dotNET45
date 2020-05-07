using System.Collections.Generic;
using System;

namespace Wrox.ProCSharp.Generics
{
  class Program
  {
    static void Main()
    {
      var accounts = new List<Account>()
      {
        new Account("Christian", 1500),
        new Account("Stephanie", 2200),
        new Account("Angela", 1800),
        new Account("Matthias", 2400)
      };

      decimal amount = Algorithm.AccumulateSimple(accounts);
            Console.WriteLine(amount);

      amount = Algorithm.Accumulate(accounts);
            Console.WriteLine(amount);

            amount = Algorithm.Accumulate<Account, decimal>(accounts, (item, sum) => sum += item.Balance);
            Console.WriteLine(amount);
            Console.ReadLine();
    }
  }
}
