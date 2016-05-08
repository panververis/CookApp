using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.CookApp_DB
{
    public interface ISQlite
    {
        SQLiteConnection GetConnection();
    }
}
