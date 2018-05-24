using BurgerMaker.Helpers;
using BurgerMaker.ViewModels;
using DataAccess;
using DataAccess.Interfaces;
using Repositories.Interfaces;
using System.Linq;
using System.Web.Mvc;

namespace BurgerMaker.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private IUserRepository userRepository;
        private IDbContext dbContext;

        public HomeController(IUserRepository userRepository, IDbContext dbContext)
        {
            this.userRepository = userRepository;
            this.dbContext = dbContext;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = ModelState.Where(x => x.Value.Errors.Count > 0).FirstOrDefault().Value.Errors.FirstOrDefault().ErrorMessage;
                return RedirectToAction("Register", "Home");
            }

            User user = userRepository.GetAll(x => x.Username == registerViewModel.Username).FirstOrDefault();
            if (user != null)
            {
                TempData["ErrorMessage"] = "A user with this username already exists!";
                return RedirectToAction("Register", "Home");
            }
            user = new User();
            user.Username = registerViewModel.Username;
            userRepository.RegisterUser(user, registerViewModel.Password);
            bool isSaved = dbContext.SaveChanges() > 0;

            if (isSaved)
            {
                TempData["Message"] = "User was registered successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Ooops something went wrong";
            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User dbUser = userRepository.GetUserByNameAndPassword(viewModel.Username, viewModel.Password);

                bool isUserExists = dbUser != null;
                if (isUserExists)
                {
                    LoginUserSession.Current.SetCurrentUser(dbUser.Id, dbUser.Username);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username and/or password");
                }
            }
            return View();
        }


        public ActionResult Logout()
        {
            LoginUserSession.Current.Logout();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult SimulateAnException()
        {
            throw new System.Exception("Simulated exception");
        }
    }
}