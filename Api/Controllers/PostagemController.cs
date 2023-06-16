using Api.Repository.IRepository;
using Api.Services;
using AutoMapper;
using Infraestrutura.Models;
using Microsoft.AspNetCore.Mvc;
using PosgramAPI.Models.Dto;

namespace PosgramAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PostagemController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly BlobServices _blobServices;
        private readonly IPostagemRepository _dbPostagem;
        private readonly IMapper _mapper;

        public PostagemController(IConfiguration configuration, IPostagemRepository dbPostagem, IMapper mapper)
        {
            _configuration = configuration;
            _dbPostagem = dbPostagem;
            _mapper = mapper;
            _blobServices = new BlobServices(_configuration);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostagemDto>>> GetPostagensAsync()
        {
            IEnumerable<Postagem> PostagemLista = await _dbPostagem.GetAllPosts();
            return Ok(PostagemLista);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PostagemDto>> CreatePostagemAsync([FromBody] PostagemDto postagemDto)
        {
            if (postagemDto == null) return BadRequest(postagemDto);

            var ulrImagem = await _blobServices.CreateBlob(postagemDto);
            postagemDto.Imagem = ulrImagem;
            Postagem modelPostagem = _mapper.Map<Postagem>(postagemDto);
            await _dbPostagem.CreateAsync(modelPostagem);
            return new CreatedResult(ulrImagem, postagemDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeletePostagem(int id)
        {
            if (id == 0) return BadRequest();
            
            var postagem = await _dbPostagem.GetAsync(u => u.Id == id);

            if (postagem == null) return NotFound();

            await _blobServices.DeleteBlob(postagem.Imagem);

            await _dbPostagem.RemoveAsync(postagem);

            return NoContent();
        }

    }
}
