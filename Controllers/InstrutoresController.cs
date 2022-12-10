using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myLive.Models;
using myLive.Repositories;

namespace myLive.Controllers
{
    public class InstrutoresController : Controller
    {
        private readonly iInstrutoresRepositorie _instrutorRepositorio;
        public InstrutoresController(iInstrutoresRepositorie instrutorRepositorio)
        {
            _instrutorRepositorio = instrutorRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(InstrutoresModel Instrutor)
        {
            if (ModelState.IsValid)
            {
                _instrutorRepositorio.Adicionar(Instrutor);
                return RedirectToAction("Index");
            }
            return View(Instrutor);
        }
    }
}
