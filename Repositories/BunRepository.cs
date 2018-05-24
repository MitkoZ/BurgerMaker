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
    public class BunRepository : BaseRepository<Bun>, IBunRepository
    {
        public BunRepository(IDbContext burgerMakerDbContext) : base(burgerMakerDbContext)
        {
        }
    }
}
