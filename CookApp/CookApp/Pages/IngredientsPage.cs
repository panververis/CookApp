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

        public IngredientsPage()
        {

            //firstly loading all of the Ingredients in the Observable Collection
            LoadIngredients();

            #region Ingredients ListView UI implementation            

            //initializing the Ingredients ListView DataTemplate (ListView "format")
            DataTemplate ingredientDataTemplate = new DataTemplate(() =>
            {
                Grid grid = new Grid();
                grid.IsEnabled = true;

                //initializing the Description and the "Available" switch
                Label descriptionLabel = new Label { FontAttributes = FontAttributes.Bold };
                Switch availableSwitch = new Switch();
                availableSwitch.HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, false);
                Button editButton = new Button();
                editButton.IsEnabled = true;
                editButton.HorizontalOptions = new LayoutOptions(LayoutAlignment.End,false);
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
            StackLayout ingredientsStackLayout = new StackLayout();

            //Initializing the "Add Ingredient" Button
            Button addIngredientButton = new Button();
            addIngredientButton.Text = StringResources.sAddNewIngredient;
            addIngredientButton.Clicked += AddIngredientButton_Clicked;
            ingredientsStackLayout.Children.Add(addIngredientButton);

            //instantiating the Ingredients ListView
            //also providing the prepared DataTemplate
            _ingredientsListView.ItemTemplate = ingredientDataTemplate;
            _ingredientsListView.ItemsSource = _ingredientsCollection;
            ingredientsStackLayout.Children.Add(_ingredientsListView);

            #endregion

            //lastly, setting the Content of the Ingredients Content Page to the prepared Stack Layout
            Content = ingredientsStackLayout;
        }

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

        #endregion

        #region Events handling

        /// <summary>
        /// Clicked Event of the "Add Ingredient" button
        /// </summary>
        private void AddIngredientButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new IngredientEditPage());
        }

        /// <summary>
        /// Clicked Event of the "Edit Ingredient" button
        /// </summary>
        private void editIngredientButton_Clicked(object sender, EventArgs e)
        {
            //if (_ingredientsListView.SelectedItem != null) {
            //    int indexOfIngredientForEdit = 0;
            //    indexOfIngredientForEdit = _ingredientsCollection.IndexOf(_ingredientsListView.SelectedItem as Ingredient);
            //    Ingredient clickedIngredient = _ingredientsCollection[indexOfIngredientForEdit];
            //} else
            //{
            int ingredientID = Convert.ToInt32((sender as Button).CommandParameter);
            //}
            
            Navigation.PushAsync(new IngredientEditPage(ingredientID));
        }

        #endregion

    }

}
