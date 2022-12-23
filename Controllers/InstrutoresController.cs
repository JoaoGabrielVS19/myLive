using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myLive.Models;
using myLive.Repositories;
using FluentValidation.Results;
using myLive.Validator;
using myLive.Data;

namespace myLive.Controllers
{
    public class InstrutoresController : Controller
    {
        private readonly iInstrutoresRepositorie _instrutorRepositorio;
        private readonly BancoContext _db;
        public InstrutoresController(iInstrutoresRepositorie instrutorRepositorio, BancoContext db)
        {
            _instrutorRepositorio = instrutorRepositorio;
            _db = db;
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
                    InstrutoresValidator validator = new InstrutoresValidator(_db);
                    ValidationResult results = validator.Validate(Instrutor);

                    if (results.IsValid)
                    {
                        _instrutorRepositorio.Adicionar(Instrutor);
                        TempData["MensagemSucesso"] = "Instrutor cadastrado com sucesso!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var failure in results.Errors)
                        {
                            TempData["MensagemErro"] = failure.ErrorMessage;
                        }
                    }
                }
                return View(Instrutor);
            
            }catch(Exception error)
            {
                TempData["MensagemErro"] = "Ops!, não foi possível cadastrar seu instrutor, tente novamente!" + " Error: " + error.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Editar(int ID)
        {
            InstrutoresModel instrutor = _instrutorRepositorio.BuscarPorID(ID);
            return View(instrutor);
        }

        [HttpPost]
        public IActionResult Editar(InstrutoresModel Instrutor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    InstrutoresValidator validador = new InstrutoresValidator(_db);
                    ValidationResult resultado = validador.Validate(Instrutor);

                    if (resultado.IsValid)
                    {
                        _instrutorRepositorio.Alterar(Instrutor);
                        TempData["MensagemSucesso"] = "Instrutor alterado com sucesso!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var failure in resultado.Errors)
                        {
                            TempData["MensagemErro"] = failure.ErrorMessage;
                        }
                    }
                }

                return View("Editar", Instrutor);
            }
            catch(Exception error)
            {
                TempData["MensagemErro"] = "Ops!, não foi possível alterar o instrutor, tente novamente!" + " Error: " + error.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult ConfirmacaoExclusao(int ID)
        {
            InstrutoresModel contato = _instrutorRepositorio.BuscarPorID(ID);
            return View(contato);
        }

        public IActionResult Excluir(int ID)
        {
            try
            {
                bool instrutorExcluido = _instrutorRepositorio.Excluir(ID);

                if (instrutorExcluido)
                {
                    TempData["MensagemSucesso"] = "Instrutor excluido com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Ocorreu um erro na exclusão do instrutor, tente novamente!";
                }

                return RedirectToAction("Index");
            }catch(Exception error)
            {
                TempData["MensagemErro"] = "Ocorreu um erro na exclusão do instrutor, tente novamente! error: " + error.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
