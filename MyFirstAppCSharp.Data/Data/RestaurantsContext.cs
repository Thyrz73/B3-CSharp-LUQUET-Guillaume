using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MyFirstAppCSharp.Data.Model;


namespace MyFirstAppCSharp.Data.Data
{
    public class RestaurantsContext : DbContext
    {
        public RestaurantsContext(DbContextOptions<RestaurantsContext> options)
           : base(options)
        {
        }

        public RestaurantsContext()
        {
        }


        // Méthode connexion a la base de donnée 
        public DbSet<Restaurant> Restaurant { get; set; }
        private const string ConnexionString = @"server=GUIGUI;database=dbRestaurant;trusted_connection=true;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                ConnexionString
               );
        }

      

    }
}
