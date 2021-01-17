using System;
using System.Collections.Generic;
using System.Text;
using MyFirstAppCSharp;
using MyFirstAppCSharp.Data.Model;
using System.Linq;
using MyFirstAppCSharp.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace MyFirstAppCSharp.Service
{
    public class RestaurantService
    {
        private readonly RestaurantsContext _context;
        
        public RestaurantService(RestaurantsContext context)
        {
            _context = context;
        }

        public RestaurantService()
        {
        }

        public List<Restaurant> Get()
        {
                        
            using (var db = new RestaurantsContext())
            {
                var result = db.Restaurant.Include(x => x.address).ToList();
                
                return result;
            }
        }

        // Méthode pour ajouter un restaurant a la base de donnée
        public void AjouterRestau(Restaurant restaurant)
        {
                    
            
            using (var db = new RestaurantsContext())
            {
                db.Restaurant.Add(restaurant);
                db.SaveChanges();
            }
            
        }

        // Méthode pour editer un restaurant dans la base de donnée 
        public void EditRestau(Restaurant restaurant)
        {
                        
            using (var db = new RestaurantsContext())
            {
                db.Restaurant.Include(x => x.address);
                db.Restaurant.Update(restaurant);
                db.SaveChanges();
            }
            

        }

        // Méthode pour supprimer un restaurant de la base de donnée 
        public void SupprRestau(Restaurant restaurant)
        {
           
            using (var db = new RestaurantsContext())
            {
                db.Restaurant.Include(x => x.address);
                db.Restaurant.Remove(restaurant);
                db.SaveChanges();
            }
        }
    }
}
