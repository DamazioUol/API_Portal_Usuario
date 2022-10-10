using Entidades.Enums;
using Entidades.Notifica;

namespace Entidades.Entidades
{
    public class Usuario : Notificacao
    {
        public int Id { get; set; }
        public DateTime Criado { get; set; }
        public DateTime Modificado { get; set; }


        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public EnumTipoEscolaridade Escolaridade { get; set; }


        public bool ValidarUsuario()
        {
            ValidarPropriedadeString(Nome, "Nome");
            ValidarPropriedadeString(Sobrenome, "Sobrenome");
            ValidarPropriedadeEmail(Email, "Email");
            ValidarPropriedadeDataNascimento(DataNascimento, "DataNascimento");

            if (Notificacoes.Count > 0) {
                return false;
            }

            return true;
        }

       
    }
}
