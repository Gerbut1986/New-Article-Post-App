namespace NewsPortal.PL.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using NewsPortal.BLL.Dtos;
    using System.Threading.Tasks;
    using NewsPortal.BLL.Services;

    public class DeleteController : Controller
    {
        private readonly NewsPortalService service;

        public DeleteController()
        {
            service = new NewsPortalService(_829528a_441d_484m_862i_22475963ffdn.Credential.ConnStr);
        }

        public ActionResult Article(int? id)
        {
            return View(service.ReadAll().FirstOrDefault(a => a.Id == id));
        }

        [HttpPost]
        public ActionResult Article(ArticleDto article)
        {
            service.Delete(article);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Comment(int? idComment)
        {
            var found = service.ReadComments().FirstOrDefault(a => a.Id == idComment);
            return View(found);
        }

        [HttpPost]
        public async Task<ActionResult> Comment(CommentDto comment)
        {
            var comments = await service.ReadCommentsAsync();
            var found = comments.FirstOrDefault(u => u.Id == comment.Id);
            service.Delete(found);
            return RedirectToAction("Comments", "Home", new { idArticle = found.ArticleId });
        }
    }
}