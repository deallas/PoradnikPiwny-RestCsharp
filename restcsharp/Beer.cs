using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace restcsharp.entity
{
    public class Beer
    {
        public enum MaltEnum
        {
            NULL,
            JECZMIENNY,
            PSZENNY,
            INNY
        }

        public enum TypeEnum
        {
            NULL,
            BEZALKOHOLOWE,
            LEKKIE,
            PELNE,
            MOCNE
        }
    
        public enum FilteredEnum
        {
            NULL,
            NIEWIEM,
            NIE,
            TAK
        }

        public enum PasteurizedEnum
        {
            NULL,
            NIEWIEM,
            NIE,
            TAK
        }
    
        public enum FlavoredEnum
        {
            NULL,
            NIEWIEM,
            NIE,
            TAK
        }

        public enum PlaceofbrewEnum
        {
            NULL,
            BROWAR,
            DOM,
            RESTAURACJA
        }

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        } 

        public float? Alcohol
        {
            get;
            set;
        }

        public float? Extract
        {
            get;
            set;
        }

        public PlaceofbrewEnum Placeofbrew
        {
            get;
            set;
        }

        public MaltEnum Malt
        {
            get;
            set;
        }

        public TypeEnum Type
        {
            get;
            set;
        }

        public FilteredEnum Filtered
        {
            get;
            set;
        }

        public PasteurizedEnum Pasteurized
        {
            get;
            set;
        }

        public FlavoredEnum Flavored
        {
            get;
            set;
        }

        public float? RankingAvg
        {
            get;
            set;
        }

        public float? RankingWeightedAvg
        {
            get;
            set;
        }

        public DateTime DateAdded
        {
            get;
            set;
        }

        public BeerFamily Family
        {
            get;
            set;
        }

        public BeerDistributor Distributor
        {
            get;
            set;
        }

        public BeerManufacturer Manufacturer
        {
            get;
            set;
        }

        public BeerImage Image
        {
            get;
            set;
        }

        public Country Country
        {
            get;
            set;
        }

        public Region Region
        {
            get;
            set;
        }

        public City City
        {
            get;
            set;
        }

        public Beer(int id, string name, float? alcohol, float? extract, string placeofbrew,
                    string malt, string type, string filtered, string pasteurized,
                    string flavored, float? rankingAvg, float? rankingWeightedAvg, string dateAdded, 
                    BeerFamily family, BeerDistributor distributor, BeerManufacturer manufacturer,
                    BeerImage image, Country country, Region region, City city
               )
        {
            Id = id;
            Name = name;
            Alcohol = alcohol;
            Extract = extract;

            if (malt != null)
            {
                Malt = (MaltEnum)System.Enum.Parse(typeof(MaltEnum), malt);
            }
            else
            {
                Malt = MaltEnum.NULL;
            }

            if (type != null)
            {
                Type = (TypeEnum)System.Enum.Parse(typeof(TypeEnum), type);
            }
            else
            {
                Type = TypeEnum.NULL;
            }

            if (filtered != null)
            {
                Filtered = (FilteredEnum)System.Enum.Parse(typeof(FilteredEnum), filtered);
            }
            else
            {
                Filtered = FilteredEnum.NULL;
            }

            if (pasteurized != null)
            {
                Pasteurized = (PasteurizedEnum)System.Enum.Parse(typeof(PasteurizedEnum), pasteurized);
            }
            else
            {
                Pasteurized = PasteurizedEnum.NULL;
            }

            if (flavored != null)
            {
                Flavored = (FlavoredEnum)System.Enum.Parse(typeof(FlavoredEnum), flavored);
            }
            else
            {
                Flavored = FlavoredEnum.NULL;
            }

            if (placeofbrew != null)
            {
                Placeofbrew = (PlaceofbrewEnum)System.Enum.Parse(typeof(PlaceofbrewEnum), placeofbrew);
            }
            else
            {
                Placeofbrew = PlaceofbrewEnum.NULL;
            }

            RankingAvg = rankingAvg;
            RankingWeightedAvg = rankingWeightedAvg;
            DateAdded = new DateTime();
            DateAdded = DateTime.ParseExact(dateAdded, "dd.MM.yyyy HH:mm:ss", null);
            Family = family;
            Manufacturer = manufacturer;
            Distributor = distributor;
            Image = image;
            Country = country;
            Region = region;
            City = city;
        }
    }
}
