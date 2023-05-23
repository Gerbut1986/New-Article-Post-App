namespace NewsPortal.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class CommentRepository : IRepository<Comment>
    {
        private readonly DataContext db;

        public CommentRepository(DataContext db)
        {
            this.db = db;
        }

        public void Create(Comment entity)
        {
            db.Comments.Add(entity);
            db.SaveChanges();
        }

        public async Task CreateAsync(Comment entity)
        {
            db.Comments.Add(entity);
            await db.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            db.Comments.Remove(Get(id) ?? null);
            db.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            db.Comments.Remove(await GetAsync(id) ?? null);
            await db.SaveChangesAsync();
        }

        public Comment Get(int id)
        {
            return db.Comments.Find(id);
        }

        public IQueryable<Comment> GetAll()
        {
            return db.Comments;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await (from ci in db.Comments select ci).ToListAsync();
        }

        public async Task<Comment> GetAsync(int id)
        {
            return await db.Comments.FindAsync(id);
        }

        public void Update(Comment entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Comments.FindAsync(id)).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
