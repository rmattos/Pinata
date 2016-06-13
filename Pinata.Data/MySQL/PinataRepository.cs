using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Pinata.Data.MySQL
{
    public class PinataRepository : BaseMySQLRepository<Common.SampleData>, IPinataRepository
    {
        public PinataRepository(string connectionString, string providerName)
            : base(connectionString, providerName)
        {
        }

        public PinataRepository(IDbConnection connection)
            : base(connection)
        {
        }

        public bool InsertData(IList<string> sqlList)
        {
            try
            {
                Parallel.ForEach(sqlList, sql =>
                {
                    using (IDbConnection connection = GetConnection())
                    {
                        connection.Execute(sql, null, null, 0, null);
                    }
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
