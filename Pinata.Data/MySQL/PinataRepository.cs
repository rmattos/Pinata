using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Pinata.Common;

namespace Pinata.Data.MySQL
{
    public class PinataRepository : BaseMySQLRepository<BaseSampleData>, IPinataRepository
    {
        #region [ PRIVATE ]

        private bool ExecuteCommand(IList<object> list)
        {
            try
            {
                Parallel.ForEach(list, sql =>
                {
                    using (IDbConnection connection = GetConnection())
                    {
                        connection.Execute(sql.ToString(), null, null, 0, null);
                    }
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        public PinataRepository(string connectionString, string providerName)
            : base(connectionString, providerName)
        {
        }

        public PinataRepository(IDbConnection connection)
            : base(connection)
        {
        }

        public bool Insert(IList<object> list)
        {
            return ExecuteCommand(list);
        }

        public bool Update(IList<object> list)
        {
            return ExecuteCommand(list);
        }

        public bool Delete(IList<object> list)
        {
            return ExecuteCommand(list);
        }
    }
}
