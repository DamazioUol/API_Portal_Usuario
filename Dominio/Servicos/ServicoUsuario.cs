using Dominio.Interfaces;
using Dominio.Paginacao;
using Entidades.Entidades;
using System.Linq.Expressions;

namespace Dominio.Servicos
{
    public class ServicoUsuario : IServicoUsuario
    {
        private readonly IUsuario _IUsuario;

        public ServicoUsuario(IUsuario IUsuario)
        {
            _IUsuario = IUsuario;
        }

        public async  Task Adicionar(Usuario objeto)
        {
            if (objeto.ValidarUsuario())
            {
                objeto.Criado = DateTime.UtcNow;
                objeto.Modificado = DateTime.UtcNow;

                await _IUsuario.Adicionar(objeto);
            }
        }

        public async Task Alterar(Usuario objeto)
        {
            if (objeto.ValidarUsuario())
            {
                objeto.Modificado = DateTime.UtcNow;

                await _IUsuario.Alterar(objeto);
            }
        }

        public async Task<Usuario> BuscarPorId(int id)
        {
           return await _IUsuario.BuscarPorId(id);
        }

        public async  Task Excluir(int id)
        {
            await _IUsuario.Excluir(id);
        }

        public async Task<Paginacao<Usuario>> Paginar(int pagina, int paginaQuantidade, Expression<Func<Usuario, bool>> expression = null)
        {
            return await _IUsuario.Paginar(pagina, paginaQuantidade, expression);
        }
    }
}
