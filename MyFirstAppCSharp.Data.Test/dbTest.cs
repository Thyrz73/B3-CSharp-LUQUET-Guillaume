using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFirstAppCSharp.Data.Data;
using MyFirstAppCSharp.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MyFirstAppCSharp.Data.Test
{
    [TestClass]
    public class dbTest
    {
 




        private TestContext testContextInstance;

        // permet d'afficher dans les test unitaires les résultats et valeurs de certaines variables
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }



        
        [TestMethod]
        public void bddTest()
        {
            IQueryable<Restaurant> MesRestau;
            using (var bdd = new RestaurantsContext())
            {
                bdd.Database.EnsureCreated(); // Vérifie que la base de donnée existe

                // Ajout d'un restaurant pour le test
                var Restau = new Restaurant()
                {
                    Name = "GuiguiRestau",
                    address = new Address() { street = "rue Ampère", zipcode = "38000" },
                    Description = "Petit restau sympa",
                    EmailProprio = "guigui@gmail.com",
                    NumeroTel = "020313165131",
                    grades = new List<Grade> {
                        new Grade(){DateLastVisite=456, Commentaire="A", Note="10"},
                        new Grade(){DateLastVisite=789, Commentaire="A", Note="10"}
                    }
                };

                bdd.Restaurant.Add(Restau);
                bdd.SaveChanges();

                var result = bdd.Restaurant.Include(r => r.address).ToList();
                MesRestau = bdd.Restaurant.Where(r => r.ID > 0);
                                
                string desc = "";
                foreach (var nom in MesRestau)
                {
                    desc = nom.Description; //replace la variable desc par la description du restau
                }
                                
                Assert.IsTrue(desc.ToString() == "Petit restau sympa"); // Le dernier restau etant celui que l'on ajoute dans la base, la description de celui-ci doit etre "Petit restau sympa" 

            }

        }
        

        
    }
}
