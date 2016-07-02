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

        public ObservableCollection<Ingredient> _ingredientsCollection;

        public IngredientsPage()
        {

            #region Old Implementation, using Labels

            //fetching all the Ingredients for displaying
            List<Ingredient> allIngredientsList = DataBase.GetAllIngredients().OrderBy(x => x.Description).ToList();

            //building the UI
            StackLayout ingredientsStackLayout = new StackLayout();
            Button addIngredientButton = new Button();
            addIngredientButton.Text = StringResources.sAddNewIngredient;
            ingredientsStackLayout.Children.Add(addIngredientButton);
            //foreach (Ingredient ingredient in allIngredientsList)
            //{
            //    ingredientsStackLayout.Children.Add(new Label() { Text = ingredient.Description, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) });
            //}

            addIngredientButton.Clicked += AddIngredientButton_Clicked;

            #endregion

            #region New implementation, using ListView

            _ingredientsCollection = new ObservableCollection<Ingredient>();
            if (allIngredientsList != null && allIngredientsList.Any())
            {
                foreach (Ingredient ingredient in allIngredientsList)
                {
                    _ingredientsCollection.Add(ingredient);
                }
            }


            DataTemplate personDataTemplate = new DataTemplate(() => {
                Grid grid = new Grid();
                grid.IsEnabled = false;
                Label descriptionLabel = new Label { FontAttributes = FontAttributes.Bold };
                Switch availableSwitch = new Switch();

                descriptionLabel.SetBinding(Label.TextProperty, "Description");
                availableSwitch.SetBinding(Switch.IsEnabledProperty, "Available");

                grid.Children.Add(descriptionLabel);
                grid.Children.Add(availableSwitch);

                return new ViewCell { View = grid };
            });


            ListView ingredientsListView = new ListView();
            ingredientsListView.ItemTemplate = personDataTemplate;
            ingredientsListView.ItemsSource = _ingredientsCollection;
            ingredientsStackLayout.Children.Add(ingredientsListView);

            ingredientsListView.ItemSelected += IngredientsListView_ItemSelected;

            #endregion

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

        private void IngredientsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
        }

        #endregion

    }

}
