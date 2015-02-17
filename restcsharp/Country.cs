using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restcsharp.entity
{
    public class Country
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

        public Country(int? id, String name)
        {
            Id = id;
            Name = name;
        }
    }
}
