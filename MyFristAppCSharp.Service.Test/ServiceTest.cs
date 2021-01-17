using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFirstAppCSharp.Service;
using System.Collections.Generic;
using System.Linq;

namespace MyFristAppCSharp.Service.Test
{
    [TestClass]
    public class ServiceTest
    {
        
        [TestMethod]
        public void TestCreation()
        {
            RestaurantService restSrv = new RestaurantService();

            var nbRestauAvant = restSrv.Get().Count();
            
                        
            var restauAAjouter = new MyFirstAppCSharp.Data.Model.Restaurant()
            {
                    Name = "GuiguiRestau",
                    address = new MyFirstAppCSharp.Data.Model.Address() { street = "rue Ampère", zipcode = "38000" },
                    Description = "Petit restau sympa",
                    EmailProprio = "guigui@gmail.com",
                    NumeroTel = "020313165131",
                    grades = new List<MyFirstAppCSharp.Data.Model.Grade> {
                        new MyFirstAppCSharp.Data.Model.Grade(){DateLastVisite=456, Commentaire="A", Note="10"},
                        new MyFirstAppCSharp.Data.Model.Grade(){DateLastVisite=789, Commentaire="A", Note="10"}
                    }
                };

            restSrv.AjouterRestau(restauAAjouter);

            var nbRestauApres = restSrv.Get().Count();
            
            
            Assert.IsTrue(nbRestauAvant == nbRestauApres + 1);
        }
        


    }
}
