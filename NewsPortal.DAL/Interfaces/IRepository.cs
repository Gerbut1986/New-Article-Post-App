namespace NewsPortal.DAL.Interfaces
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<IEntity> where IEntity : class, new()
    {
        IEntity Get(int id);
        Task<IEntity> GetAsync(int id);
        void Create(IEntity entity);
        Task CreateAsync(IEntity entity);
        void Update(IEntity entity);
        Task UpdateAsync(int id);
        void Delete(int id); 
        Task DeleteAsync(int id);
        IQueryable<IEntity> GetAll();
        Task<System.Collections.Generic.List<IEntity>> GetAllAsync();
    }
}
