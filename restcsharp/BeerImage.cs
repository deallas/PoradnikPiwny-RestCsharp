using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restcsharp.entity
{
    public class BeerImage
    {
        public int? Id
        {
            get;
            set;
        }

        public String Title
        {
            get;
            set;
        }

        public String Path
        {
            get;
            set;
        }

        public DateTime DateAdded
        {
            get;
            set;
        }

        public BeerImage(int? id, String title, String path, String dateAdded)
        {
            Id = id;
            Title = title;
            Path = path;
            DateAdded = new DateTime();
            DateAdded = DateTime.ParseExact(dateAdded, "dd.MM.yyyy HH:mm:ss", null);
        }
    }
}
