using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BurgerMaker.ViewModels
{
    public class MakeABurgerPostViewModel
    {
        [Required]
        public string BurgerName { get; set; }
        public int ChosenBunId { get; set; }
        public List<int> ChosenIngredientsIds { get; set; }
        public MakeABurgerPostViewModel()
        {
            this.ChosenIngredientsIds = new List<int>();
        }
    }
}