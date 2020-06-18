using System;

namespace MyDemoBase
{
    class Program
    {
        static void Main()
        {
            using (var data = new a3vtestEntities1())
            {
                Blogs Blog1 = new Blogs()
                {
                    BlogId = 1,
                    Name = "Name 1"
                };
                Blogs Blog2 = new Blogs()
                {
                    BlogId = 2,
                    Name = "Name 2"
                };
                data.Blogs.Add(Blog1);
                data.Blogs.Add(Blog2);
                data.SaveChanges();
            }
            using (var data = new a3vtestEntities1())
            {
                foreach (var blog in data.Blogs)
                {
                    Console.WriteLine("{0}", blog.Name);
                }
            }
        }
    }
}
