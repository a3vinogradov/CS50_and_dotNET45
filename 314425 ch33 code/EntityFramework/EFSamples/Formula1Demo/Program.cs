using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Objects;
using System.Linq;

namespace Formula1Demo
{
    class Program
    {
        static void Main()
        {
             //EagerLoadingDemo();
            // TrackingDemo();
            // ChangeInformation();
            DetachDemo();
        }

        private static void DetachDemo()
        {
            using (var data = new Formula1Entities())
            {
                data.ObjectStateManager.ObjectStateManagerChanged +=
                    ObjectStateManager_ObjectStateManagerChanged;
                ObjectQuery<Racer> racers = data.Racers.Where("it.Lastname='Alonso'");
                Racer fernando = racers.First();
                EntityKey key = fernando.EntityKey;
                data.Racers.Detach(fernando);
                // Racer is now detached and can be changed independent of the 
                // object context
                fernando.Starts++;
                Racer originalObject = data.GetObjectByKey(key) as Racer;
                data.Racers.ApplyCurrentValues(fernando);
            }

        }

        private static void ObjectStateManager_ObjectStateManagerChanged(object sender, CollectionChangeEventArgs e)
        {
            Console.WriteLine("Object State change — action: {0}", e.Action);
            Racer r = e.Element as Racer;
            if (r != null)
                Console.WriteLine("Racer {0}", r.LastName);
        }

        private static void TrackingDemo()
        {
            using (var data = new Formula1Entities())
            {
                data.ObjectStateManager.ObjectStateManagerChanged += ObjectStateManager_ObjectStateManagerChanged;


                Racer niki1 = (from r in data.Racers
                               where r.Nationality == "Austria" && r.LastName == "Lauda"
                               select r).First();

                Racer niki2 = (from r in data.Racers
                               where r.Nationality == "Austria"
                               orderby r.Wins descending
                               select r).First();

                if (Object.ReferenceEquals(niki1, niki2))
                {
                    Console.WriteLine("the same object");
                }
            }
        }

        private static void ChangeInformation()
        {
            using (var data = new Formula1Entities())
            {
                var jean = new Racer
                {
                    FirstName = "Jean-Eric",
                    LastName = "Vergne",
                    Nationality = "France",
                    Starts = 0
                };
                data.Racers.AddObject(jean);
                Racer fernando = data.Racers.Where("it.Lastname='Alonso'").First();
                fernando.Starts++;
                DisplayState(EntityState.Added.ToString(),
                    data.ObjectStateManager.GetObjectStateEntries(EntityState.Added));
                DisplayState(EntityState.Modified.ToString(),
                    data.ObjectStateManager.GetObjectStateEntries(EntityState.Modified));
                ObjectStateEntry stateOfFernando =
                    data.ObjectStateManager.GetObjectStateEntry(fernando.EntityKey);
                Console.WriteLine("state of Fernando: {0}",
                              stateOfFernando.State.ToString());
                foreach (string modifiedProp in stateOfFernando.GetModifiedProperties())
                {
                    Console.WriteLine("modified: {0}", modifiedProp);
                    Console.WriteLine("original: {0}",
                                      stateOfFernando.OriginalValues[modifiedProp]);
                    Console.WriteLine("current: {0}",
                                      stateOfFernando.CurrentValues[modifiedProp]);
                }
            }
        }



        static void DisplayState(string state, IEnumerable<ObjectStateEntry> entries)
        {
            foreach (var entry in entries)
            {
                var r = entry.Entity as Racer;
                if (r != null)
                {
                    Console.WriteLine("{0}: {1}", state, r.LastName);
                }
            }
        }


        private static void EagerLoadingDemo()
        {
            using (var data = new Formula1Entities())
            {
                foreach (var racer in data.Racers.Include("RaceResults.Race.Circuit"))
                {
                    Console.WriteLine("{0} {1}", racer.FirstName, racer.LastName);
                    foreach (var raceResult in racer.RaceResults)
                    {
                        Console.WriteLine("\t{0} {1:d} {2}", raceResult.Race.Circuit.Name, raceResult.Race.Date, raceResult.Position);
                    }
                }
            }
        }
    }
}
