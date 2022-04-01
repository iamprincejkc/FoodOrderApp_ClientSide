using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;

namespace FoodOrderApp.Model
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
