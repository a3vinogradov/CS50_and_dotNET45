using System.Collections.Generic;

namespace POCODemo
{
  public class Book
  {
    public Book()
    {
      this.Authors = new HashSet<Author>();
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Publisher { get; set; }
    public string Isbn { get; set; }

    public virtual ICollection<Author> Authors { get; set; }
  }
}
