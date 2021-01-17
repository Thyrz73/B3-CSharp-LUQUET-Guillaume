using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFirstAppCSharp.Data.Data;
using System;
using System.Data;

namespace B3-CSharp-LUQUET-Guillaume.Data.Test
{
    [TestClass]
    public class dbTest
    {

        [TestMethod]
        //Test de la connexion à la base de données
        public void dbConnect()
        {

            var p = new RestaurantsContext();
            Assert.IsTrue(p.paire(2));
        }
    }
}
