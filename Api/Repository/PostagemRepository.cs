using Api.Repository.IRepository;
using Infraestrutura;
using Infraestrutura.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class PostagemRepository : Repository<Postagem>, IPostagemRepository
    {
        private readonly ApplicationDbContext _db;

        public PostagemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Postagem>> GetAllPosts() => 
            await _db.Postagem.Take(100).OrderByDescending(p => p.DataHora).ToListAsync();


        public async Task<Postagem> UpdateAsync(Postagem entity)
        {
            _db.Postagem.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
