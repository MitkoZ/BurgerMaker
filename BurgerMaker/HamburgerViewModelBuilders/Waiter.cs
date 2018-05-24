using BurgerMaker.Interfaces;
using BurgerMaker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BurgerMaker.HamburgerViewModelBuilders
{
    internal class Waiter
    {
        private IHamburgerViewModelBuilder hamburgerViewModelBuilder;
        internal void SetHamburgerViewModelBuilder(IHamburgerViewModelBuilder hamburgerViewModelBuilder)
        {
            this.hamburgerViewModelBuilder = hamburgerViewModelBuilder;
        }

        internal GetABurgerViewModel GetABurgerViewModel()
        {
            return hamburgerViewModelBuilder.HamburgerViewModel;
        }

        internal void ConstructHamburgerViewModel()
        {
            hamburgerViewModelBuilder.AddName();
            hamburgerViewModelBuilder.AddBuns();
            hamburgerViewModelBuilder.AddMeats();
            hamburgerViewModelBuilder.AddVegetables();
            hamburgerViewModelBuilder.AddSauces();
        }
    }
}