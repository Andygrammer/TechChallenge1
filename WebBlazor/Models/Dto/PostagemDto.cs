using System.ComponentModel.DataAnnotations;

namespace WebBlazor.Models
{
    public class PostagemDto
    {
        public int Id { get; set; }
        public string Autor { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Legenda { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Imagem { get; set; }
        public string NomeImagem { get; set; }
        public DateTime DataHora { get; set; }
    }
}
