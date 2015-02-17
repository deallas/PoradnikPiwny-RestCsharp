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

using restcsharp.rest;
using restcsharp.entity;
using restcsharp.exception;

using Newtonsoft.Json;
using RestSharp;

namespace restcsharp.view.list
{
    public class RestToprankingListView : AbstractRestListView
    {
        public RestToprankingListView(RestClient client, ListView lv):base(client, lv)
        {
        }

        public override void fillData()
        {
            LV.Items.Clear();
            var request = new RestRequest("beer/topranking", Method.GET);
            IRestResponse response = Client.Execute(request);
            var content = response.Content;

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            
            BeerListHandler bl;
            try
            {
                bl = JsonConvert.DeserializeObject<BeerListHandler>(content, settings);
            }
            catch (Exception)
            {
                throw new JsonParseException();
            }

            if (bl == null)
            {
                return;
            }

            string manName = "", disName = "", imageUrl = "";
            float ranking;
            foreach (Beer b in bl.beers)
            {
                if (b.Manufacturer == null)
                {
                    manName = "";
                }
                else
                {
                    manName = b.Manufacturer.Name;
                }

                if (b.Distributor == null)
                {
                    disName = "";
                }
                else
                {
                    disName = b.Distributor.Name;
                }

                if (b.Image == null)
                {
                    imageUrl = null;
                }
                else
                {
                    imageUrl = b.Image.Path;
                }

                if (b.RankingAvg == null)
                {
                    ranking = 0f;
                }
                else
                {
                    ranking = (float)b.RankingWeightedAvg;
                }

                LV.Items.Add(new LVData(b.Id, b.Name, manName, disName, imageUrl,ranking.ToString("N3")));
            }
        }
    }
}
