using System.Data;

namespace Pinata.Data
{
    public interface IBaseMySQLRepository<T> where T : class
    {
        void Insert(T entity);

        void Delete(T entity);

        void Update(T entity);

        void BeginTransaction();

        IDbTransaction BeginTransaction(IDbConnection connection);

        void Commit();

        void Commit(IDbTransaction transaction);

        void Rollback();

        void Rollback(IDbTransaction transaction);

        IDbConnection GetConnection();

        void Close(IDbConnection connection);
    }
}