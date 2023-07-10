using Npgsql;
using System.Data;

namespace AddressBook.Infrastructure.Helpers
{
    public class DbHelper
    {
        public static IDbConnection GetConn(string conString)
        {
            return new NpgsqlConnection(conString);
        }

    }
}
