using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movies.Web.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      var movies = 
        new[] {
          new Movie("The Matrix", new DateTime(1999, 3, 31), 
            new[] {
              new Cast("Keanu Reeves", "Neo"),
              new Cast("Laurence Fishburne", "Morpheus"),
              new Cast("Carrie-Anne Moss", "Trinity") }),

          new Movie("Star Wars: Episode IV", new DateTime(1977, 5, 25), 
            new[] {
              new Cast("Mark Hamill", "Luke Skywalker"),
              new Cast("Harrison Ford", "Han Solo"),
              new Cast("Carrie Fisher", "Princess Leia Organa") })
        };
      return View(movies);
    }
  }
}