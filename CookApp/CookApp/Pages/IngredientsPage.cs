using CookApp.CookApp_DB.DB;
using CookApp.CookApp_DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookApp.Pages
{

    public class IngredientsPage : ContentPage
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

        public IngredientsPage()
        {

            //fetching all the Ingredients for displaying
            List<Ingredient> allIngredientsList = DataBase.GetAllIngredients().OrderBy(x => x.Description).ToList();

            //building the UI
            StackLayout ingredientsStackLayout = new StackLayout();
            Button addIngredientButton = new Button();
            addIngredientButton.Text = "Προσθέστε ένα νέο Υλικό!";
            ingredientsStackLayout.Children.Add(addIngredientButton);
            foreach (Ingredient ingredient in allIngredientsList)
            {
                ingredientsStackLayout.Children.Add(new Label() { Text = ingredient.Description, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) });
            }

            addIngredientButton.Clicked += AddIngredientButton_Clicked;

            Content = ingredientsStackLayout;
        }


        #region Events handling

        /// <summary>
        /// Clicked Event of the "Add Ingredient" button
        /// </summary>
        private void AddIngredientButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new IngredientEditPage());
        }

        #endregion

    }

}
