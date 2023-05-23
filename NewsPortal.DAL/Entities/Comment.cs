namespace NewsPortal.DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public System.DateTime Date_Created { get; set; }
    }
}
