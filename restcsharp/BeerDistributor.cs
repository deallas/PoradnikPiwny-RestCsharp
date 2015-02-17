using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restcsharp.entity
{
    public class BeerDistributor
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

        public BeerDistributor(int? id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
