using Microsoft.AspNetCore.Mvc;
using myLive.Data;
using myLive.Models;
using myLive.Repositories.Inscritos;
using myLive.Validator;
using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;
using System.Threading.Tasks;

namespace myLive.Controllers
{
    public class InscritosController : Controller
    {
        private readonly iInscritosRepositorie _inscritosRepositorie;
        private readonly BancoContext _db;
        public InscritosController(iInscritosRepositorie inscritosRepositorie, BancoContext db)
        {
            _inscritosRepositorie = inscritosRepositorie;
            _db = db;
        }
        public IActionResult Index()
        {
            List<InscritosModel> Inscritos = _inscritosRepositorie.BuscarTodos();
            return View(Inscritos);
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(InscritosModel inscrito)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    InscritosValidator validator = new InscritosValidator(_db);
                    ValidationResult results = validator.Validate(inscrito);

                    if (results.IsValid)
                    {
                        _inscritosRepositorie.Adicionar(inscrito);
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

                return View(inscrito);
            }
            catch(Exception error)
            {
                TempData["MensagemErro"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Editar(int ID)
        {
            InscritosModel inscrito = _inscritosRepositorie.BuscarPorID(ID);
            return View(inscrito);
        }

        [HttpPost]
        public IActionResult Editar(InscritosModel inscrito)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    InscritosValidator validador = new InscritosValidator(_db);
                    ValidationResult results = validador.Validate(inscrito);

                    if (results.IsValid)
                    {
                        _inscritosRepositorie.Alterar(inscrito);
                        TempData["MensagemSucesso"] = "Inscrito alterado com sucesso!";
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
                return View("Editar", inscrito);

            }
            catch(Exception error)
            {
                TempData["MensagemErro"] = "Ops!, não foi possível alterar o inscrito, tente novamente!" + " Error: " + error.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult ConfirmacaoExclusao(int ID)
        {
            InscritosModel inscrito = _inscritosRepositorie.BuscarPorID(ID);
            return View(inscrito);
        }

        public IActionResult Excluir(int ID)
        {
            try
            {
                bool inscritoExcluido = _inscritosRepositorie.Excluir(ID);

                if (inscritoExcluido)
                {
                    TempData["MensagemSucesso"] = "Inscrito excluido com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Ocorreu um erro na exclusão do inscrito, tente novamente!";
                }

                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                TempData["MensagemErro"] = "Ocorreu um erro na exclusão do inscrito, tente novamente! error: " + error.Message;
                return RedirectToAction("Index");
            }
        }

    }
}
