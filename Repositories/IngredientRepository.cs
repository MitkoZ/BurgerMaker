using DataAccess;
using DataAccess.Interfaces;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(IDbContext burgerMakerDbContext) : base(burgerMakerDbContext)
        {
        }
    }
}
