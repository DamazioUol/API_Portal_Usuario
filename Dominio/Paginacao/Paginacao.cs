namespace Dominio.Paginacao
{
    public class Paginacao<T> where T : class
    {
        public List<T> Itens { get; set; }
        public int PaginaQuantidade { get; set; }
        public int Pagina { get; set; }
        public int Total { get; set; }
    }
}
