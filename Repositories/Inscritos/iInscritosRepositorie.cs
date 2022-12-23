using myLive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myLive.Repositories.Inscritos
{
    public interface iInscritosRepositorie
    {
        InscritosModel Adicionar(InscritosModel Instrutor);

        InscritosModel Alterar(InscritosModel Instrutor);

        bool Excluir(int ID);

        InscritosModel BuscarPorID(int ID);

        List<InscritosModel> BuscarTodos();
    }
}
