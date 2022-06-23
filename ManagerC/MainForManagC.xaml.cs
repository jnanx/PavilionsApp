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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using PavilionsApp.Model;

namespace PavilionsApp
{
    
    public partial class MainForManagC : Window
    {
        PAVILIONSEntities db;

        public MainForManagC()
        {
            InitializeComponent();
            db = new PAVILIONSEntities();
            AllShopCenters.ItemsSource = db.shoppingCenters.OrderBy(sc => sc.city).ThenBy(sc => sc.shoppingCentersStatuses.shoppingCenterStatusID).ToList();
            //.Where(sc => sc.shoppingCenterStatusID != 4)
            //.Select(s => new
            //{
            //    name = s.shoppingCenterName,
            //    statusName = s.shoppingCentersStatuses.shoppingCenterStatusName,
            //    numofpav = s.numberOfPavilions,
            //    city = s.city,
            //    cost = s.cost,
            //    numoffloors = s.numberOfFloors,
            //    coeffaddcost = s.coefficientOfAddedCost
            //})

        }

      

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void ToAdd_Click(object sender, RoutedEventArgs e)
        {
            AddShopCenter addShopCenter = new AddShopCenter();
            addShopCenter.Show();
            this.Close();
        }

        private void ToSaveDeleted_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void AllShopCenters_KeyDown(object sender, KeyEventArgs e)
        {
            var datagridsc = sender as DataGrid;
            if (datagridsc.SelectedItem != null)
            {
                var sc = datagridsc.SelectedItem as shoppingCenter;
                //var db = new PAVILIONSEntities();
                db.shoppingCenters.Find(sc.shoppingCenterID).shoppingCenterStatusID = 4;
            }
        }
    }
}
