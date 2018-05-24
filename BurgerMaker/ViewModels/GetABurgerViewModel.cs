using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BurgerMaker.ViewModels
{
    public class GetABurgerViewModel
    {
        internal string BurgerName { private get; set; }
        internal string BunType { private get; set; }
        internal List<string> Sauces { private get; set; }
        internal List<string> Vegetables { private get; set; }
        internal List<string> Meats { private get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Burger Name: {BurgerName}");
            stringBuilder.AppendLine($"{Environment.NewLine} Buns types: {BunType}");
            stringBuilder.AppendLine($"{Environment.NewLine} Sauces:");
            AddItemsToStringBuilder(stringBuilder, Sauces);
            stringBuilder.AppendLine($"{Environment.NewLine} Vegetables:");
            AddItemsToStringBuilder(stringBuilder, Vegetables);
            if (Meats?.Count > 0)
            {
                stringBuilder.AppendLine($"{Environment.NewLine} Meats:");
                AddItemsToStringBuilder(stringBuilder, Meats);
            }
            return stringBuilder.ToString();
        }

        private void AddItemsToStringBuilder(StringBuilder stringBuilder, List<string> ingredients)
        {
            if (ingredients.Count > 0)
            {
                for (int i = 0; i < ingredients.Count; i++)
                {
                    if (i + 1 == ingredients.Count)
                    {
                        stringBuilder.AppendLine(" " + ingredients[i]);
                    }
                    else
                    {
                        stringBuilder.AppendLine(" " + ingredients[i] + ",");
                    }
                }
            }
            else
            {
                stringBuilder.AppendLine("None");
            }
        }

    }
}