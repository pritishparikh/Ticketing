using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.MySqlDBContext
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
