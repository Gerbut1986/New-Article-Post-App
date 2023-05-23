namespace NewsPortal.BLL.Helpers
{
    using Dtos;
    using DAL.Entities;
    using System.Collections.Generic;

    public class FillObject
    {
        #region Article fill it in:
        public static IEnumerable<ArticleDto> ArticlesList(List<Article> list)
        {
            var dto = new List<ArticleDto>();
            for (int i = 0; i < list.Count; i++)
            {
                dto.Add(new ArticleDto());
                dto[i].Id = list[i].Id;
                dto[i].Title = list[i].Title;
                dto[i].Text = list[i].Text;
                dto[i].Date_Created = list[i].Date_Created;
                dto[i].Image_Url = list[i].Image_Url;
                dto[i].UserId = list[i].UserId;
            }
            return dto;
        }

        public static Article Article(ArticleDto dto)
        {
            if (dto != null)
                return new Article
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    Text = dto.Text,
                    Date_Created = dto.Date_Created,
                    Image_Url = dto.Image_Url,
                    UserId = dto.UserId
                };
            else return null;
        }
        #endregion
        #region Comments fill it in:
        public static IEnumerable<CommentDto> CommentsList(List<Comment> list)
        {
            var dto = new List<CommentDto>();
            for (int i = 0; i < list.Count; i++)
            {
                dto.Add(new CommentDto());
                dto[i].Id = list[i].Id;
                dto[i].ArticleId = list[i].ArticleId; 
                dto[i].UserId = list[i].UserId;
                dto[i].Date_Created = list[i].Date_Created;
                dto[i].Text = list[i].Text;
            }
            return dto;
        }

        public static Comment Comment(CommentDto dto)
        {
            if (dto != null)
                return new Comment
                {
                    Id = dto.Id,
                    ArticleId = dto.ArticleId,
                    Date_Created = dto.Date_Created,
                    Text = dto.Text,
                    UserId = dto.UserId
                };
            else return null;
        }
        #endregion     
    }
}
