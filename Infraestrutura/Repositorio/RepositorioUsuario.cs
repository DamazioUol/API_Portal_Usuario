using Dominio.Interfaces;
using Dominio.Paginacao;
using Entidades.Entidades;
using Infraestrutura.Configuracoes;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infraestrutura.Repositorio
{
    public class RepositorioUsuario : IServicoUsuario, IUsuario
    {
        private readonly DbContextOptions<SqlServerContext> _dbContextOptions;

        public RepositorioUsuario()
        {
            _dbContextOptions = new DbContextOptions<SqlServerContext>();
        }

        public async Task Adicionar(Usuario objeto)
        {
            using (var context = new SqlServerContext(_dbContextOptions))
            {
                await context.Set<Usuario>().AddAsync(objeto);
                await context.SaveChangesAsync();
            }
        }

        public async Task Alterar(Usuario objeto)
        {
            using (var context = new SqlServerContext(_dbContextOptions))
            {
                context.Set<Usuario>().Update(objeto);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Usuario> BuscarPorId(int id)
        {
            using (var context = new SqlServerContext(_dbContextOptions))
            {
                return await context.Set<Usuario>().FindAsync(id);
            }
        }

        public async Task Excluir(int id)
        {
            using (var context = new SqlServerContext(_dbContextOptions))
            {
                var objeto = await context.Set<Usuario>().FindAsync(id); ;
                context.Set<Usuario>().Remove(objeto);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Paginacao<Usuario>> Paginar(int pagina, int paginaQuantidade, Expression<Func<Usuario, bool>> expression = null)
        {
            using (var context = new SqlServerContext(_dbContextOptions))
            {
                IQueryable<Usuario> query = context.Set<Usuario>().AsNoTracking();

                if (expression != null)
                {
                    query = query.Where(expression);
                }

                var item =  query.ToList();

                return new Paginacao<Usuario>
                {
                    Itens = item.Skip((pagina - 1) * paginaQuantidade).Take(paginaQuantidade).ToList(),
                    Total = item.Count,
                    Pagina = pagina,
                    PaginaQuantidade = paginaQuantidade
                };
            }
        }

    }
}
