using myLive.Data;
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

        public InstrutoresModel Alterar(InstrutoresModel Instrutor)
        {
            InstrutoresModel instrutorDB = BuscarPorID(Instrutor.ID);

            if (instrutorDB.ID == 0) throw new Exception("Houve um problema na edição do instrutor. tente novamente!");

            instrutorDB.Nome = Instrutor.Nome;
            instrutorDB.Email = Instrutor.Email;
            instrutorDB.DataNascimento = Instrutor.DataNascimento;
            instrutorDB.EnderecoInstagram = Instrutor.EnderecoInstagram;

            _bancoContext.Instrutores.Update(instrutorDB);
            _bancoContext.SaveChanges();

            return instrutorDB;
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
