using Infraestrutura.Models;
using PosgramAPI.Models.Dto;
using static System.Net.Mime.MediaTypeNames;

namespace PosgramAPI.Data
{
    public class PostagemStore
    {
        public static List<PostagemDto> listaPostagem = new List<PostagemDto>
        {
            new PostagemDto {Id=1, Autor="Caio", Legenda="Lorem ipsum dolor sit amet, consectetur adipiscing elit.", Imagem="https://picsum.photos/200/300", DataHoraPostagem=DateTime.Now },
             new PostagemDto {Id=2, Autor="Maria", Legenda="Lorem ipsum dolor sit amet, consectetur adipiscing elit.", Imagem="https://picsum.photos/200/300", DataHoraPostagem=DateTime.Now},
              new PostagemDto {Id=3, Autor="Thiago", Legenda="Lorem ipsum dolor sit amet, consectetur adipiscing elit.", Imagem="https://picsum.photos/200/300", DataHoraPostagem=DateTime.Now }
        };
    }
}
