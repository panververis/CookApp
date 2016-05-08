using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookWhat.Pages
{
    public class RecipesPage : ContentPage
    {
        public RecipesPage()
        {
            Content = new StackLayout()
            {
                Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
                Children = {
                    new Label() {
                        Text = "Όλες οι συνταγές μου φαίνονται εδώ :D"
                    }
                }
            };
        }
    }
}
