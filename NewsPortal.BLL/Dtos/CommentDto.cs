namespace NewsPortal.BLL.Dtos
{
    public class CommentDto : BLL.Interfaces.IModel
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public System.DateTime Date_Created { get; set; }
    }
}
