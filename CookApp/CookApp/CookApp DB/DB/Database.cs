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

        #region Default "CookApp" DataBase Constructor

        /// <summary>
        /// CookAppDatabase default Constructor, ensuring that the DBTable will be created on the class' initialization
        /// </summary>
        public CookAppDatabase()
        {
            database = DependencyService.Get<ISQlite>().GetConnection();
            database.CreateTable<Ingredient>();
            database.CreateTable<Recipe>();
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
        /// Get All Recipes Operation
        /// </summary>
        public IEnumerable<Recipe> GetAllRecipes()
        {
            lock (locker)
            {
                return (from i in database.Table<Recipe>() select i).ToList();
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
        /// Check if Recipes exist
        /// </summary>
        public bool CheckIfAnyRecipiesExist()
        {
            lock (locker)
            {
                return database.Table<Recipe>().Any();
            }
        }

        /// <summary>
        /// Get the first Ingredient in the DB Operation
        /// </summary>
        public Ingredient GetAnIngredient()
        {
            lock (locker)
            {
                return database.Table<Ingredient>().FirstOrDefault();
            }
        }

        /// <summary>
        /// Get the first Recipe in the DB Operation
        /// </summary>
        public Recipe GetARecipe()
        {
            lock (locker)
            {
                return database.Table<Recipe>().FirstOrDefault();
            }
        }

        /// <summary>
        /// Get a specific Ingredient Operation
        /// </summary>
        public Ingredient GetIngredientByID(int ID)
        {
            lock (locker)
            {
                return database.Table<Ingredient>().FirstOrDefault(x => x.ID == ID);
            }
        }

        /// <summary>
        /// Get a specific Recipe Operation
        /// </summary>
        public Recipe GetRecipeByID(int ID)
        {
            lock (locker)
            {
                return database.Table<Recipe>().FirstOrDefault(x => x.ID == ID);
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

        /// <summary>
        /// Update Recipe operation
        /// </summary>
        public int SaveRecipe(Recipe recipe)
        {
            lock (locker)
            {
                if (recipe.ID != 0)
                {
                    database.Update(recipe);
                    return recipe.ID;
                }
                else
                {
                    return database.Insert(recipe);
                }
            }
        }

        /// <summary>
        /// Delete Recipe by ID operation
        /// </summary>
        public int DeleteRecipe(int ID)
        {
            lock (locker)
            {
                return database.Delete<Recipe>(ID);
            }
        }

        #endregion

    }
}
