using Infraestrutura.Models;

namespace Api.Repository.IRepository
{
    public interface IPostagemRepository :  IRepository<Postagem>
    {
        Task<Postagem> UpdateAsync(Postagem entity);
        Task<List<Postagem>> GetAllPosts();
    }
}
