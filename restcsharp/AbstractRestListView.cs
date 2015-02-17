using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using restcsharp.exception;

using RestSharp;

namespace restcsharp.view.list
{
    public abstract class AbstractRestListView
    {
        protected ListView LV;

        protected RestClient Client;

        public class LVData
        {
            public int Id
            {
                get;
                set;
            }
            public String BeerName
            {
                get;
                set;
            }
            public String ManufacturerName
            {
                get;
                set;
            }
            public String DistributorName
            {
                get;
                set;
            }
            public String ImageUrl
            {
                get;
                set;
            }

            public string Ranking
            {
                get;
                set;
            }

            public LVData(int id, string beerName, string manufacturerName, string distributorName, string imageUrl, string ranking)
            {
                Id = id;
                BeerName = beerName;
                ManufacturerName = manufacturerName;
                DistributorName = distributorName;
                ImageUrl = imageUrl;
                Ranking = ranking;
            }
        }

        public AbstractRestListView(RestClient client, ListView lv)
        {
            Client = client;
            LV = lv;
        }

        public int getId()
        {
            if (LV.SelectedItems.Count == 0 || LV.SelectedItems[0] == null)
            {
                //throw new BeerNotFoundException("1");
                throw new BeerNotFoundException();
            }
            var selectedStockObject = LV.SelectedItems[0] as LVData;
            if (selectedStockObject == null)
            {
                //throw new BeerNotFoundException("2");
                throw new BeerNotFoundException();
            }

            return selectedStockObject.Id;
        }

        public abstract void fillData();
    }
}
