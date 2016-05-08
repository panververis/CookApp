using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookWhat.Pages
{

    public class IngredientsPage : ContentPage
    {
        public IngredientsPage()
        {
            Content = new StackLayout()
            {
                Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
                Children = {
                    new Label() {
                        Text = "Όλα τα υλικά μου φαίνονται εδώ! :D "
                    }
                }
            };
        }
    }

}
