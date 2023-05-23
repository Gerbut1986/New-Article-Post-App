namespace NewsPortal.PL.Controllers
{
    using Microsoft.AspNet.Identity.Owin;
    using NewsPortal.BLL.Services;
    using System.Threading.Tasks;
    using NewsPortal.PL.Models;
    using System.Web.Mvc;
    using System.Linq;
    using System.Web;

    public class HomeController : Controller
    {
        #region Fields & Properties:
        private int _userId = 0;
        private bool _isLogin = false;
        private string WelcomeStr { get; set; }
        private string CurrentUserEmail { get; set; } 
        private ApplicationUser CurrentLocalUser { get; set; }
        private readonly NewsPortalService db;

        #region SignInManager & UserManager:
        private ApplicationSignInManager _signInManager;
        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }
        #endregion
        #endregion

        public HomeController()
        {
            db = new NewsPortalService(_829528a_441d_484m_862i_22475963ffdn.Credential.ConnStr);
        }

        public async Task<ActionResult> Index()
        {
            var articles = await db.ReadAllAsync();
            var comments = await db.ReadCommentsAsync();

            ViewBag.IsLogin = _isLogin;
            ViewBag.UserId = _userId;
            ViewBag.Comments = comments;
            ViewBag.WelcomeStr = WelcomeStr;
            ViewBag.MyEmail = CurrentUserEmail;
            return View(articles);
        }

        public ActionResult FullArticle(int id)
        {
            ViewBag.IsLogin = _isLogin;
            ViewBag.Comments = db.ReadComments();
            return View(db.ReadAll().FirstOrDefault(i => i.Id == id));
        }

        public async Task<ActionResult> Comments(int? idArticle)
        {
            var comments = await db.ReadCommentsAsync();

            ViewBag.Users = UserManager.Users.ToList();
            ViewBag.CurrentUser = CurrentLocalUser;
            ViewBag.IsLogin = _isLogin;
            ViewBag.WelcomeStr = WelcomeStr;
            ViewBag.MyEmail = CurrentUserEmail;
            return View(comments.Where(a => a.ArticleId == idArticle).OrderByDescending(d => d.Date_Created));
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                CurrentUserEmail = filterContext.HttpContext.User.Identity.Name;
                try
                {
                    _isLogin = true;
                    CurrentLocalUser = UserManager.Users.FirstOrDefault(u => u.Email == CurrentUserEmail);
                    WelcomeStr = $"Hello {CurrentLocalUser.FirstName} {CurrentLocalUser.LastName} !";
                    _userId = CurrentLocalUser.UserId;
                }
                catch { }
            }
            else // When User Log off, or still doesn't enter 
            {
                CurrentUserEmail = null;
                CurrentLocalUser = new ApplicationUser { UserId = 0 };
            }
        }
    }
}