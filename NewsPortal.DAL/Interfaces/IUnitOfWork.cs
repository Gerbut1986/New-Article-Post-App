namespace NewsPortal.DAL.Interfaces
{
    using Entities;

    public interface IUnitOfWork : System.IDisposable
    {
        IRepository<Article> Articles { get; }
        IRepository<Comment> Comments { get; }
        System.Threading.Tasks.Task<int> SaveAsync();
    }
}
