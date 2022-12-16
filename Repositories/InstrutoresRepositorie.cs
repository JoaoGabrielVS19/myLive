﻿using myLive.Data;
using myLive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myLive.Repositories
{
    public class InstrutoresRepositorie : iInstrutoresRepositorie
    {
        private readonly BancoContext _bancoContext;

        public InstrutoresRepositorie(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public InstrutoresModel Adicionar(InstrutoresModel Instrutor)
        {
            _bancoContext.Add(Instrutor);
            _bancoContext.SaveChanges();

            return Instrutor;
        }

        public InstrutoresModel BuscarPorID(int ID)
        {
            return _bancoContext.Instrutores.FirstOrDefault(x => x.ID == ID);
        }

        public List<InstrutoresModel> BuscarTodos()
        {
            return _bancoContext.Instrutores.Where(x => x.Excluido == null).OrderBy(x => x.ID).ToList();
        }
    }
}
