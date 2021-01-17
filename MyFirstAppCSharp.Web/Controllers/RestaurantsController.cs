using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFirstAppCSharp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyFirstAppCSharp.Service;
using MyFirstAppCSharp.Data.Data;
using MyFirstAppCSharp.Data.Model;
using Newtonsoft.Json;

namespace MyFirstAppCSharp.Web.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly RestaurantsContext _context;

        public RestaurantsController(RestaurantsContext context)
        {
            _context = context;
        }
        

        // Connecion a la base de donnée + affichage des restaurants
        public async Task<IActionResult> Index()
        {
            _context.Database.EnsureCreated();  // Créer la base de donnée avec les tables et nom qui sont dans Restaurant.cs Adress.cs et Grade.cs dans le dossier Model 
            _context.Restaurant.Include(x => x.address);
            RestaurantService restaurantService = new RestaurantService(_context);

            var result = restaurantService.Get();
            return View(result);
        }
        
        // Récupère le détail des restaurants 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _context.Restaurant.Include(x => x.address);
            var restaurant = await _context.Restaurant.Include(x => x.address)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // Affiche la page de création 
        public IActionResult Create()
        {
            return View();
        }
        

        public void JsonToSQL(string JSONresult)
        {

        }


        // Créer un nouveau restraurant 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,NumeroTel, Description, EmailProprio,address,  address.street")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Restaurant.Include(x => x.address);
                RestaurantService restaurantService = new RestaurantService(_context);
                restaurantService.AjouterRestau(restaurant);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        
        // Affiche la page d'édition
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View();
        }
        
        // Edition d'un restaurant
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID, Name,NumeroTel, Description, EmailProprio, address, address.street")] Restaurant restaurant)
        {
            if (id != restaurant.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    RestaurantService restaurantService = new RestaurantService(_context);
                    restaurantService.EditRestau(restaurant);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }
        
        // Supprime un restaurant
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant
                .FirstOrDefaultAsync(m => m.ID == id);
            RestaurantService restaurantService = new RestaurantService(_context);
            restaurantService.SupprRestau(restaurant);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }
        
        // Redirection de l'utilisateur après suppresion d'un restaurant
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _context.Restaurant.FindAsync(id);
            
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurant.Any(e => e.ID == id);
        }
    }
}
