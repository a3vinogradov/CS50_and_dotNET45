using System;

namespace PaymentsDemo
{
  class Program
  {
    static void Main()
    {
      using (var data = new PaymentsEntities())
      {
        foreach (var p in data.Payments)
        {
          Console.WriteLine("{0}, {1} - {2:C}", p.GetType().Name, p.Name, p.Amount);
        }
      }

      using (var data = new PaymentsEntities())
      {
        var q = data.Payments.OfType<CreditcardPayment>();
        Console.WriteLine(q.ToTraceString());
        Console.WriteLine();
        foreach (var p in data.Payments.OfType<CreditcardPayment>())
        {
          Console.WriteLine("{0} {1} {2}", p.Name, p.Amount, p.CreditCard);
        }
      }

    }
  }
}
