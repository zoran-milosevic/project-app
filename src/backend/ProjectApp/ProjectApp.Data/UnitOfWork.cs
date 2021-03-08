using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using ProjectApp.Data.Context;
using ProjectApp.Interface.Repository;

namespace ProjectApp.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default(CancellationToken));
        void Commit();
        void Rollback();
        IUserProfileRepository UserProfileRepository();
        ITextRepository TextRepository();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        private readonly AppDbContext _context;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ITextRepository _textRepository;

        public UnitOfWork(AppDbContext context,
            IUserProfileRepository userProfileRepository,
            ITextRepository textRepository)
        {
            _context = context;
            _userProfileRepository = userProfileRepository;
            _textRepository = textRepository;
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken)) => await _context.SaveChangesAsync(cancellationToken);
        public async Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default(CancellationToken)) => await _context.Database.BeginTransactionAsync(cancellationToken);
        public void Commit() => _context.Database.CommitTransaction();
        public void Rollback() => _context.Database.RollbackTransaction();

        // REPOSITORY
        public IUserProfileRepository UserProfileRepository() => _userProfileRepository;

        public ITextRepository TextRepository() => _textRepository;


        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
