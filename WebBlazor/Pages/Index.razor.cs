using WebBlazor.Helpers;
using WebBlazor.Interfaces;
using WebBlazor.Models;
using WebBlazor.Services;

namespace WebBlazor.Pages
{
    public partial class Index : BlazorComponent
    {
        [Microsoft.AspNetCore.Components.Inject]
        public IPostagemService postagemService { get; set; }

        private IEnumerable<PostagemDto> posts = null;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                posts = await postagemService.GetPostagensAsync();

                CallRequestRefresh();
            }


            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
