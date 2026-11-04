using System.Collections.Generic;
using System.Threading.Tasks;
using PetLog.Dominio.Entidades;

namespace PetLog.Aplicacao
{
    public class VendedorRequisicao 
    {
        public string Nome {get; set;} = string.Empty;
        public string Email {get; set;} = string.Empty;
        public string Senha {get; set;} = string.Empty;
        public bool Ativo;
    }
}