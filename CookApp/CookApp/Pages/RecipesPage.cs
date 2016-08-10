using CookApp.CookApp_DB.DB;
using CookApp.CookApp_DB.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookApp.Pages
{
    public class RecipesPage : ContentPage
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

        //the Recipes Observable Collection
        public ObservableCollection<Recipe> _recipesCollection;

        //the ListView displayed
        ListView _recipesListView = new ListView();

        //the Page's Stack Layout
        StackLayout _recipesStackLayout = new StackLayout();

        //the Delete IRecipe Button
        Button _deleteRecipeButton = new Button();

        //locallly defined variable denoting whether the delete Recipe button is pressed
        bool _deleteMode = false;

        #region Recipes Page Constructor

        public RecipesPage()
        {
            
        }

        #endregion

        #region Page Overrides

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadRecipes();
            LoadUI();
        }

        #endregion

        #region Page Methods

        #region LoadUI

        private void LoadUI()
        {
            //initializing the Recipes ListView DataTemplate (ListView "format")
            DataTemplate recipeDataTemplate = new DataTemplate(() =>
            {
                Grid grid = new Grid();
                grid.IsEnabled = false;

                //initializing the Name control
                Label nameLabel = new Label { FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
                Button editButton = new Button();
                editButton.IsEnabled = true;
                editButton.Image = "edit2.png";
                editButton.WidthRequest = 50;
                editButton.HeightRequest = 50;
                editButton.HorizontalOptions = new LayoutOptions(LayoutAlignment.End, false);
                editButton.Clicked += editRecipeButton_Clicked;

                //seting the Bindings to the above created controls
                nameLabel.SetBinding(Label.TextProperty, "Name");
                editButton.SetBinding(Button.CommandParameterProperty, "ID");

                //adding the controls to the ListView
                grid.Children.Add(nameLabel);
                grid.Children.Add(editButton);

                return new ViewCell { View = grid };
            });

            //Initializing the basic Layout (Stack Layout)
            _recipesStackLayout = new StackLayout();

            //Initializing the "Add Recipe" Button
            Button addRecipeButton = new Button();
            addRecipeButton.Text = StringResources.sAddNewRecipe;
            addRecipeButton.Clicked += AddRecipeButton_Clicked;
            _recipesStackLayout.Children.Add(addRecipeButton);

            //instantiating the Recipes ListView
            //also providing the prepared DataTemplate
            _recipesListView.ItemTemplate = recipeDataTemplate;
            _recipesListView.ItemsSource = _recipesCollection;
            _recipesListView.ItemSelected += _recipesListView_ItemSelected;
            _recipesStackLayout.Children.Add(_recipesListView);

            //Initializing the "Delete Recipe" Button
            _deleteRecipeButton = new Button();
            _deleteRecipeButton.Image = "delete.png";
            _deleteRecipeButton.WidthRequest = 60;
            _deleteRecipeButton.HeightRequest = 60;
            _deleteRecipeButton.HorizontalOptions = new LayoutOptions(LayoutAlignment.End, false);
            _deleteRecipeButton.Clicked += DeleteRecipeButton_Clicked;
            _recipesStackLayout.Children.Add(_deleteRecipeButton);

            //lastly, setting the Content of the Ingredients Content Page to the prepared Stack Layout
            Content = _recipesStackLayout;
        }

        #endregion

        #region Load Recipes

        private void LoadRecipes()
        {
            //fetching all the Recipes for displaying
            List<Recipe> allRecipesList = DataBase.GetAllRecipes().OrderBy(x => x.Description).ToList();
            _recipesCollection = new ObservableCollection<Recipe>();

            //populating the List of Recipes
            if (allRecipesList != null && allRecipesList.Any())
            {
                foreach (Recipe recipe in allRecipesList)
                {
                    _recipesCollection.Add(recipe);
                }
            }
        }

        #endregion

        #region Delete Recipe method

        private void DeleteSelectedRecipe(Recipe selectedRecipe)
        {
            if (selectedRecipe == null)
            {
                return;
            }
            DataBase.DeleteRecipe(selectedRecipe.ID);
            _recipesCollection.Remove(selectedRecipe);
        }

        #endregion

        #endregion

        #region Event handlers

        /// <summary>
        /// Edit Recipe Button Click Event
        /// </summary>
        private void editRecipeButton_Clicked(object sender, EventArgs e)
        {
            int recipeID = Convert.ToInt32((sender as Button).CommandParameter);
            Navigation.PushAsync(new RecipeEditPage(recipeID), true);
        }

        /// <summary>
        /// Edit Recipe Button Click Event
        /// </summary>
        private void AddRecipeButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecipeEditPage(), true);
        }

        /// <summary>
        /// The Item Selected Event of the Recipes ListView
        /// </summary>
        private void _recipesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (_deleteMode)
            {
                if (e.SelectedItem == null)
                {
                    return;
                }
                Recipe selectedRecipe = (e.SelectedItem as Recipe);
                if (selectedRecipe == null)
                {
                    return;
                }
                DeleteSelectedRecipe(selectedRecipe);
            }
            else
            {
                //do nothing
            }
        }

        private void DeleteRecipeButton_Clicked(object sender, EventArgs e)
        {
            //if the user has already clicked Delete Modes
            if (_deleteMode)
            {
                _deleteMode = false;
                _deleteRecipeButton.BackgroundColor = Color.Default;
            }
            else
            {
                _recipesListView.SelectedItem = null;
                _deleteMode = true;
                _deleteRecipeButton.BackgroundColor = Color.Red;
            }
        }

        #endregion

    }
}
