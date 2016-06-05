using System;
using System.Data;
using System.Data.Common;
using DapperExtensions;

namespace Piñata.Data
{
    public class BaseMySQLRepository<T> : IBaseMySQLRepository<T>, IDisposable where T : class
    {
        #region [ PRIVATE ]

        private object _lockObject = new object();

        private string ConnectionString { get; set; }

        private string ProviderName { get; set; }

        private IDbConnection CreateDbConnection(string connectionString, string providerName)
        {
            Check.Argument.IsNotEmpty(connectionString, "connectionString");
            Check.Argument.IsNotEmpty(providerName, "providerName");

            DbConnection connection = null;

            try
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
                connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;
            }
            catch (Exception ex)
            {
                throw new Exception("Occurred an error while creating the DbProviderFactory for: {0}, Error details: {1}".FormatWith(providerName, ex.ToString()));
            }

            return connection;
        }

        private void OpenConnection(IDbConnection connection)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        #endregion

        protected IDbTransaction Transaction { get; private set; }

        protected IDbConnection Connection { get; private set; }

        public BaseMySQLRepository(string connectionString, string providerName)
        {
            Check.Argument.IsNotEmpty(connectionString, "connectionString");
            Check.Argument.IsNotEmpty(providerName, "providerName");

            ConnectionString = connectionString;
            ProviderName = providerName;

            Connection = CreateDbConnection(connectionString, providerName);

            OpenConnection(Connection);
        }

        public BaseMySQLRepository(IDbConnection connection)
        {
            Check.Argument.IsNotNull(connection, "connection");

            Connection = connection;

            OpenConnection(Connection);
        }

        public IDbConnection GetConnection()
        {
            lock (_lockObject)
            {
                var connection = CreateDbConnection(ConnectionString, ProviderName);

                OpenConnection(connection);

                return connection;
            }
        }

        public void Insert(T entity)
        {
            Connection.Insert(entity);
        }

        public void Delete(T entity)
        {
            Connection.Delete(entity);
        }

        public void Update(T entity)
        {
            Connection.Update(entity);
        }

        public void BeginTransaction()
        {
            Transaction = Connection.BeginTransaction();
        }

        public void BeginTransaction(IDbConnection connection)
        {
            Transaction = connection.BeginTransaction();
        }

        public void Commit()
        {
            Transaction.Commit();
        }

        public void Rollback()
        {
            Transaction.Rollback();
        }

        public void Close(IDbConnection connection)
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
        }
    }
}
