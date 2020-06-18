using System;


namespace POCODemo
{
  class Program
  {
    static void Main()
    {
      using (BooksEntities data = new BooksEntities())
      {
        data.Configuration.LazyLoadingEnabled = true;
        var books = data.Books; // .Include("Authors");
        foreach (var b in books)
        {
          Console.WriteLine("{0} {1}", b.Title, b.Publisher);
          foreach (var a in b.Authors)
          {
            Console.WriteLine("\t{0} {1}", a.FirstName, a.LastName);
          }
        }

        
      }
    }
  }
}
