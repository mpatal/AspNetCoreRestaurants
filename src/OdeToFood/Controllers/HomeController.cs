using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Entities;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IRestaurantData _restaurantData;
        private readonly IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = new HomePageViewModel
            {
                Restaurants = _restaurantData.GetAll(),
                CurrentMessage = _greeter.GetGreeting()
            };
            return View(model);
        }

        
        public IActionResult Details(int id)
        {
            var model = _restaurantData.Get(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditViewModel restaurantViewModel)
        {
            if (ModelState.IsValid)
            {
                var newRestaurant = Mapper.Map<RestaurantEditViewModel, Restaurant>(restaurantViewModel);
                newRestaurant = _restaurantData.Add(newRestaurant);
                _restaurantData.Commit();
                return RedirectToAction("Details", new { id = newRestaurant.Id });
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            var restaurant = _restaurantData.Get(id);
            if (restaurant == null)
            {
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, RestaurantEditViewModel model)
        {
            var restaurant = _restaurantData.Get(id);
            //TODO::handle not found error with page handle the case someone deleted 
            if (ModelState.IsValid)
            {
                restaurant = Mapper.Map(model, restaurant);
                _restaurantData.Commit();
                return RedirectToAction("Details", new { id = restaurant.Id });
            }
            
            return View(restaurant);
        }
    }
}
