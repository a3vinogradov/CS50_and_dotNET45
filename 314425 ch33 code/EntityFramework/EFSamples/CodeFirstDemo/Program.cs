using System;


namespace CodeFirstDemo
{
    class Program
    {
        static void Main()
        {
            //CreateObjects();
            QueryData();
        }

        private static void QueryData()
        {
            using (var data = new MenuContext())
            {
                data.Configuration.LazyLoadingEnabled = false;
                foreach (var card in data.MenuCards.Include("Menus"))
                {
                    Console.WriteLine("{0}", card.Text);
                    foreach (var menu in card.Menus)
                    {
                        Console.WriteLine("\t{0} {1:d}", menu.Text, menu.Day);
                    }
                }
            }
        }

        private static void CreateObjects()
        {
            using (var data = new MenuContext())
            {
                MenuCard card = data.MenuCards.Create();
                card.Text = "Soups";
                data.MenuCards.Add(card);

                Menu m = data.Menus.Create();
                m.Text = "Baked Potato Soup";
                m.Price = 4.80M;
                m.Day = new DateTime(2012, 9, 20);
                m.MenuCard = card;

                data.Menus.Add(m);

                Menu m2 = data.Menus.Create();
                m2.Text = "Cheddar Broccoli Soup";
                m2.Price = 4.50M;
                m2.Day = new DateTime(2012, 9, 21);
                m2.MenuCard = card;


                data.Menus.Add(m2);

                try
                {
                    data.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
