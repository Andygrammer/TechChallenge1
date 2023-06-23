using WebBlazor.Models;

namespace WebBlazor.Interfaces
{
    public interface IPostagemService
    {
        Task<IEnumerable<PostagemDto>> GetPostagensAsync();

        Task<PostagemDto> CreatePostagemAsync(PostagemDto postagem);
    }
}
