using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotnetAPI.Models;
using DotnetAPI.Services;

namespace DotnetMovieAPI.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieService _movSvc;

        public MovieController(MovieService movieService)
        {
            _movSvc = movieService;
        }

        public ActionResult<IList<Movie>> Index() => View(_movSvc.Read());

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<Movie> Create(Movie movie){
            movie.Created = movie.LastUpdated = DateTime.Now;
            if(ModelState.IsValid){
                _movSvc.Create(movie);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult<Movie> Edit(string id) =>
            View(_movSvc.Find(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movie)
        {
            movie.LastUpdated = DateTime.Now;
            movie.Created = movie.Created.ToLocalTime();
            if(ModelState.IsValid){
                _movSvc.Update(movie);
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            _movSvc.Delete(id);
            return RedirectToAction("Index");
        }
    }
}