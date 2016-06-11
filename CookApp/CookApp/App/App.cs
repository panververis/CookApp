using CookApp.CookApp_DB.DB;
using CookApp.CookApp_DB.Model;
using CookWhat.Main_Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CookApp
{
    public class App : Application
    {

        //Application's DataBase
        public static CookAppDatabase database;

        //CookApp Database Getter
        public static CookAppDatabase DataBase
        {
            get
            {
                if (database == null)
                {
                    database = new CookAppDatabase();
                }
                return database;
            }
        }

        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new HomePage());
        }

        protected override void OnStart()
        {
            //Ingredient anyIngredient = DataBase
            if (!DataBase.CheckIfAnyIngredientsExist())
            {
                InsertBasicIngredients();
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        /// <summary>
        /// Helper method inserting two basic Ingredients
        /// </summary>
        private void InsertBasicIngredients()
        {
            Ingredient potatoes = new Ingredient();
            potatoes.Description = "Πατάτες";
            potatoes.Available = true;

            Ingredient tomatoes = new Ingredient();
            tomatoes.Description = "Ντομάτες";
            tomatoes.Available = true;

            DataBase.SaveItem(potatoes);
            DataBase.SaveItem(tomatoes);

        }

    }
}
