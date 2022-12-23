using FluentValidation;
using myLive.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myLive.Models;

namespace myLive.Validator
{
    public class InscritosValidator: AbstractValidator<InscritosModel>
    {
        readonly BancoContext _bancoContext;
        private int _idInscrito;

        public InscritosValidator(BancoContext context)
        {
            _bancoContext = context;
            RuleFor(inscrito => inscrito.ID).NotNull().Must(SetIDInscrito);
            RuleFor(inscrito => inscrito.Nome).NotNull().Length(8, 255).WithMessage("Nome deve conter entre 8 a 255 caracteres.");
            RuleFor(inscrito => inscrito.DataNascimento).Must(BeOver18).WithMessage("Inscrito deve conter no minimo 18 anos.");
            RuleFor(inscrito => inscrito.Email).NotNull().Length(8, 255).Must(NotDuplicadEmail).WithMessage("E-mail deve conter entre 8 e 255 caracteres ou já esta em uso.");
        }

        protected bool BeOver18(DateTime data)
        {
            return (DateTime.Now.Date.Year - data.Date.Year) >= 18;
        }

        protected bool NotDuplicadEmail(string email)
        {
            return _bancoContext.Inscritos.Where(x => x.Email == email && x.ID != _idInscrito && x.Excluido == null).Count() == 0;
        }

        protected bool SetIDInscrito(int id)
        {
            _idInscrito = id;
            return true;
        }
    }
}
