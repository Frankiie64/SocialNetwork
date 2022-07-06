using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Domain.Entities;
using RedSocial.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Infrastructure.Persistence.Repositories
{
   public class PublicationRepository : GenericRepository<Publications>, IPublicationRepository
    {
        private readonly ApplicationDbContext _db;
        public PublicationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
