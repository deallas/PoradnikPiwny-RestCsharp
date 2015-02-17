using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restcsharp.entity
{
    public class City
    {
        public int? Id
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public City(int? id, String name)
        {
            Id = id;
            Name = name;
        }
    }
}
