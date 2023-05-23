namespace NewsPortal.BLL.Dtos
{
    public class ArticleDto : BLL.Interfaces.IModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image_Url { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public System.DateTime Date_Created { get; set; }
    }
}
