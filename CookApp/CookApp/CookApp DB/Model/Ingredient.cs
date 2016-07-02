using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.CookApp_DB.Model
{
    public class Ingredient : CookAppDataObject
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private string _description { set; get; }

        public string Description {
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

        private bool _available { set; get; }

        public bool Available {
            get
            { return _available; }
            set
            {
                if (_available != value)
                {
                    _available = value;
                    RaisePropertyChanged("Available");
                }
            }
        }

        public Ingredient()
        {
            Description = StringResources.sNewIngredient;
            Available = false;
        }

    }
}
