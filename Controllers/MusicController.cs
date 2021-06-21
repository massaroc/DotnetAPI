using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DotnetAPI.Models;
using DotnetAPI.Services;

namespace DotnetAPI.MusicController
{
    public class MusicController : Controller
    {
        private readonly MusicService _musSvc;

        public MusicController(MusicService musicService)
        {
            _musSvc = musicService;
        }

        public ActionResult<IList<Music>> Index() => View(_musSvc.Read());

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Music music)
        {
            if(ModelState.IsValid){
                _musSvc.Create(music);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult<Music> Edit(string id) => View(_musSvc.Find(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Music music)
        {
            if(ModelState.IsValid){
                _musSvc.Update(music);
                return RedirectToAction("Index");
            }
            return View(music);
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            _musSvc.Delete(id);
            return RedirectToAction("Index");
        }
    }
    
}