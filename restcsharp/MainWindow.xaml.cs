using RestSharp;
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

using restcsharp.view.list;
using restcsharp.view.grid;
using restcsharp.exception;

namespace restcsharp
{
    public partial class MainWindow : Window
    {
        const String URL = "http://rest.poradnikpiwny.pl";

        private RestClient client;
        private AbstractRestListView topLV, lastAddedLV, lastLV;
        private RestSearchListView searchLV;
        private RestInfoGrid infoGrid;

        public MainWindow()
        {
            InitializeComponent();
            client = new RestClient(URL);

            topLV = new RestToprankingListView(client, lvTopRanking);
            lastAddedLV = new RestLastaddedListView(client, lvLastAdded);
            searchLV = new RestSearchListView(client,lvSearch);
            infoGrid = new RestInfoGrid(client, this);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (tPopular.IsSelected && !(lastLV is RestToprankingListView))
                {
                    lastLV = topLV;
                    topLV.fillData();
                    infoGrid.hide();
                }
                else if (tLastAdded.IsSelected && !(lastLV is RestLastaddedListView))
                {
                    lastLV = lastAddedLV;
                    lastAddedLV.fillData();
                    infoGrid.hide();
                }
                else if (tSearchResult.IsSelected && !(lastLV is RestSearchListView))
                {
                    lastLV = searchLV;
                    searchLV.fillData();
                    infoGrid.hide();
                }
            }
            catch (JsonParseException ex)
            {
                _serveException(ex);
            }
        }

        private void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ListView lv = sender as ListView;
            if (lv.SelectedIndex == -1)
            {
                infoGrid.hide();
                return;
            }

            try
            {
                int id = lastLV.getId();
                infoGrid.fillData(id);
            }
            catch (Exception ex)
            {
                if (ex is BeerNotFoundException || ex is JsonParseException)
                {
                    _serveException(ex);
                    return;
                }

                throw;
            }
        }

        private void _serveException(Exception ex)
        {
            if (ex.Message.Length != 0)
            {
                MessageBox.Show(ex.Message, "Ostrzeżenie");
            }
        }

        private void b_search_Click(object sender, RoutedEventArgs e)
        {
            if (!searchLV.query.Equals(t_search.Text))
            {
                searchLV.query = t_search.Text;
                tSearchResult.Visibility = Visibility.Visible;

                if (BeerTab.SelectedItem != tSearchResult)
                {
                    BeerTab.SelectedItem = tSearchResult;
                    TabControl_SelectionChanged(BeerTab, null);
                }
                else
                {
                    searchLV.fillData();
                    infoGrid.hide();
                }
            }
        }
    }
}
