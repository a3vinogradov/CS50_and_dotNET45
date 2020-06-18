using System;

namespace BooksDemo
{
  class Program
  {
    static void Main()
    {
      using (var data = new BooksEntities())
      {
        foreach (var book in data.Books)
        {
          Console.WriteLine("{0}, {1}", book.Title, book.Publisher);
        }
      }

    }
  }
}
