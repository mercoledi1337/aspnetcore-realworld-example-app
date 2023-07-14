
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System.Data;

namespace Conduit.Infrastructure
{
    //public class ConduitContext : ApiAuthorizationDbContext<Profile>
    //{
    //    private IDbContextTransaction? _currentTransaction;

    //    public ConduitContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
    //        : base(options, operationalStoreOptions)
    //    {            
    //    }


    //    #region Transaction Handling
    //    public void BeginTransaction()
    //    {
    //        if (_currentTransaction != null)
    //        {
    //            return;
    //        }

    //        if (!Database.IsInMemory())
    //        {
    //            _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
    //        }
    //    }

    //    public void CommitTransaction()
    //    {
    //        try
    //        {
    //            _currentTransaction?.Commit();
    //        }
    //        catch
    //        {
    //            RollbackTransaction();
    //            throw;
    //        }
    //        finally
    //        {
    //            if (_currentTransaction != null)
    //            {
    //                _currentTransaction.Dispose();
    //                _currentTransaction = null;
    //            }
    //        }
    //    }

    //    public void RollbackTransaction()
    //    {
    //        try
    //        {
    //            _currentTransaction?.Rollback();
    //        }
    //        finally
    //        {
    //            if (_currentTransaction != null)
    //            {
    //                _currentTransaction.Dispose();
    //                _currentTransaction = null;
    //            }
    //        }
    //    }
    //    #endregion
    //}
}