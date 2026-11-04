using System.Collections.Generic;
using System.Threading.Tasks;
using PetLog.Dominio.Entidades;

namespace PetLog.Aplicacao
{
    public class ClienteRequisicao
    {
        public string Nome {get; set;} = string.Empty;
        public string SobreNome {get; set;} = string.Empty;
        public long Telefone {get; set;}
    }
}