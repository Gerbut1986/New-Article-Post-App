namespace NewsPortal.BLL.Interfaces
{
    using NewsPortal.BLL.Dtos;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface INewsPostalService
    {
        #region Article:
        void Insert(ArticleDto model);
        Task InsertAsync(ArticleDto model);
        void Update(ArticleDto model);
        Task UpdateAsync(int id);
        void Delete(ArticleDto model);
        Task DeleteAsync(int id);
        IEnumerable<ArticleDto> ReadAll();
        Task<IEnumerable<ArticleDto>> ReadAllAsync();
        #endregion
        #region Comment:
        void Insert(CommentDto model);
        Task InsertAsync(CommentDto model);
        void Update(CommentDto model);
        Task UpdateCommentAsync(int id);
        void Delete(CommentDto model);
        Task DeleteCommentAsync(int id);
        IEnumerable<CommentDto> ReadComments();
        Task<IEnumerable<CommentDto>> ReadCommentsAsync();
        #endregion
    }
}
