using System;

namespace Easyrewardz_TicketSystem.Services
{
    public  class CommonServices  : IDisposable
    {
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (db != null)
                //{
                //    db.Dispose();
                //    db = null;
                //}
            }
        }

    }
    
    
}
