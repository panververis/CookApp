using CookApp.CookApp_DB.DB;
using CookApp.CookApp_DB.Model;
using Java.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
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

        protected Image _recipeImage;

        protected Editor _DescriptionEditor;

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
            _recipeImage = new Image();
            string photoFileName = "CookAppImages" + "/" + _RecipeID + "_Master" + ".jpg";
            File imageFile = new File(photoFileName);
            if (imageFile.Exists())
            {
                _recipeImage.Source = ImageSource.FromFile(photoFileName);
            } else
            {
                _recipeImage.Source = ImageSource.FromFile("delete.png");
            }
            TapGestureRecognizer imageGestureRecognizer = new TapGestureRecognizer();
            imageGestureRecognizer.Tapped += async (sender, args) =>
            {

                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Name = photoFileName
                });

                if (file == null)
                    return;

                await DisplayAlert("File Location", file.Path, "OK");

                _recipeImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            };
            _recipeImage.GestureRecognizers.Add(imageGestureRecognizer);
            _DescriptionEditor = new Editor();
            _DescriptionEditor.FontAttributes = FontAttributes.Bold;
            _DescriptionEditor.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Editor));
            _DescriptionEditor.SetBinding(Editor.TextProperty, "Description");
            _DescriptionEditor.HeightRequest = 350;
            Button saveRecipeButton = new Button();
            saveRecipeButton.Clicked += SaveRecipeButton_Clicked;
            saveRecipeButton.Text = StringResources.sStoreRecipe;
            recipeEditStackLayout.Children.Add(_NameEntry);
            recipeEditStackLayout.Children.Add(_recipeImage);
            recipeEditStackLayout.Children.Add(_DescriptionEditor);
            recipeEditStackLayout.Children.Add(saveRecipeButton);
            Content = recipeEditStackLayout;
            BindingContext = _recipe;
        }

        #endregion

        #region Page Events

        private void SaveRecipeButton_Clicked(object sender, EventArgs e)
        {
            DataBase.SaveRecipe(_recipe);
            Navigation.PopAsync(true);
        }

        #endregion

    }
}
