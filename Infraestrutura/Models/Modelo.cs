using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Infraestrutura.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Postagem 
    {
        [DataMember]
        [Key]
        public int Id { get; }

        [Required]
        public string Autor { get; set; }
        [Required]
        public string Legenda { get; set; }
        [Required]
        public string Imagem { get; set; }
        [Required]
        public DateTime DataHora { get; set; }

        public string ImagemAutor { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="autor">Nome do Autor</param>
        /// <param name="legenda">Legenda do Post</param>
        /// <param name="imagem">Imagem do Post</param>
        /// <param name="dataHora">Data e Hora da Postagem</param>
        /// /// <param name="imagemAutor">Imagem de perfil do Autor do Post</param>
        public Postagem(string autor, 
                        string legenda, 
                        string imagem,
                        string imagemAutor,
                        DateTime dataHora)
        {
            Autor = autor;
            Legenda = legenda;
            Imagem = imagem;
            DataHora = dataHora;
            ImagemAutor = imagemAutor;
        }

        public Postagem()
        {
        }
    }
}
