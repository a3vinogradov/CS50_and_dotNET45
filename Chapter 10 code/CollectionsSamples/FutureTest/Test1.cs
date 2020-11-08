using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace FutureTest
{
    public class Tests
    {
        private List<Racer> _racers;

        [SetUp]
        public void Setup()
        {
            var graham = new Racer(7, "Graham", "Hill", "UK", 14);
            var emerson = new Racer(13, "Emerson", "Fittipaldi", "Brazil", 14);
            var mario = new Racer(16, "Mario", "Andretti", "USA", 12);
            _racers = new List<Racer>(20) { graham, emerson, mario };
            _racers.Add(new Racer(24, "Michael", "Schumacher", "Germany", 91));
            _racers.Add(new Racer(27, "Mika", "Hakkinen", "Finland", 20));
            _racers.Add(new Racer(12, "Jochen", "Rindt", "Austria", 6));
            _racers.Add(new Racer(22, "Ayrton", "Senna", "Brazil", 41));

        }

        #region List Remove methods
        [Test]
        public void TestList_RemoveAt()
        {
            Assert.AreEqual(7, _racers.Count);
            _racers.RemoveAt(0);
            Assert.AreEqual(6, _racers.Count);
        }
        [Test]
        public void TestList_Remove()
        {
            var theFirst = _racers[0];
            Assert.AreEqual(7, _racers.Count);
            _racers.Remove(theFirst);
            Assert.AreEqual(6, _racers.Count);
        }
        [Test]
        public void TestList_RemoveRange()
        {
            Assert.AreEqual(7, _racers.Count);
            _racers.RemoveRange(1, 2);
            Assert.AreEqual(5, _racers.Count);
        }
        [Test]
        public void TestList_Clear()
        {
            Assert.AreEqual(7, _racers.Count);
            _racers.Clear();
            Assert.AreEqual(0, _racers.Count);
        }
        #endregion

        #region List Find methods
        [Test]
        public void TestList_IndexOf()
        {
            var racer = _racers[4];
            Assert.AreEqual(4, _racers.IndexOf(racer));
        }
        [Test]
        public void TestList_FindIndex_with_Predicate()
        {
            var CountryFinder = new FindCountry("Finland");
            int ind = _racers.FindIndex(CountryFinder.FindCountryPredicate);
            //int ind = _racers.FindIndex(new FindCountry("Finland").FindCountryPredicate);
            Assert.AreEqual(4, ind);
        }
        [Test]
        public void TestList_FindIndex_with_Lambda()
        {
            int ind = _racers.FindIndex(r => r.Country == "Finland");
            Assert.AreEqual(4, ind);
        }
        [Test]
        public void TestList_FindAll()
        {
            var res = _racers.FindAll(r => r.Country == "Brazil");
            //int ind = _racers.FindIndex(r => r.Country == "Brazil");
            Assert.AreEqual(2, res.Count);
            Assert.AreEqual(13, res[0].Id);
            Assert.AreEqual(22, res[1].Id);

            res = _racers.FindAll(r => r.Wins > 20);
            Assert.AreEqual(2, res.Count);
            Assert.AreEqual(24, res[0].Id);
            Assert.AreEqual(22, res[1].Id);
        }
        #endregion 

        #region List Sort method
        [Test]
        public void TestList_Sort()
        {
            Assert.AreEqual(7, _racers[0].Id);
            _racers.Sort();
            Assert.AreEqual(16, _racers[0].Id);
        }
        [Test]
        public void TestList_Sort_Comparer()
        {
            Assert.AreEqual(7, _racers[0].Id);
            _racers.Sort(new RacerComparer(CompareType.Country));
            Assert.AreEqual(12, _racers[0].Id);
        }
        [Test]
        public void TestList_Sort_Comparison()
        {
            Assert.AreEqual(7, _racers[0].Id);
            _racers.Sort((r1,r2)=>r2.Wins.CompareTo(r1.Wins));
            Assert.AreEqual(24, _racers[0].Id);
        }
        #endregion 
        [Test]
        public void TestList_ConvertAll()
        {
            List<Person> persons = _racers.ConvertAll(r => new Person(r.FirstName + " " + r.LastName));
            Assert.AreEqual("Graham Hill", persons[0].fullName);
        }
    }
}