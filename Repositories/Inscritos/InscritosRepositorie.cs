using myLive.Data;
using myLive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myLive.Repositories.Inscritos
{
    public class InscritosRepositorie : iInscritosRepositorie
    {
        private readonly BancoContext _bancoContext;

        public InscritosRepositorie(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public InscritosModel Adicionar(InscritosModel inscrito)
        {
            _bancoContext.Add(inscrito);
            _bancoContext.SaveChanges();

            return inscrito;
        }

        public InscritosModel Alterar(InscritosModel inscrito)
        {
            InscritosModel inscritoDB = BuscarPorID(inscrito.ID);

            if (inscritoDB.ID == 0) throw new Exception("Ocorreu um erro na edição do inscrito, tente novamente!");

            inscritoDB.Nome = inscrito.Nome;
            inscritoDB.DataNascimento = inscrito.DataNascimento;
            inscritoDB.Email = inscrito.Email;
            inscritoDB.EnderecoInstagram = inscrito.EnderecoInstagram;

            _bancoContext.Inscritos.Update(inscritoDB);
            _bancoContext.SaveChanges();

            return inscritoDB;
        }

        public InscritosModel BuscarPorID(int ID)
        {
            return _bancoContext.Inscritos.FirstOrDefault(x => x.ID == ID);
        }

        public List<InscritosModel> BuscarTodos()
        {
            return _bancoContext.Inscritos.Where(x => x.Excluido == null).OrderBy(x => x.ID).ToList();
        }

        public bool Excluir(int ID)
        {
            InscritosModel inscritoDB = BuscarPorID(ID);

            if (inscritoDB.ID == 0) throw new Exception("Ocorreu um erro na exclusão do inscrito, tente novamente!");

            inscritoDB.Excluido = true;

            _bancoContext.Inscritos.Update(inscritoDB);
            _bancoContext.SaveChanges();

            return true;
        }
    }
}
