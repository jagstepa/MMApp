﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Data;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;

namespace MMApp.Web.Controllers.Music
{
    public class SongController : Controller
    {
        private readonly IMusicRepository _dashboardSP = new MusicSPRepository();

        public ActionResult Index(string searchText)
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            List<Song> targetList = new List<Song>();
            if (!string.IsNullOrEmpty(searchText))
            {
                var list = _dashboardSP.GetAllForText<Song>(searchText);
                targetList = new List<Song>(list.Cast<Song>());
            }

            return View(targetList);
        }

        public ActionResult AddSong()
        {
            var model = new Song
            {
                Musicians = new List<Musician>(_dashboardSP.GetAll<Musician>().Cast<Musician>()),
                SelectedMusicians = new List<Musician>()
            };

            TempData["SelectedMusicians"] = model.SelectedMusicians;

            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddSong(Song song)
        {
            song.SelectedMusicians = (List<Musician>)TempData["SelectedMusicians"];

            if (_dashboardSP.CheckDuplicate<Song>(song.SongName, ""))
            {
                TempData["CustomError"] = "Song ( " + song.SongName + " ) already exists!";
                ModelState.AddModelError("CustomError", "Song ( " + song.SongName + " ) already exists!");
            }

            if (ModelState.IsValid)
            {
                _dashboardSP.Add(song);

                return RedirectToAction("Index");
            }

            return RedirectToAction("AddSong");
        }

        public ActionResult UpdateSong(int songId)
        {
            var model = (Song)_dashboardSP.Find<Song>(songId);

            TempData["SelectedMusicians"] = model.SelectedMusicians;

            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateSong(Song song)
        {
            song.SelectedMusicians = (List<Musician>)TempData["SelectedMusicians"];

            if (ModelState.IsValid)
            {
                _dashboardSP.Update(song);

                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateSong", "Song", new { songId = song.Id });
        }

        public ActionResult RemoveSong(int songId, string songName)
        {
            if (_dashboardSP.CheckDelete<Song>(songId))
            {
                TempData["CustomError"] = "Can't Delete. There are musicians for Song ( " + songName + " )";
                ModelState.AddModelError("CustomError", "Can't Delete. There are musicians for Song ( " + songName + " )");
            }
            else
            {
                _dashboardSP.Remove<Song>(songId);
            }

            return RedirectToAction("Index");
        }

        public ActionResult SearchSongs(string searchText)
        {
            return RedirectToAction("Index", "Song", new {searchText });
        }

        public ActionResult GetMusician(int musicianId)
        {
            Musician musician = (Musician)_dashboardSP.Find<Musician>(musicianId);
            var list = (List<Musician>)TempData["SelectedMusicians"];
            var duplicateItem = list.SingleOrDefault(r => r.Id == musicianId);
            if (duplicateItem != null)
            {
                TempData["SelectedMusicians"] = list;
                return null;
            }
            list.Add(musician);
            TempData["SelectedMusicians"] = list;

            return Json(musician, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveMusician(int musicianId)
        {
            Musician musician = (Musician)_dashboardSP.Find<Musician>(musicianId);
            var list = (List<Musician>)TempData["SelectedMusicians"];
            var itemToRemove = list.SingleOrDefault(r => r.Id == musicianId);
            list.Remove(itemToRemove);
            TempData["SelectedMusicians"] = list;

            return Json(musician, JsonRequestBehavior.AllowGet);
        }
    }
}
