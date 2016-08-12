using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookApp.Pages
{
    class RecipeSearchPage : ContentPage
    {


        #region Locally defined variables

        StackLayout _recipeSearchStackLayout = new StackLayout();
        Switch _useAvailableIngredientsSwitch = new Switch();

        #endregion

        #region Recipes Search Page Constructor

        public RecipeSearchPage()
        {
            _recipeSearchStackLayout = new StackLayout();
            Content = _recipeSearchStackLayout;
        }

        #endregion

        #region Page Overrides

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadUI();
        }

        #endregion

        #region Page Methods

        private void LoadUI()
        {



            //_NameEntry = new Entry();
            //_NameEntry.FontAttributes = FontAttributes.Bold;
            //_NameEntry.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Entry));
            //_NameEntry.SetBinding(Entry.TextProperty, "Name");
            //_DescriptionEditor = new Editor();
            //_DescriptionEditor.FontAttributes = FontAttributes.Bold;
            //_DescriptionEditor.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Editor));
            //_DescriptionEditor.SetBinding(Editor.TextProperty, "Description");
            //_DescriptionEditor.HeightRequest = 400;
            //Button saveRecipeButton = new Button();
            //saveRecipeButton.Clicked += SaveRecipeButton_Clicked;
            //saveRecipeButton.Text = StringResources.sStoreRecipe;
            //recipeEditStackLayout.Children.Add(_NameEntry);
            //recipeEditStackLayout.Children.Add(_DescriptionEditor);
            //recipeEditStackLayout.Children.Add(saveRecipeButton);
        }

        #endregion

    }
}
