using CookWhat.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookWhat.Main_Page
{
    public class HomePage : ContentPage
    {
        public HomePage()
        {
            Content = new StackLayout
            {
                Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
                Children = {
                    new Button() {
                        Text = "Τα υλικά μου",
                        Command = new Command (() => Navigation.PushAsync (new IngredientsPage ()))
                    },
                    new Button() {
                        Text = "Οι συνταγές μου",
                        Command = new Command (() => Navigation.PushAsync (new RecipesPage ()))
                    }
                }
            };
        }
    }
}
