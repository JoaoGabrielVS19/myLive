using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using myLive.Data;
using myLive.Models;

namespace myLive.Validator
{
    public class InstrutoresValidator : AbstractValidator<InstrutoresModel>
    {
        readonly BancoContext _bancoContext;
        private int _idInscrito;

        public InstrutoresValidator(BancoContext context)
        {
            _bancoContext = context;
            RuleFor(inscrito => inscrito.ID).NotNull().Must(SetIDInscrito);
            RuleFor(instrutor => instrutor.Nome).NotNull().Length(8, 255).WithMessage("Nome deve conter entre 8 a 255 caracteres.");
            RuleFor(instrutor => instrutor.DataNascimento).Must(BeOver18).WithMessage("Instrutor deve conter no minimo 18 anos.");
            RuleFor(instrutor => instrutor.Email).NotNull().Length(8,255).Must(NotDuplicadEmail).WithMessage("E-mail deve conter entre 8 e 255 caracteres ou já esta em uso.");
        }

        protected bool BeOver18(DateTime data)
        {
            return  (DateTime.Now.Date.Year - data.Date.Year) >= 18 ;
        }

        protected bool NotDuplicadEmail(string email)
        {
            return _bancoContext.Instrutores.Where(x => x.Email == email && x.Excluido == null).Count() == 0;
        }

        protected bool SetIDInscrito(int id)
        {
            _idInscrito = id;
            return true;
        }

    }
}
