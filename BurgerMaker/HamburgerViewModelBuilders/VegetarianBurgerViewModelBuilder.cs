using BurgerMaker.ViewModels;
using DataAccess;
using System.Linq;

namespace BurgerMaker.HamburgerViewModelBuilders
{
    public class VegetarianBurgerViewModelBuilder : IHamburgerViewModelBuilder
    {
        public GetABurgerViewModel HamburgerViewModel { get; }
        private Burger vegetarianBurger;

        public VegetarianBurgerViewModelBuilder(Burger vegetarianBurger)
        {
            this.HamburgerViewModel = new GetABurgerViewModel();
            this.vegetarianBurger = vegetarianBurger;
        }

        public void AddBuns()
        {
            this.HamburgerViewModel.BunType = vegetarianBurger.Bun.Type;
        }

        public void AddMeats()
        {
            this.HamburgerViewModel.Meats = null;
        }

        public void AddSauces()
        {
            this.HamburgerViewModel.Sauces = vegetarianBurger.Ingredients.Where(x => x.IngredientType.Type == "Sauces").Select(x => x.Name).ToList();
        }

        public void AddVegetables()
        {
            this.HamburgerViewModel.Vegetables = vegetarianBurger.Ingredients.Where(x => x.IngredientType.Type == "Vegetables").Select(x => x.Name).ToList();
        }

        public void AddName()
        {
            this.HamburgerViewModel.BurgerName = vegetarianBurger.Name;
        }
    }
}