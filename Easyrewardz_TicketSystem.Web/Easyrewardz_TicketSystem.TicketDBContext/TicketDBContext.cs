using MySql.Data.MySqlClient;
using System;

namespace Easyrewardz_TicketSystem.TicketDBContext
{
    public class TicketDBContext:IDisposable
    {
        public MySqlConnection Connection { get; }

        public TicketDBContext(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }
        public void Dispose() => Connection.Dispose();
    }
}
