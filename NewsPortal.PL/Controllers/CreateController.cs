namespace NewsPortal.PL.Controllers
{
    using NewsPortal.BLL.Services;
    using NewsPortal.BLL.Dtos;
    using System.Web.Mvc;
    using System.IO;
    using System.Web;
    using System;

    public class CreateController : Controller
    {
        private static string _path;
        private static int _articleId, _userId;
        private static string PhotoPath { get; set; }
        private readonly NewsPortalService service;

        public CreateController()
        {
            service = new NewsPortalService(_829528a_441d_484m_862i_22475963ffdn.Credential.ConnStr);
        }

        public ActionResult Article(int userId)
        {
            _userId = userId;
            return View();
        }

        [HttpPost]
        public ActionResult Article(ArticleDto article)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    article.Image_Url = PhotoPath;
                    article.Date_Created = DateTime.Now;
                    article.UserId = _userId;
                    service.Insert(article);
                    return RedirectToAction("Index", "Home");
                }
                catch { return View(); }
            }
            return View();
        }

        public ActionResult Comment(int idArticle, int userId)
        {
            _userId = userId;
            _articleId = idArticle;
            return View();
        }

        [HttpPost]
        public ActionResult Comment(CommentDto comment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    comment.Date_Created = DateTime.Now;
                    comment.ArticleId = _articleId;
                    comment.UserId = _userId;
                    service.Insert(comment);
                    return RedirectToAction("Comments", "Home", new { idArticle = _articleId });
                }
                catch { return View(); }
            }
            return View();
        }

        #region Upload product photo[Post]:
        [HttpPost]
        public ActionResult PhotoUpload()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            PhotoPath = fname = testfiles[testfiles.Length - 1];
                        }
                        else fname = PhotoPath = file.FileName;
                        _path = Path.Combine(Server.MapPath($"~/Images/News/{fname}"));
                        file.SaveAs(_path);
                    }

                    return Json("Upload!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else return Json("U should to upload image!");
        }
        #endregion
    }
}