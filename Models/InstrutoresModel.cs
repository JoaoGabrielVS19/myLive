using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myLive.Models
{
    public class InstrutoresModel
    {
        public int ID { get; set; }


        [Required(ErrorMessage ="Informe um nome para o instrutor")]
        public string Nome { get; set; }


        [Required(ErrorMessage ="Informe a data de nascimento do instrutor")]
        public DateTime DataNascimento { get; set; }


        [Required(ErrorMessage ="Informe um email para o instrutor")]
        [EmailAddress(ErrorMessage ="Informe um email válido")]
        public string Email { get; set; }

        public string EnderecoInstagram { get; set; }

        public bool? Excluido { get; set; }
    }
}
