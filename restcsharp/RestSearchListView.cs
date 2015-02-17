using Newtonsoft.Json;
using restcsharp.entity;
using restcsharp.exception;
using restcsharp.rest;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace restcsharp.view.list
{
    class RestSearchListView : AbstractRestListView
    {
        public string query 
        { 
            get; 
            set; 
        }

        public RestSearchListView(RestClient client, ListView lv):base(client, lv)
        {
            query = "";
        }

        public override void fillData()
        {
            LV.Items.Clear();

            var request = new RestRequest("beer/search", Method.POST);
            request.AddParameter("query",query);

            IRestResponse response = Client.Execute(request);
            var content = response.Content;

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;

            SearchHandler sl;
            try
            {
                sl = JsonConvert.DeserializeObject<SearchHandler>(content, settings);
            }
            catch (Exception)
            {
                throw new JsonParseException();
            }

            if (sl == null)
            {
                return;
            }

            //System.Diagnostics.Debug.WriteLine(sl.result_id);
            if (sl.result_id != null)
            {
                var GetRequest = new RestRequest("beer/search/search_id/"+sl.result_id, Method.GET);
                IRestResponse GetResponse = Client.Execute(GetRequest);
                var GetContent = GetResponse.Content;

                BeerListHandler bl;
                try
                {
                    bl = JsonConvert.DeserializeObject<BeerListHandler>(GetContent, settings);
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
                        ranking = (float)b.RankingAvg;
                    }

                    LV.Items.Add(new LVData(b.Id, b.Name, manName, disName, imageUrl,ranking.ToString("N3")));
                }
            }
        }
    }
}
