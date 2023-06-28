using Microsoft.Extensions.Options;
using WebBlazor.Interfaces;
using WebBlazor.Models;

namespace WebBlazor.Services
{
    public class PostagemService : IPostagemService
    {
        private readonly HttpService _httpService;
        private readonly string _blobUrl;
        private readonly string _tokenAccess;
        public PostagemService(HttpService httpService, IOptions<BaseUrlConfiguration> baseUrlConfiguration)
        {
            _httpService = httpService;
            _blobUrl = baseUrlConfiguration.Value.BlobEndPoint;
            _tokenAccess = baseUrlConfiguration.Value.TokenAccess;
        }

        public async Task<IEnumerable<PostagemDto>> GetPostagensAsync()
        {
            var itemGetTask = await _httpService.HttpGet<List<PostagemDto>>("api/Postagem");
            foreach(var item in itemGetTask)
            {
                item.Imagem = String.Concat(_blobUrl + item.Imagem + _tokenAccess);
            }
            return itemGetTask;
        }

        public async Task<PostagemDto> CreatePostagemAsync(PostagemDto postagem)
        {
            return await _httpService.HttpPost<PostagemDto>("api/Postagem", postagem);
        }

        public async Task DeletePostagemAsync(int id)
        {
            await _httpService.HttpDelete<PostagemDto>("api/Postagem", id);
        }
    }
}
