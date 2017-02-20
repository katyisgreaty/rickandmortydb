using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RickAndMortyDataBase
{
    public class RickAndMortyDataBaseTest : IDisposable
    {
        public RickAndMortyDataBaseTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=RickandMorty_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_DatabaseEmptyAtFirst()
        {
             //Arrange, Act
             int result = Parasite.GetAll().Count;

             //Assert
             Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueIfNamesAreTheSame()
        {
            //Arrange, Act
            Parasite firstParasite = new Parasite("Duck with Muscles");
            Parasite secondParasite = new Parasite("Duck with Muscles");
            //Assert
            Assert.Equal(firstParasite, secondParasite);
        }
        [Fact]
        public void Test_Save_SavesToDatabase()
        {
            // Arrange
            Parasite testParasite = new Parasite("Sleepy Gary");

            // Act
            testParasite.Save();
            List<Parasite> result = Parasite.GetAll();
            List<Parasite> testList = new List<Parasite>{testParasite};

            // Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Save_AssignsIdToObject()
        {
            //Arrange
            Parasite testParasite = new Parasite("Hamurai");
            //Act
            testParasite.Save();
            Parasite savedParasite = Parasite.GetAll()[0];

            int result = savedParasite.GetId();
            int testId = testParasite.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        public void Dispose()
          {
            Parasite.DeleteAll();
          }
    }
}
