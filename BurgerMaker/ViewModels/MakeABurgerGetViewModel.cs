using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BurgerMaker.ViewModels
{
    public class MakeABurgerGetViewModel
    {
        public List<BunViewModel> PossibleBunsViewModel { get; set; }
        public Dictionary<string, List<IngredientViewModel>> PossibleIngredientsViewModel { get; set; }

        public MakeABurgerGetViewModel(List<DataAccess.Bun> bunsDb, Dictionary<string, List<DataAccess.Ingredient>> ingredientsDb)
        {
            this.PossibleBunsViewModel = new List<BunViewModel>();
            this.PossibleIngredientsViewModel = new Dictionary<string, List<IngredientViewModel>>();
            foreach (DataAccess.Bun bun in bunsDb)
            {
                this.PossibleBunsViewModel.Add(new BunViewModel
                {
                    Id = bun.Id,
                    Type = bun.Type
                });
            }

            foreach (KeyValuePair<string, List<DataAccess.Ingredient>> ingredientDbKeyValuePair in ingredientsDb) // the key is the type of the ingredient from IngredientTypes table
            {
                foreach (DataAccess.Ingredient ingredientDb in ingredientDbKeyValuePair.Value)
                {
                    if (!PossibleIngredientsViewModel.ContainsKey(ingredientDbKeyValuePair.Key))
                    {
                        this.PossibleIngredientsViewModel.Add(ingredientDbKeyValuePair.Key, new List<IngredientViewModel>
                        {

                          new IngredientViewModel
                          {
                                Id = ingredientDb.Id,
                                Name = ingredientDb.Name
                          }

                        });

                    }
                    else // it already constains the key
                    {
                        PossibleIngredientsViewModel[ingredientDbKeyValuePair.Key].Add(new IngredientViewModel
                        {
                            Id = ingredientDb.Id,
                            Name = ingredientDb.Name
                        });

                    }
                }
            }
        }


        [Display(Name = "Burger Name")]
        public string BurgerName { get; set; }

        public class BunViewModel
        {
            public int Id { get; set; }
            public string Type { get; set; }
        }

        public class IngredientViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

    }
}