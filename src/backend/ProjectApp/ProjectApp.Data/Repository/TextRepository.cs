using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using ProjectApp.Data.Context;
using ProjectApp.Interface.Repository;
using ProjectApp.Domain.Entities;

namespace ProjectApp.Data.Repository
{
    public class TextRepository : GenericRepository<Text>, ITextRepository
    {
        private readonly ILogger<TextRepository> _logger;
        private readonly AppDbContext _context;

        public TextRepository(ILogger<TextRepository> logger, AppDbContext context) : base(logger, context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<Text>> GetAllWithSomethingElseAsync()
        {
            throw new NotImplementedException();
        }
    }
}
