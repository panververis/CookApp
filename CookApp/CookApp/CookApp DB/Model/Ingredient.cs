using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.CookApp_DB.Model
{
    public class Ingredient
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }

        public Ingredient()
        {
            Description = "Νέο Υλικό";
            Available = false;
        }

    }
}
