using System;
using Xunit;

namespace TestChapter8
{
    //delegate string ReturnString();
    delegate int GetInt();
    delegate long GetLong(int val);

    public class TestChapter8
    {
        [Fact(DisplayName ="Вызов делегата у int")]
        public void TestDelegate()
        {
            //ReturnString meth;
            Func<string> meth;
            int i = 8;
            meth = i.ToString;
            string was = meth();
            Assert.Equal("8", was);
        }

        [Fact(DisplayName = "Вызов делегата у пользовательского класса ")]
        public void TestDelegateFromUserClass()
        {
            GetInt meth;
            IntGenerator obj = new IntGenerator();
            meth = obj.GetPi;
            int was = meth();
            Assert.Equal(3, was);
            meth = obj.GetE;
            was = meth();
            Assert.Equal(2, was);
        }
        [Fact(DisplayName = "Вызов делегата c параметром ")]
        public void TestDelegateWithParameter()
        {
            GetLong meth;
            IntGenerator obj = new IntGenerator();
            meth = obj.GetInteger;
            long was = meth(2);
            Assert.Equal(2, was);
            was = meth(5);
            Assert.Equal(5, was);
        }
    }
}
