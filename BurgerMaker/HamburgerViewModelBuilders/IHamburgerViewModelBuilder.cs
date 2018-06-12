using BurgerMaker.ViewModels;

namespace BurgerMaker.HamburgerViewModelBuilders
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
