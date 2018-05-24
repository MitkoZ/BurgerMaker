using BurgerMaker.ViewModels;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMaker.Interfaces
{
    internal interface IHamburgerViewModelBuilder
    {
        void AddName();
        void AddBuns();
        void AddSauces();
        void AddVegetables();
        void AddMeats();
        GetABurgerViewModel HamburgerViewModel { get; }
    }
}
