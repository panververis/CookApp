using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.CookApp_DB.Model
{
    public class Recipe : CookAppDataObject
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        private string _description { set; get; }

        public string Description
        {
            get
            { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    RaisePropertyChanged("Description");
                }
            }
        }

        private string _name { set; get; }

        public string Name
        {
            get
            { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        public Recipe()
        {
            Name = StringResources.sNewRecipe;
            Description = StringResources.sNewRecipeDescription;
        }

    }
}
