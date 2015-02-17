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

using RestSharp;
using Newtonsoft.Json;

namespace restcsharp.view.grid
{
    class RestInfoGrid
    {
        const string BEER_NOT_FOUND_CODE = "beer_not_found";
        const string NULL_POINTER_CODE = "null_pointer";

        const string BEER_NOT_FOUND_MESSAGE = "Nie odnaleziono piwa w bazie";
        const string NULL_POINTER_MESSAGE = "Wystąpił nieoczekiwany błąd";

        private RestClient Client;
        private MainWindow WindowHandler;

        public RestInfoGrid(RestClient client, MainWindow windowHandler)
        {
            Client = client;
            WindowHandler = windowHandler;
        }

        protected string convertFloatToStars(float? fl, int max = 5)
        {
            float number;

            if (fl == null)
            {
                number = 0f;
            }
            else
            {
                number = (float)fl;
            }

            string s = "";

            for (int i = 0; i < (int)number; i++)
            {
                s += "\uf005"; // icon-star
            }

            int r = max - (int)number;

            if (number - Math.Floor(number) >= 0.5f)
            {
                s += "\uf123"; // icon-star-half-empty
                r--;
            }

            for (int i = 0; i < r; i++)
            {
                s += "\uf006"; // icon-star-empty
            }

            return s;

        }

        public void hide()
        {
            WindowHandler.dockPanel.Visibility = Visibility.Hidden;
        }

