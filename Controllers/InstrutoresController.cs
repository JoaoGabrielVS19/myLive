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
            List<InstrutoresModel> instrutores = _instrutorRepositorio.BuscarTodos();
            return View(instrutores);
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(InstrutoresModel Instrutor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool EmailDuplicado = _instrutorRepositorio.EmailDuplicado(Instrutor.Email);

                    if (EmailDuplicado == false)
                    {
                        bool InstagramDuplicado = _instrutorRepositorio.InstagramDuplicado(Instrutor.EnderecoInstagram);

                        if(InstagramDuplicado == false)
                        {
                            int Idade = DateTime.Now.Year - Instrutor.DataNascimento.Year;

                            if (Idade >= 18)
                            {
                                _instrutorRepositorio.Adicionar(Instrutor);
                                TempData["MensagemSucesso"] = "Instrutor cadastrado com sucesso!";
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                TempData["MensagemErro"] = "Para realizar o cadastro deve-se ser maior de 18 anos.";
                            }

                        }
                        else
                        {
                            TempData["MensagemErro"] = "Já existe instrutor cadastro com este Instagram!";
                        }   
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Já existe instrutor cadastro com este E-mail!";
                    }
                }
                return View(Instrutor);
            }catch(Exception error)
            {
                TempData["MensagemErro"] = "Ops!, não foi possível cadastrar seu instrutor, tente novamente!" + " Error: " + error.Message;
                return RedirectToAction("Index");
            }
            
        }
    }
}
