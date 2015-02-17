using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restcsharp.entity
{
    public class BeerManufacturer
    {
        public int? Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public BeerManufacturer(int? id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
