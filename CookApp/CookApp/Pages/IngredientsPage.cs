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

    public class IngredientsPage : ContentPage
    {

        #region Locally (page's) defined variables

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

        //the Ingredients Observable Collection
        public ObservableCollection<Ingredient> _ingredientsCollection;

        //the ListView displayed
        ListView _ingredientsListView = new ListView();

        //the Page's Stack Layout
        StackLayout _ingredientsStackLayout = new StackLayout();

        //the Delete Ingredient Button
        Button _deleteIngredientButton = new Button();

        //locallly defined variable denoting whether the delete Ingredient button is pressed
        bool _deleteMode = false;

        #endregion

        #region Page Constructor

        /// <summary>
        /// The Ingredients Page builder
        /// </summary>
        public IngredientsPage()
        {
            
        }

        #endregion

        #region Page Overrides

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadIngredients();
            LoadUI();
        }

        #endregion

        #region Page Methods

        #region LoadIngredients

        private void LoadIngredients()
        {
            //fetching all the Ingredients for displaying
            List<Ingredient> allIngredientsList = DataBase.GetAllIngredients().OrderBy(x => x.Description).ToList();
            _ingredientsCollection = new ObservableCollection<Ingredient>();

            //populating the List of Ingredients
            if (allIngredientsList != null && allIngredientsList.Any())
            {
                foreach (Ingredient ingredient in allIngredientsList)
                {
                    _ingredientsCollection.Add(ingredient);
                }
            }
        }

        #endregion

        #region LoadUI method

        private void LoadUI()
        {  

            //initializing the Ingredients ListView DataTemplate (ListView "format")
            DataTemplate ingredientDataTemplate = new DataTemplate(() =>
            {
                Grid grid = new Grid();
                grid.IsEnabled = false;

                //initializing the Description and the "Available" switch
                Label descriptionLabel = new Label { FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
                Switch availableSwitch = new Switch();
                availableSwitch.HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, false);
                Button editButton = new Button();
                editButton.IsEnabled = true;
                editButton.Image = "edit2.png";
                editButton.WidthRequest = 50;
                editButton.HeightRequest = 50;
                editButton.HorizontalOptions = new LayoutOptions(LayoutAlignment.End, false);
                editButton.Clicked += editIngredientButton_Clicked;

                //seting the Bindings to the above created controls
                descriptionLabel.SetBinding(Label.TextProperty, "Description");
                availableSwitch.SetBinding(Switch.IsToggledProperty, "Available");
                editButton.SetBinding(Button.CommandParameterProperty, "ID");
                availableSwitch.IsEnabled = false;

                //adding the controls to the ListView
                grid.Children.Add(descriptionLabel);
                grid.Children.Add(availableSwitch);
                grid.Children.Add(editButton);

                return new ViewCell { View = grid };
            });

            //Initializing the basic Layout (Stack Layout)
            _ingredientsStackLayout = new StackLayout();

            //Initializing the "Add Ingredient" Button
            Button addIngredientButton = new Button();
            addIngredientButton.Text = StringResources.sAddNewIngredient;
            addIngredientButton.Clicked += AddIngredientButton_Clicked;
            _ingredientsStackLayout.Children.Add(addIngredientButton);

            //instantiating the Ingredients ListView
            //also providing the prepared DataTemplate
            _ingredientsListView.ItemTemplate = ingredientDataTemplate;
            _ingredientsListView.ItemsSource = _ingredientsCollection;
            _ingredientsListView.ItemSelected += _ingredientsListView_ItemSelected;
            _ingredientsStackLayout.Children.Add(_ingredientsListView);

            //Initializing the "Delete Ingredient" Button
            _deleteIngredientButton = new Button();
            _deleteIngredientButton.Image = "delete.png";
            _deleteIngredientButton.WidthRequest = 60;
            _deleteIngredientButton.HeightRequest = 60;
            _deleteIngredientButton.HorizontalOptions = new LayoutOptions(LayoutAlignment.End, false);
            _deleteIngredientButton.Clicked += DeleteIngredientButton_Clicked;
            _ingredientsStackLayout.Children.Add(_deleteIngredientButton);

            //lastly, setting the Content of the Ingredients Content Page to the prepared Stack Layout
            Content = _ingredientsStackLayout;
        }

        #endregion

        #region Delete Ingredient method

        private void DeleteSelectedIngredient(Ingredient selectedIngredient)
        {
            if (selectedIngredient == null)
            {
                return;
            }
            DataBase.DeleteIngredient(selectedIngredient.ID);
            _ingredientsCollection.Remove(selectedIngredient);
        }

        #endregion

        #endregion

        #region Events handling

        /// <summary>
        /// Clicked Event of the "Add Ingredient" button
        /// </summary>
        private void AddIngredientButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new IngredientEditPage(), true);
        }

        /// <summary>
        /// Clicked Event of the "Edit Ingredient" button
        /// </summary>
        private void editIngredientButton_Clicked(object sender, EventArgs e)
        {
            int ingredientID = Convert.ToInt32((sender as Button).CommandParameter);
            Navigation.PushAsync(new IngredientEditPage(ingredientID), true);
        }

        /// <summary>
        /// Clicked Event of the "Delete Ingredient" button
        /// </summary>
        private void DeleteIngredientButton_Clicked(object sender, EventArgs e)
        {
            //if the user has already clicked Delete Modes
            if (_deleteMode)
            {
                _deleteMode = false;
                _deleteIngredientButton.BackgroundColor = Color.Default;
            }
            else
            {
                _deleteMode = true;
                _deleteIngredientButton.BackgroundColor = Color.Red;
            }
        }

        /// <summary>
        /// Item selected Event of the Ingredients ListView
        /// </summary>
        private void _ingredientsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (_deleteMode)
            {
                if (e.SelectedItem == null)
                {
                    return;
                }
                Ingredient selectedIngredient = (e.SelectedItem as Ingredient);
                if (selectedIngredient == null)
                {
                    return;
                }
                DeleteSelectedIngredient(selectedIngredient);
            } else
            {
                //do nothing
            }
        }

        #endregion

    }

}
