using Repositorypatern.UnitOfWork.Core.Interfaces;
using Repositorypatern.UnitOfWork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorypatern.UnitOfWork.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Author> Authers { get; }
        IBaseRepository<Book> Books { get; }

        int Complete();
    }
}