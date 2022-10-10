using Entidades.Enums;

namespace WebAPI.Models
{
    public class FiltroUsuario
    {
        public int Pagina { get; set; }
        public int PaginaQuantidade { get; set; }

        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? Email { get; set; }
        public EnumTipoEscolaridade? Escolaridade { get; set; }
    }
}
