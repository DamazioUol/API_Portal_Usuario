using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Paginacao;
using Entidades.Entidades;
using System.Linq.Expressions;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoUsuario : IAplicacaoUsuario
    {
        IUsuario _IUsuario;
        IServicoUsuario _IServicoUsuario;

        public AplicacaoUsuario(IUsuario IUsuario, IServicoUsuario IServicoUsuario)
        {
            _IUsuario = IUsuario;
            _IServicoUsuario = IServicoUsuario;
        }

        public async Task Adicionar(Usuario objeto)
        {
            await _IServicoUsuario.Adicionar(objeto);
        }

        public async Task Alterar(Usuario objeto)
        {
            await _IServicoUsuario.Alterar(objeto);
        }

        public async Task<Usuario> BuscarPorId(int id)
        {
            return await _IUsuario.BuscarPorId(id);
        }

        public async Task Excluir(int id)
        {
            await _IUsuario.Excluir(id);
        }

        public async Task<Paginacao<Usuario>> Paginar(int pagina, int paginaQuantidade, Expression<Func<Usuario, bool>> expression = null)
        {
            return await _IUsuario.Paginar(pagina, paginaQuantidade, expression);
        }
    }
}
