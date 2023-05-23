namespace NewsPortal.DAL.UOW
{
    using Repositories;
    using Interfaces;
    using Entities;
    using EF;

    public class UnitOfWork : IUnitOfWork
    {
        readonly DataContext db;

        #region All Repos:    
        private ArticleRepository articleRepo;
        private CommentRepository commentRepo;
        #endregion

        public UnitOfWork(string conn) => db = new DataContext(conn);

        public IRepository<Article> Articles
        {
            get
            {
                if (articleRepo == null)
                    articleRepo = new ArticleRepository(db);
                return articleRepo;
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                if (commentRepo == null)
                    commentRepo = new CommentRepository(db);
                return commentRepo;
            }
        }

        #region Dispose:
        bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    db.Dispose();

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
        #endregion

        public async System.Threading.Tasks.Task<int> SaveAsync() => await db.SaveChangesAsync();
    }
}
