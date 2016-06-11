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
    public class IngredientEditPage : ContentPage
    {

        protected Entry _DescriptionEntry;

        protected int? _IngredientID;

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

        public IngredientEditPage(int? IngredientID = null)
        {
            _IngredientID = IngredientID;
            StackLayout ingredientEditStackLayout = new StackLayout();
            _DescriptionEntry = new Entry();
            Switch availableSwitch = new Switch();
            Button saveIngredientButton = new Button();
            saveIngredientButton.Clicked += SaveIngredientButton_Clicked;
            saveIngredientButton.Text = "Αποθήκευση νέου υλικού";
            ingredientEditStackLayout.Children.Add(_DescriptionEntry);
            ingredientEditStackLayout.Children.Add(availableSwitch);
            ingredientEditStackLayout.Children.Add(saveIngredientButton);
            Content = ingredientEditStackLayout;
        }

        private void SaveIngredientButton_Clicked(object sender, EventArgs e)
        {
            string descriptionText = _DescriptionEntry.Text;
            Ingredient newIngredient = new Ingredient();
            newIngredient.Description = descriptionText;
            DataBase.SaveItem(newIngredient);
        }
    }
}
