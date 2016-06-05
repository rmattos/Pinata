using System.Data;

namespace Piñata.Data
{
    public interface IBaseMySQLRepository<T> where T : class
    {
        void Insert(T entity);

        void Delete(T entity);

        void Update(T entity);

        void BeginTransaction();

        void BeginTransaction(IDbConnection connection);

        void Commit();

        void Rollback();

        IDbConnection GetConnection();

        void Close(IDbConnection connection);
    }
}