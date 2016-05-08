using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using CookApp.Droid.DB;
using CookApp.CookApp_DB;
using System.IO;

[assembly: Dependency(typeof(SQLite_Android))]
namespace CookApp.Droid.DB
{
    public class SQLite_Android : ISQlite
    {
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "YesSQLite.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            var conn = new SQLite.SQLiteConnection(path);

            // Return the database connection 
            return conn;
        }
    }
}