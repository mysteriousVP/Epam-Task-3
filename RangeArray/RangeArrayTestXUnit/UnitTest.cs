using System;
using Xunit;
using RangeArray;

namespace RangeArrayTestXUnit
{
    public class UnitTest
    {
        [Theory]
        [InlineData(-2, 5)]
        [InlineData(-1, 100)]
        [InlineData(-0, -15)]
        [InlineData(2, 8)]
        [InlineData(5, 68)]
        public void SetterShouldSetValueInTheArrayByIndex(int index, int value)
        {
            //arrange
            ReIndexedArray<int> arr = new ReIndexedArray<int>(-2, 5);
            int expected = value;

            //act
            arr[index] = value;

            //assert
            Assert.Equal(expected, arr[index]);
        }

        [Theory]
        [InlineData(-3, 5)]
        [InlineData(6, 5)]
        [InlineData(int.MinValue, 5)]
        [InlineData(int.MaxValue, 5)]
        public void SetterShouldThrowIndexOutOfRangeExceptionDueToIncorrectIndex(int index, int value)
        {
            //arrange 
            ReIndexedArray<int> arr = new ReIndexedArray<int>(-2, 5);

            //act
            Action action = () => arr[index] = value;

            //assert
            Assert.Throws<IndexOutOfRangeException>(action);
        }

        [Theory]
        [InlineData(-2, 5)]
        [InlineData(-1, 100)]
        [InlineData(-0, -15)]
        [InlineData(2, 8)]
        [InlineData(5, 68)]
        public void GetterShouldReturnValueFromTheArrayByIndex(int index, int value)
        {
            //arrange
            ReIndexedArray<int> arr = new ReIndexedArray<int>(-2, 5);
            int expected = 0;

            //act
            int a = arr[index];

            //assert
            Assert.Equal(expected, a);
        }

        [Theory]
        [InlineData(-3, 5)]
        [InlineData(6, 5)]
        [InlineData(int.MinValue, 5)]
        [InlineData(int.MaxValue, 5)]
        public void GetterShouldThrowIndexOutOfRangeExceptionDueToIncorrectIndex(int index, int value)
        {
            //arrange 
            ReIndexedArray<int> arr = new ReIndexedArray<int>(-2, 5);

            //act
            int number;
            Action action = () => number = arr[index];

            //assert
            Assert.Throws<IndexOutOfRangeException>(action);
        }

        [Fact]
        public void EqualsShouldCompareTwoDifferentInstances()
        {
            //arrange 
            ReIndexedArray<string> stringArr = new ReIndexedArray<string>(-1, 1);
            ReIndexedArray<int> intArr = new ReIndexedArray<int>(-1, 1);
            bool expected = false;

            //act
            bool actual = stringArr.Equals(intArr);

            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EqualsShouldCompareTwoSameInstances()
        {
            //arrange 
            ReIndexedArray<string> firstStringArr = new ReIndexedArray<string>(-1, 1);

            ReIndexedArray<string> secondStringArr = firstStringArr;
            bool expected = true;

            //act
            bool actual = firstStringArr.Equals(secondStringArr);

            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToArrayShouldReturnValuesOfTheArrayInArrayForm()
        {
            //arrange
            int[] expected = new int[] { 1, 2, 6, 3, 5 };
            ReIndexedArray<int> arr = new ReIndexedArray<int>(expected, 1, 5);

            //act
            int[] actual = arr.ToArray();

            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OverridedToStringShouldReturnValuesOfTheArrayInStringForm()
        {
            //arrange
            int[] data = new int[] { 1, 2, 6, 3, 5 };
            ReIndexedArray<int> arr = new ReIndexedArray<int>(data, 1, 5);

            string expected = string.Empty;
            for (int i = 0; i < data.Length - 1; i++)
            {
                expected += string.Format("[" + data[i] + "], ");
            }
            expected += string.Format("[" + data[data.Length - 1] + "];");

            //act
            string actual = arr.ToString();

            //assert
            Assert.Equal(expected, actual);
        }
    }
}
