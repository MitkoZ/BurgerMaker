using BurgerMaker.HamburgerViewModelBuilders;
using BurgerMaker.Helpers;
using BurgerMaker.Interfaces;
using BurgerMaker.ViewModels;
using DataAccess;
using DataAccess.Interfaces;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BurgerMaker.Controllers
{
    public class BurgerController : Controller
    {
        private IIngredientTypeRepository ingredientTypeRepository;
        private IUserRepository userRepository;
        private IBunRepository bunRepository;
        private IIngredientRepository ingredientRepository;
        private IDbContext dbContext;

        public BurgerController(IDbContext dbContext, IIngredientTypeRepository ingredientTypeRepository, IBunRepository bunRepository, IUserRepository userRepository, IIngredientRepository ingredientRepository)
        {
            this.dbContext = dbContext;
            this.ingredientTypeRepository = ingredientTypeRepository;
            this.bunRepository = bunRepository;
            this.userRepository = userRepository;
            this.ingredientRepository = ingredientRepository;
        }

        public ActionResult MakeANormalBurger()
        {
            List<IngredientType> ingredientTypesDb = ingredientTypeRepository.GetAll();
            return View("MakeABurger", CreateMakeABurgerGetViewModel(ingredientTypesDb));
        }

        /// <summary>
        /// A helper method for generating a BurgerViewModel
        /// </summary>
        /// <param name="possibleIngredientTypesDb">The ingredients that should be included in the view model</param>
        /// <returns>The already created BurgerViewModel for the get request filled with buns and ingredients data</returns>
        private MakeABurgerGetViewModel CreateMakeABurgerGetViewModel(List<IngredientType> possibleIngredientTypesDb)
        {
            Dictionary<string, List<Ingredient>> ingredientsDb = new Dictionary<string, List<Ingredient>>();
            foreach (IngredientType ingredientTypeDb in possibleIngredientTypesDb)
            {
                ingredientsDb[ingredientTypeDb.Type] = ingredientTypeDb.Ingredients.ToList();
            }
            List<Bun> bunsDb = bunRepository.GetAll();

            MakeABurgerGetViewModel makeABurgerGetViewModel = new MakeABurgerGetViewModel(bunsDb, ingredientsDb);
            return makeABurgerGetViewModel;
        }

        public ActionResult MakeAVegetarianBurger()
        {
            List<IngredientType> ingredientTypesDb = ingredientTypeRepository.GetAll(x => x.Type != "Meats");
            return View("MakeABurger", CreateMakeABurgerGetViewModel(ingredientTypesDb));
        }

        [HttpPost]
        public ActionResult MakeABurger(MakeABurgerPostViewModel makeABurgerPostViewModel)
        {
            User currentUserDb = userRepository.GetAll(x => x.Id == LoginUserSession.Current.UserId).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = ModelState.Where(x => x.Value.Errors.Count > 0).FirstOrDefault().Value.Errors.FirstOrDefault().ErrorMessage;
                return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
            }
            if (currentUserDb == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (makeABurgerPostViewModel.ChosenBunId <= 0)
            {
                TempData["ErrorMessage"] = "Please select a bun type";
                return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
            }
            if (makeABurgerPostViewModel.ChosenIngredientsIds.Count == 0)
            {
                TempData["ErrorMessage"] = "Please select at least 1 ingredient for the burger";
                return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
            }
            if (currentUserDb.Burgers.Any(x => x.Name == makeABurgerPostViewModel.BurgerName))
            {
                TempData["ErrorMessage"] = "You have already created a burger with this name!";
                return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
            }

            List<Ingredient> chosenIngredients = new List<Ingredient>();
            makeABurgerPostViewModel.ChosenIngredientsIds.ForEach(x => chosenIngredients.Add(ingredientRepository.GetAll(c => c.Id == x).FirstOrDefault()));
            Bun chosenBun = bunRepository.GetAll(x => x.Id == makeABurgerPostViewModel.ChosenBunId).FirstOrDefault();
            currentUserDb.Burgers.Add(new Burger
            {
                Name = makeABurgerPostViewModel.BurgerName,
                Ingredients = chosenIngredients,
                Bun = chosenBun
            });

            bool isSaved = dbContext.SaveChanges() > 0;

            if (isSaved)
            {
                TempData["SuccessMessage"] = "Burger created successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Ooops something went wrong";
            }
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }


        public ActionResult GetMyNormalBurgers()
        {
            User currentUserDb = userRepository.GetAll(x => x.Id == LoginUserSession.Current.UserId).FirstOrDefault();
            List<Burger> currentUserBurgersWithMeat = currentUserDb.Burgers.Where(x => x.Ingredients.Any(c => c.IngredientType.Type == "Meats")).ToList();
            List<GetABurgerViewModel> getABurgerViewModelList = new List<GetABurgerViewModel>();
            Waiter waiter = new Waiter();
            foreach (Burger burgerWithMeat in currentUserBurgersWithMeat)
            {
                IHamburgerViewModelBuilder hamburgerViewModelBuilder = new NormalBurgerViewModelBuilder(burgerWithMeat);
                waiter.SetHamburgerViewModelBuilder(hamburgerViewModelBuilder);
                waiter.ConstructHamburgerViewModel();
                getABurgerViewModelList.Add(waiter.GetABurgerViewModel());
            }
            return View("GetMyBurgers", getABurgerViewModelList);
        }

        public ActionResult GetMyVegetarianBurgers()
        {
            User currentUserDb = userRepository.GetAll(x => x.Id == LoginUserSession.Current.UserId).FirstOrDefault();
            List<Burger> currentUserBurgersWithoutMeat = currentUserDb.Burgers.Where(x => !(x.Ingredients.Any(c => c.IngredientType.Type == "Meats"))).ToList();
            List<GetABurgerViewModel> getABurgerViewModelList = new List<GetABurgerViewModel>();
            Waiter waiter = new Waiter();
            foreach (Burger burgerWithoutMeat in currentUserBurgersWithoutMeat)
            {
                IHamburgerViewModelBuilder hamburgerViewModelBuilder = new VegetarianBurgerViewModelBuilder(burgerWithoutMeat);
                waiter.SetHamburgerViewModelBuilder(hamburgerViewModelBuilder);
                waiter.ConstructHamburgerViewModel();
                getABurgerViewModelList.Add(waiter.GetABurgerViewModel());
            }
            return View("GetMyBurgers", getABurgerViewModelList);
        }

    }
}