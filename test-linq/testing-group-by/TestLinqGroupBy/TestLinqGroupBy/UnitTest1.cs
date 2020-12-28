using AutoFixture.Xunit2;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Xunit;

namespace TestLinqGroupBy
{
    public class UnitTest1
    {

        public class ClassWithListAttached
        {
            public int key { get; set; }
            public IEnumerable<int> ListAssociated { get; set; }
        }


        [Fact]
        public void Test1()
        {
            // Arrange
            var obj1 = new ClassWithListAttached
            {
                key = 3,
                ListAssociated = new List<int> { 1, 2, 3, 4 }
            };

            var obj2 = new ClassWithListAttached
            {
                key = 2,
                ListAssociated = new List<int> { 2, 3, 4 }
            };

            var obj3 = new ClassWithListAttached
            {
                key = 3,
                ListAssociated = new List<int> { 1, 3 }
            };

            var objList = new List<ClassWithListAttached> { obj1, obj2, obj3 };
            // Assert

            // Here we have the next dictionary:
            //  item[1] => ob1 ,ob3
            //  item[2] => ob2.ListAssociated 
            var key1VsListAssociated = objList.GroupBy(x => x.key);


            // Here we have the next dictionary:
            //  item[1] => ob1 ,ob3
            //  item[2] => ob2.ListAssociated 
            key1VsListAssociated.Count().Should().Be(2);
            key1VsListAssociated.ToList()[0].Should().Contain(obj1);
            key1VsListAssociated.ToList()[0].Should().Contain(obj3);
            key1VsListAssociated.ToList()[1].Should().Contain(obj2);
        }


        [Fact]
        public void Test2([Frozen]IClassThatdoSomething classThatdoSomething)
        {
            // Arrange
            var numbersList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var pageSize = 3;
            var expectedPages = 4;
            // Act
            var resultPage = numbersList.Count / pageSize + numbersList.Count % pageSize;
            // Assert
            resultPage.Should().Be(expectedPages);

        }


    }
}
