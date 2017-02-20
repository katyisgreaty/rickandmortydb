using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RickAndMortyDataBase
{
    public class RickAndMortyDataBaseTest
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
    }
}
