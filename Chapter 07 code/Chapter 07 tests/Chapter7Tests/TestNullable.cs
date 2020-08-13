using System;
using Xunit;

namespace Chapter7Tests
{
    public class TestNullable
    {
        [Fact(DisplayName = "Бинарные операции с Nullable")]
        public void TestNullOperation()
        {
            int? a = null;
            int? b = a + 4;
            Assert.Null(b);
            b = a * 2;
            Assert.Null(b);
        }
        
        [Fact(DisplayName = "Сравнение с Nullable")]
        public void TestNullCompare()
        {
            int? a = null;
            int? b = null;
            int? c = 5;
            Assert.False(a<c);
            Assert.False(a > c);
            Assert.False(a == c);
            Assert.False(a < b);
            Assert.False(a > b);
            Assert.False(a != b);
            Assert.True(a == b);
            Assert.Equal(5, c);
        }

        [Fact(DisplayName = "Операция поглощения Nullable")]
        public void TestNullAbsorbtion()
        {
            int? a = null;
            int b;
            
            b = a ?? 10;
            Assert.Equal(10, b);
            a = 3;
            b = a ?? 10;
            Assert.Equal(3, b);
        }
    }
}
