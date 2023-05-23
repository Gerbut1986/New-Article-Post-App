namespace NewsPortal.BLL.Services
{
    using DAL.UOW;
    using System.Linq;
    using DAL.Interfaces;
    using NewsPortal.BLL.Dtos;
    using NewsPortal.BLL.Helpers;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class NewsPortalService : Interfaces.INewsPostalService
    {
        private readonly IUnitOfWork Db;

        public NewsPortalService(string connStr)
        {
            Db = new UnitOfWork(conn: connStr);
        }

        public void Delete(ArticleDto model)
        {
            Db.Articles.Delete(FillObject.Article(model).Id);
        }

        public void Delete(CommentDto model)
        {
            Db.Comments.Delete(FillObject.Comment(model).Id);
        }

        public async Task DeleteAsync(int id)
        {
            await Db.Articles.DeleteAsync(id);
        }

        public async Task DeleteCommentAsync(int id)
        {
            await Db.Comments.DeleteAsync(id);
        }

        public void Insert(ArticleDto model)
        {
            Db.Articles.Create(FillObject.Article(model));
        }

        public void Insert(CommentDto model)
        {
            Db.Comments.Create(FillObject.Comment(model));
        }

        public async Task InsertAsync(ArticleDto model)
        {
            await Db.Articles.CreateAsync(FillObject.Article(model));
        }

        public async Task InsertAsync(CommentDto model)
        {
            await Db.Comments.CreateAsync(FillObject.Comment(model));
        }

        public IEnumerable<ArticleDto> ReadAll()
        {
            return FillObject.ArticlesList(Db.Articles.GetAll().ToList());
        }

        public async Task<IEnumerable<ArticleDto>> ReadAllAsync()
        {
            return FillObject.ArticlesList(await Db.Articles.GetAllAsync());
        }

        public IEnumerable<CommentDto> ReadComments()
        {
            return FillObject.CommentsList(Db.Comments.GetAll().ToList());
        }

        public async Task<IEnumerable<CommentDto>> ReadCommentsAsync()
        {
            return FillObject.CommentsList(await Db.Comments.GetAllAsync());
        }

        public void Update(ArticleDto model)
        {
            Db.Articles.Update(FillObject.Article(model));
        }

        public void Update(CommentDto model)
        {
            Db.Comments.Update(FillObject.Comment(model));
        }

        public async Task UpdateAsync(int id)
        {
            await Db.Articles.UpdateAsync(id);
        }

        public async Task UpdateCommentAsync(int id)
        {
            await Db.Comments.UpdateAsync(id);
        }
    }
}
