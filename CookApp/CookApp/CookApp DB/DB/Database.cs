using CookApp.CookApp_DB.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookApp.CookApp_DB.DB
{
    public class CookAppDatabase
    {

        #region Class Properties

        //the Locker object
        static object locker = new object();

        //the SQliteConnection Database
        SQLiteConnection database;

        #endregion

        #region Default "YesItem" DataBase Constructor

        /// <summary>
        /// YesItemDatabase default Constructor, ensuring that the DBTable will be created on the class' initialization
        /// </summary>
        public CookAppDatabase()
        {
            database = DependencyService.Get<ISQlite>().GetConnection();
            database.CreateTable<Ingredient>();
        }

        #endregion

        #region Database Operations

        /// <summary>
        /// Get All Ingredients Operation
        /// </summary>
        public IEnumerable<Ingredient> GetAllIngredients()
        {
            lock (locker)
            {
                return (from i in database.Table<Ingredient>() select i).ToList();
            }
        }

        /// <summary>
        /// Check if Ingredients exist
        /// </summary>
        public bool CheckIfAnyIngredientsExist()
        {
            lock (locker)
            {
                return database.Table<Ingredient>().Any();
            }
        }

        /// <summary>
        /// Get an Ingredient Operation
        /// </summary>
        public Ingredient GetAnIngredient()
        {
            lock (locker)
            {
                return database.Table<Ingredient>().FirstOrDefault();
            }
        }

        /// <summary>
        /// Get All Available Ingredients Operation
        /// </summary>
        public IEnumerable<Ingredient> GetAvailableIngredients()
        {
            lock (locker)
            {
                return database.Query<Ingredient>("SELECT * FROM [Ingredient] WHERE [Available] = 1");
            }
        }

        /// <summary>
        /// Get Ingredient ID operation
        /// </summary>
        public Ingredient GetIngredient(int ID)
        {
            lock (locker)
            {
                return database.Table<Ingredient>().FirstOrDefault(x => x.ID == ID);
            }
        }

        /// <summary>
        /// Update Ingredient operation
        /// </summary>
        public int SaveItem(Ingredient ingredient)
        {
            lock (locker)
            {
                if (ingredient.ID != 0)
                {
                    database.Update(ingredient);
                    return ingredient.ID;
                }
                else
                {
                    return database.Insert(ingredient);
                }
            }
        }

        /// <summary>
        /// Delete Ingredient by ID operation
        /// </summary>
        public int DeleteIngredient(int ID)
        {
            lock (locker)
            {
                return database.Delete<Ingredient>(ID);
            }
        }

        #endregion

    }
}
