using Microsoft.Extensions.Options;
using WebBlazor.Interfaces;
using WebBlazor.Models;

namespace WebBlazor.Services
{
    public class PostagemService : IPostagemService
    {
        private readonly HttpService _httpService;
        private string _url = "https://localhost:7271/api/Postagem";
        private readonly string _blobUrl;
        private string token_access = "?sv=2022-11-02&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2023-07-07T09:34:31Z&st=2023-06-16T01:34:31Z&spr=https&sig=XGqsaMvjTBKUQ%2BRDnjYnksY4q5fDOzeE0qQAqnyKNZM%3D";
        public PostagemService(HttpService httpService, IOptions<BaseUrlConfiguration> baseUrlConfiguration)
        {
            _httpService = httpService;
            _blobUrl = baseUrlConfiguration.Value.BlobEndPoint;
        }

        public async Task<IEnumerable<PostagemDto>> GetPostagensAsync()
        {
            var itemGetTask = await _httpService.HttpGet<List<PostagemDto>>(_url);
            foreach(var item in itemGetTask)
            {
                item.Imagem = String.Concat(_blobUrl + item.Imagem + token_access);
            }
            return itemGetTask;
        }

        public async Task<PostagemDto> CreatePostagemAsync(PostagemDto postagem)
        {
            return await _httpService.HttpPost<PostagemDto>($"{_url}", postagem);
        }

        public async Task DeletePostagemAsync(int id)
        {
            await _httpService.HttpDelete<PostagemDto>($"{_url}", id);
        }
    }
}
