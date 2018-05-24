using BurgerMaker.Interfaces;
using BurgerMaker.ViewModels;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BurgerMaker.HamburgerViewModelBuilders
{
    public class NormalBurgerViewModelBuilder : IHamburgerViewModelBuilder
    {
        public GetABurgerViewModel HamburgerViewModel { get; }
        private Burger burgerWithMeat;

        public NormalBurgerViewModelBuilder(Burger burgerWithMeat)
        {
            this.HamburgerViewModel = new GetABurgerViewModel();
            this.burgerWithMeat = burgerWithMeat;
        }

        public void AddBuns()
        {
            this.HamburgerViewModel.BunType = burgerWithMeat.Bun.Type;
        }

        public void AddMeats()
        {
            this.HamburgerViewModel.Meats = burgerWithMeat.Ingredients.Where(x => x.IngredientType.Type == "Meats").Select(x => x.Name).ToList();
        }

        public void AddSauces()
        {
            this.HamburgerViewModel.Sauces = burgerWithMeat.Ingredients.Where(x => x.IngredientType.Type == "Sauces").Select(x => x.Name).ToList();
        }

        public void AddVegetables()
        {
            this.HamburgerViewModel.Vegetables = burgerWithMeat.Ingredients.Where(x => x.IngredientType.Type == "Vegetables").Select(x => x.Name).ToList();
        }

        public void AddName()
        {
            this.HamburgerViewModel.BurgerName = burgerWithMeat.Name;
        }
    }
}