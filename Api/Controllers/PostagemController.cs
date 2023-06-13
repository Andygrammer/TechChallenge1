﻿using Infraestrutura;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PosgramAPI.Data;
using PosgramAPI.Models.Dto;

namespace PosgramAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PostagemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostagemController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostagemDto>> GetPostagens()
        {
            var get = _context;
            return Ok(PostagemStore.listaPostagem);
        }

        [HttpGet("{id:int}", Name = "GetPostagem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PostagemDto> GetPostagem(int id)
        {
            var get = _context;
            if (id == 0)
            {
                return BadRequest();
            }
            var postagem = PostagemStore.listaPostagem.FirstOrDefault(u => u.Id == id);

            if (postagem == null)
            {
                return NotFound();
            }
            return Ok(postagem);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PostagemDto> CreatePostagem([FromBody] PostagemDto postagemDto)
        {
            if (postagemDto == null)
            {
                return BadRequest(postagemDto);
            }
            if (postagemDto.Id > 0)
            {
                return BadRequest();
            }
            postagemDto.Id = PostagemStore.listaPostagem.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;

            PostagemStore.listaPostagem.Add(postagemDto);

            return CreatedAtRoute("GetPostagem", new { id = postagemDto.Id }, postagemDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeletePostagem(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var postagem = PostagemStore.listaPostagem.FirstOrDefault(u => u.Id == id);

            if (postagem == null)
            {
                return NotFound();
            }

            PostagemStore.listaPostagem.Remove(postagem);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdatePostagem(int id, [FromBody] PostagemDto postagemDto)
        {
            if (postagemDto == null || id != postagemDto.Id)
            {
                return BadRequest();
            }

            var postagem = PostagemStore.listaPostagem.FirstOrDefault(u => u.Id == id);

            if (postagem == null)
            {
                return NotFound();
            }

            postagem.Autor = postagemDto.Autor;
            postagem.Legenda = postagemDto.Legenda;
            postagem.DataHoraPostagem = postagemDto.DataHoraPostagem;
            postagem.Imagem = postagemDto.Imagem;

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public ActionResult PatchPostagem(int id, JsonPatchDocument<PostagemDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var postagem = PostagemStore.listaPostagem.FirstOrDefault(u => u.Id == id);

            if (postagem == null)
            {
                return NotFound();
            }

            patchDto.ApplyTo(postagem, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();

        }

    }
}