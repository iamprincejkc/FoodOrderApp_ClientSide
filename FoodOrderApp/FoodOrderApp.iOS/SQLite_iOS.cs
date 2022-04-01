using FoodOrderApp.Model;
using Foundation;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(FoodOrderApp.iOS.SQLite_iOS))]
namespace FoodOrderApp.iOS
{
    class SQLite_iOS:ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFileName = "FoodOrderApp.db3";
            string docpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libpath = Path.Combine(docpath, "..", "Library");
            var path = Path.Combine(libpath, sqliteFileName);
            var cn = new SQLiteConnection(path);
            return cn;
        }
    }
}