namespace NewsPortal.DAL.EF
{
    using NewsPortal.DAL.Entities;
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext(string connStr) 
            : base(connStr)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
    }
}