        public void fillData(int beerId)
        {
            WindowHandler.dockPanel.Visibility = Visibility.Visible;

            var request = new RestRequest("beer/info/beer_id/" + beerId, Method.GET);
            IRestResponse response = Client.Execute(request);
            var content = response.Content;

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;

            BeerHandler bh;
            try
            {
                bh = JsonConvert.DeserializeObject<BeerHandler>(content, settings);
            }
            catch (Exception)
            {
                throw new JsonParseException();
            }

            if (bh.msg != null && bh.msg.Length != 0)
            {
                string msg;
                switch (bh.msg)
                {
                    case BEER_NOT_FOUND_CODE:
                        msg = BEER_NOT_FOUND_MESSAGE;
                        break;
                    case NULL_POINTER_CODE:
                    default:
                        msg = NULL_POINTER_MESSAGE;
                        break;
                }
                throw new BeerNotFoundException(msg);
            }

            // ---->

            BeerTranslationHandler bth;

            var request2 = new RestRequest("beer/translation/beer_id/" + beerId, Method.GET);
            IRestResponse response2 = Client.Execute(request2);
            var content2 = response2.Content;

            try
            {
                bth = JsonConvert.DeserializeObject<BeerTranslationHandler>(content2, settings);
            }
            catch (Exception)
            {
                throw new JsonParseException();
            }

            if (bth.msg != null && bth.msg.Length != 0)
            {
                string msg;
                switch (bth.msg)
                {
                    case BEER_NOT_FOUND_CODE:
                        msg = BEER_NOT_FOUND_MESSAGE;
                        break;
                    case NULL_POINTER_CODE:
                    default:
                        msg = NULL_POINTER_MESSAGE;
                        break;
                }
                throw new BeerNotFoundException(msg);
            }

            // ---->

            if (bh.beer.Image.Path == null)
            {
                // wczytanie lokalnego zdjęcia zastępcznego
            }
            else
            {
                WindowHandler.iBeerImage.Source = new BitmapImage(new Uri(bh.beer.Image.Path));
            }

 
            WindowHandler.lBeerRanking.Content = convertFloatToStars(bh.beer.RankingAvg);
            WindowHandler.lBeerName.Content = bh.beer.Name;
            WindowHandler.lDistributorName.Content = bh.beer.Distributor.Name;
            WindowHandler.lManufacturerName.Content = bh.beer.Manufacturer.Name;
            WindowHandler.lBeerDescription.Text = bth.translation.Description;

            //---->

            if (bh.beer.Alcohol == null)
            {
                WindowHandler.lAlcoholRow.Visibility = Visibility.Collapsed;
            }
            else
            {
                WindowHandler.lAlcohol.Content = bh.beer.Alcohol + " %";
                WindowHandler.lAlcoholRow.Visibility = Visibility.Visible;
            }

            //---->

            if (bh.beer.Extract == null)
            {
                WindowHandler.lExtractRow.Visibility = Visibility.Collapsed;
            }
            else
            {
                WindowHandler.lExtract.Content = bh.beer.Extract + " %";
                WindowHandler.lExtractRow.Visibility = Visibility.Visible;
            }

            //---->

            if(bh.beer.Malt == Beer.MaltEnum.NULL)
            {
                WindowHandler.lMaltRow.Visibility = Visibility.Collapsed;
            } 
            else 
            {
                string malt = "";
                switch (bh.beer.Malt)
                {
                    case Beer.MaltEnum.PSZENNY:
                        malt = "Pszenny";
                        break;
                    case Beer.MaltEnum.JECZMIENNY:
                        malt = "Jęczmienny";
                        break;
                    case Beer.MaltEnum.INNY:
                        malt = "Inny";
                        break;
                }
                WindowHandler.lMalt.Content = malt;
                WindowHandler.lMaltRow.Visibility = Visibility.Visible;
            }

            //---->

            if (bh.beer.Filtered == Beer.FilteredEnum.NULL 
                || bh.beer.Filtered == Beer.FilteredEnum.NIEWIEM)
            {
                WindowHandler.lFilteredRow.Visibility = Visibility.Collapsed;
            }
            else
            {
                string filtered = "";
                switch (bh.beer.Filtered)
                {
                    case Beer.FilteredEnum.TAK:
                        filtered = "Tak";
                        break;
                    case Beer.FilteredEnum.NIE:
                        filtered = "Nie";
                        break;
                }
                WindowHandler.lFiltered.Content = filtered;
                WindowHandler.lFilteredRow.Visibility = Visibility.Visible;
            }

            //---->

            if(bh.beer.Pasteurized == Beer.PasteurizedEnum.NULL 
                || bh.beer.Pasteurized == Beer.PasteurizedEnum.NIEWIEM)
            {
                WindowHandler.lPasteurizedRow.Visibility = Visibility.Collapsed;
            }
            else 
            {
                string pasteurized = "";
                switch (bh.beer.Pasteurized)
                {
                    case Beer.PasteurizedEnum.TAK:
                        pasteurized = "Tak";
                        break;
                    case Beer.PasteurizedEnum.NIE:
                        pasteurized = "Nie";
                        break;
                }
                WindowHandler.lPasteurized.Content = pasteurized;
                WindowHandler.lPasteurizedRow.Visibility = Visibility.Visible;
            }

            //---->

            if (bh.beer.Flavored == Beer.FlavoredEnum.NULL 
                || bh.beer.Flavored == Beer.FlavoredEnum.NIEWIEM)
            {
                WindowHandler.lFlavoredRow.Visibility = Visibility.Collapsed;
            }
            else
            {
                string flavored = "";
                switch (bh.beer.Flavored)
                {
                    case Beer.FlavoredEnum.TAK:
                        flavored = "Tak";
                        break;
                    case Beer.FlavoredEnum.NIE:
                        flavored = "Nie";
                        break;
                }
                WindowHandler.lFlavored.Content = flavored;
                WindowHandler.lFlavoredRow.Visibility = Visibility.Visible;
            }
            
            //---->

            if (bh.beer.Type == Beer.TypeEnum.NULL)
            {
                WindowHandler.lTypeRow.Visibility = Visibility.Collapsed;
            }
            else
            {
                string type = "";
                switch (bh.beer.Type)
                {
                    case Beer.TypeEnum.BEZALKOHOLOWE:
                        type = "Bezalkoholowe";
                        break;
                    case Beer.TypeEnum.LEKKIE:
                        type = "Lekkie";
                        break;
                    case Beer.TypeEnum.MOCNE:
                        type = "Mocne";
                        break;
                    case Beer.TypeEnum.PELNE:
                        type = "Pełne";
                        break;
                }
                WindowHandler.lType.Content = type;
                WindowHandler.lTypeRow.Visibility = Visibility.Visible;
            }

            //---->

            if (bh.beer.Placeofbrew == Beer.PlaceofbrewEnum.NULL)
            {
                WindowHandler.lPlaceofbrewRow.Visibility = Visibility.Collapsed;
            }
            else
            {
                string placeofbrew = "";
                switch (bh.beer.Placeofbrew)
                {
                    case Beer.PlaceofbrewEnum.BROWAR:
                        placeofbrew = "Browar";
                        break;
                    case Beer.PlaceofbrewEnum.RESTAURACJA:
                        placeofbrew = "Restauracja";
                        break;
                    case Beer.PlaceofbrewEnum.DOM:
                        placeofbrew = "Dom";
                        break;
                }
                WindowHandler.lPlaceofbrew.Content = placeofbrew;
                WindowHandler.lPlaceofbrewRow.Visibility = Visibility.Visible;
            }

            //---->
            
            if(bh.beer.Family == null)
            {
                WindowHandler.lFamilyRow.Visibility = Visibility.Collapsed;
            }
            else 
            {
                WindowHandler.lFamily.Content = bh.beer.Family.Name;
                WindowHandler.lFamilyRow.Visibility = Visibility.Visible;
            }
            
            //---->

            if (bh.beer.Country == null)
            {
                WindowHandler.lCountryRow.Visibility = Visibility.Collapsed;
            }
            else
            {
                WindowHandler.lCountry.Content = bh.beer.Country.Name;
                WindowHandler.lCountryRow.Visibility = Visibility.Visible;
            }

            //---->

            if (bh.beer.Region == null)
            {
                WindowHandler.lRegionRow.Visibility = Visibility.Collapsed;
            }
            else
            {
                WindowHandler.lRegion.Content = bh.beer.Region.Name;
                WindowHandler.lRegionRow.Visibility = Visibility.Visible;
            }

            //---->

            if (bh.beer.City == null)
            {
                WindowHandler.lCityRow.Visibility = Visibility.Collapsed;
            }
            else
            {
                WindowHandler.lCity.Content = bh.beer.City.Name;
                WindowHandler.lCityRow.Visibility = Visibility.Visible;
            }
        }
    }
}
