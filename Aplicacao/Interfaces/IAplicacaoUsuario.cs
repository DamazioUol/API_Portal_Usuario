﻿using Dominio.Paginacao;
using Entidades.Entidades;
using System.Linq.Expressions;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoUsuario
    {
        Task<Paginacao<Usuario>> Paginar(int pagina, int paginaQuantidade, Expression<Func<Usuario, bool>> expression = null);
        Task<Usuario> BuscarPorId(int id);
        Task Adicionar(Usuario objeto);
        Task Alterar(Usuario objeto);
        Task Excluir(int id);
    }
}
