using Repositorypatern.UnitOfWork.Core.Interfaces;
using Repositorypatern.UnitOfWork.Core.Models;
using Repositorypatern.UnitOfWork.EF.Data;
using Repositorypatern.UnitOfWork.EF.Repositories;

namespace Repositorypatern.UnitOfWork.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            Authers = new BaseRepository<Author>(_applicationDbContext);
            Books = new BaseRepository<Book>(_applicationDbContext);
        }

        public IBaseRepository<Author> Authers { get; private set; }

        public IBaseRepository<Book> Books { get; private set; }

        public int Complete()
        {
            return _applicationDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}