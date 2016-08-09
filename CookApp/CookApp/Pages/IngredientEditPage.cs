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

        protected Ingredient _ingredient;

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
            if (!_IngredientID.HasValue)
            {
                _ingredient = new Ingredient();
            }
            else
            {
                _ingredient = DataBase.GetIngredientByID(IngredientID.Value);
            }
            StackLayout ingredientEditStackLayout = new StackLayout();
            _DescriptionEntry = new Entry();
            _DescriptionEntry.FontAttributes = FontAttributes.Bold;
            _DescriptionEntry.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry));
            _DescriptionEntry.SetBinding(Entry.TextProperty, "Description");
            Label availableLabel = new Label { FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) };
            availableLabel.Text = StringResources.sAvailable;
            Switch availableSwitch = new Switch();
            availableSwitch.SetBinding(Switch.IsToggledProperty, "Available");
            availableSwitch.HorizontalOptions = new LayoutOptions(LayoutAlignment.Start, false);
            Button saveIngredientButton = new Button();
            saveIngredientButton.Clicked += SaveIngredientButton_Clicked;
            saveIngredientButton.Text = StringResources.sStoreIngredient;
            ingredientEditStackLayout.Children.Add(_DescriptionEntry);
            ingredientEditStackLayout.Children.Add(availableLabel);
            ingredientEditStackLayout.Children.Add(availableSwitch);
            ingredientEditStackLayout.Children.Add(saveIngredientButton);
            Content = ingredientEditStackLayout;
            BindingContext = _ingredient;
        }

        private void SaveIngredientButton_Clicked(object sender, EventArgs e)
        {
            DataBase.SaveItem(_ingredient);
            Navigation.PopAsync(true);
        }

    }
}
