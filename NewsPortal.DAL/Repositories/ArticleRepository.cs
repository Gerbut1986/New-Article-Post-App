namespace NewsPortal.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class ArticleRepository : IRepository<Article>
    {
        private readonly DataContext db;

        public ArticleRepository(DataContext db)
        {
            this.db = db;
        }

        public void Create(Article entity)
        {
            db.Articles.Add(entity);
            db.SaveChanges();
        }

        public async Task CreateAsync(Article entity)
        {
            db.Articles.Add(entity);
            await db.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            db.Articles.Remove(Get(id) ?? null);
            db.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            db.Articles.Remove(await GetAsync(id) ?? null);
            await db.SaveChangesAsync();
        }

        public Article Get(int id)
        {
            return db.Articles.Find(id);
        }

        public IQueryable<Article> GetAll()
        {
            return db.Articles;
        }

        public async Task<List<Article>> GetAllAsync()
        {
            return await (from ci in db.Articles select ci).ToListAsync();
        }

        public async Task<Article> GetAsync(int id)
        {
            return await db.Articles.FindAsync(id);
        }

        public void Update(Article entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Articles.FindAsync(id)).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
