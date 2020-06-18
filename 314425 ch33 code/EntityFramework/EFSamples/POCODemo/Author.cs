using System.Collections.Generic;

namespace POCODemo
{
  public class Author
  {
    public Author()
    {
      this.Books = new HashSet<Book>();
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public virtual ICollection<Book> Books { get; set; }
  }
}
