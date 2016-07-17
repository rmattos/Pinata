using System;
using System.Collections.Generic;
using System.Data;
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
                foreach (var sql in list)
                {
                    using (IDbConnection connection = GetConnection())
                    {
                        connection.Execute(sql.ToString(), null, null, 0, null);
                    }
                }

                return true;
            }
            catch (Exception ex)
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
