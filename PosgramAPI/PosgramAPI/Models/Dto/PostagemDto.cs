namespace PosgramAPI.Models.Dto
{
    public class PostagemDto
    {
        public int Id { get; set; }
        public string Autor { get; set; }
        public string Legenda { get; set; }
        public string Imagem { get; set; }
        public DateTime? DataHoraPostagem { get; set; }
    }
}
