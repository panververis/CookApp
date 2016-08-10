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
    class RecipeEditPage : ContentPage
    {

        #region Locally defined variables

        protected Recipe _recipe;

        protected Entry _NameEntry;

        protected Entry _DescriptionEntry;

        protected int? _RecipeID;

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

        #endregion

        #region Constructor 

        public RecipeEditPage(int? RecipeID = null)
        {
            _RecipeID = RecipeID;
            if (!_RecipeID.HasValue)
            {
                _recipe = new Recipe();
            }
            else
            {
                _recipe = DataBase.GetRecipeByID(RecipeID.Value);
            }
            StackLayout recipeEditStackLayout = new StackLayout();
            _NameEntry = new Entry();
            _NameEntry.FontAttributes = FontAttributes.Bold;
            _NameEntry.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Entry));
            _NameEntry.SetBinding(Entry.TextProperty, "Name");
            _DescriptionEntry = new Entry();
            _DescriptionEntry.FontAttributes = FontAttributes.Bold;
            _DescriptionEntry.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry));
            _DescriptionEntry.SetBinding(Entry.TextProperty, "Description");
            Button saveRecipeButton = new Button();
            saveRecipeButton.Clicked += SaveRecipeButton_Clicked;
            saveRecipeButton.Text = StringResources.sStoreRecipe;
            recipeEditStackLayout.Children.Add(_NameEntry);
            recipeEditStackLayout.Children.Add(_DescriptionEntry);
            recipeEditStackLayout.Children.Add(saveRecipeButton);
            Content = recipeEditStackLayout;
            BindingContext = _recipe;
        }

        #endregion

        #region Page methods

        private void SaveRecipeButton_Clicked(object sender, EventArgs e)
        {
            DataBase.SaveRecipe(_recipe);
            Navigation.PopAsync(true);
        }

        #endregion

    }
}
