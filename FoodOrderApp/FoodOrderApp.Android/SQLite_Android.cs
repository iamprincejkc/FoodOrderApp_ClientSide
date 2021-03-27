using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FoodOrderApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly:Dependency(typeof(FoodOrderApp.Droid.SQLite_Android))]
namespace FoodOrderApp.Droid
{
    
    class SQLite_Android:ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFileName = "FoodOrderApp.db3";
            string docpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(docpath, sqliteFileName);
            var cn = new SQLiteConnection(path);
            return cn;
        }
    }
}