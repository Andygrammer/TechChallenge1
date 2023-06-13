using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class BaseModel
    {
        [DataMember]
        public string Id { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Postagem : BaseModel 
    { 
        /// <summary>
        /// Construtor
        /// </summary>
        public Postagem() { }

        public string Autor { get; set; }
        public string Legenda { get; set; }
        public string Imagem { get; set; }
        public DateTime DataHora { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="autor">Nome do Autor</param>
        /// <param name="legenda">Legenda do Post</param>
        /// <param name="imagem">Imagem do Post</param>
        /// <param name="dataHora">Data e Hora da Postagem</param>
        public Postagem(string autor, 
                        string legenda, 
                        string imagem, 
                        DateTime dataHora)
        {
            Autor = autor;
            Legenda = legenda;
            Imagem = imagem;
            DataHora = dataHora;
        }
    }
}
